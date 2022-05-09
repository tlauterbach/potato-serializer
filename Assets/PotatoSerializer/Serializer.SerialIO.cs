using System;
using System.Collections.Generic;

namespace PotatoSerializer {


	public sealed partial class Serializer {

		internal delegate TVal JsonToValue<TVal>(JsonNode node);
		internal delegate JsonNode ValueToJson<TVal>(TVal value);

		private sealed partial class SerialIO : ISerializer {

			private Lexer m_lexer;
			private Parser m_parser;
			private Stringifier m_stringifier;
			private int m_tabSize;

			private JsonNode m_root;
			private Stack<JsonNode> m_stack;
			private JsonNode Current {
				get { return m_stack.Peek(); }
			}

			public SerialIO(int tabSize = 4) {
				m_lexer = new Lexer();
				m_parser = new Parser();
				m_stringifier = new Stringifier();
				m_stack = new Stack<JsonNode>();
				m_tabSize = tabSize;
			}

			public string WriteObject<T>(T obj) where T : ISerialObject, new() {
				IsWriting = true;
				IsReading = false;
				m_root = new JsonNode(JsonType.Object);
				m_stack.Clear();
				m_stack.Push(m_root);

				obj.Serialize(this);

				string result = m_stringifier.Stringify(m_root);
				IsWriting = false;
				m_stack.Clear();
				return result;

			}
			public T ReadObject<T>(string json) where T : ISerialObject, new() {
				IsWriting = false;
				IsReading = true;
				m_root = m_parser.Parse(m_lexer.Tokenize(json, m_tabSize));
				m_stack.Clear();
				m_stack.Push(m_root);

				T obj = new T();
				obj.Serialize(this);

				IsReading = false;
				m_stack.Clear();
				return obj;

			}

			public bool IsWriting { get; private set; }
			public bool IsReading { get; private set; }


			private void DoSerialize<T>(string name, ref T value, JsonToValue<T> read, ValueToJson<T> write) {
				if (IsReading) {
					value = read(Current[name]);
				} else {
					Current.Add(name, write(value));
				}
			}
			private void DoSerializeCollection<T>(string name, ICollection<T> value, JsonToValue<T> read, ValueToJson<T> write) {
				if (IsReading) {
					if (Current.Contains(name)) {
						if (Current[name].IsType(JsonType.Array)) {
							value.Clear();
							foreach (JsonNode item in Current[name].Children) {
								value.Add(read(item));
							}
						} else {
							throw new Exception(string.Format("Value `{0}' is not an array", name));
						}
					} else {
						throw new KeyNotFoundException(string.Format("No value named `{0}' exists to read", name));
					}
				} else {
					JsonNode array = new JsonNode(JsonType.Array);
					foreach (T item in value) {
						array.Add(write(item));
					}
					Current.Add(name, array);
				}
			}

			private void DoSerializeDictionary<T>(string name, IDictionary<string, T> value, JsonToValue<T> read, ValueToJson<T> write) {
				if (IsReading) {
					if (Current.Contains(name)) {
						JsonNode node = Current[name];
						if (node.IsType(JsonType.Object)) {
							value.Clear();
							foreach (string child in node.Names) {
								value.Add(child, read(node[child]));
							}
						}
					}
				} else {
					JsonNode obj = new JsonNode(JsonType.Object);
					foreach (var kvp in value) {
						obj.Add(kvp.Key, write(kvp.Value));
					}
					Current.Add(name, obj);
				}
			}

			private void DoSerializeDictionary<TKey, TVal>(string name, IDictionary<TKey, TVal> value, JsonToValue<TVal> read, ValueToJson<TVal> write) where TKey : ISerialProxy<string>, new() {
				if (IsReading) {
					if (Current.Contains(name)) {
						JsonNode node = Current[name];
						if (node.IsType(JsonType.Object)) {
							value.Clear();
							foreach (string child in node.Names) {
								TKey key = new TKey();
								key.SetSerialProxy(child);
								value.Add(key, read(node[child]));
							}
						}
					}
				} else {
					JsonNode obj = new JsonNode(JsonType.Object);
					foreach (var kvp in value) {
						obj.Add(kvp.Key.GetSerialProxy(), write(kvp.Value));
					}
					Current.Add(name, obj);
				}
			}

			private void DoSerializeDictionaryCollection<TVal, TCol>(string name, IDictionary<string, TCol> value, JsonToValue<TVal> read, ValueToJson<TVal> write) where TCol : ICollection<TVal>, new() {

				if (IsReading) {
					if (Current.Contains(name)) {
						JsonNode node = Current[name];
						if (node.IsType(JsonType.Object)) {
							value.Clear();
							foreach (string child in node.Names) {
								if (node[child].IsNull()) {
									value.Add(child, default);
								} else {
									TCol collection = new TCol();
									foreach (JsonNode item in node[child].Children) {
										collection.Add(read(item));
									}
									value.Add(child, collection);
								}
							}
						}
					}
				} else {
					JsonNode obj = new JsonNode(JsonType.Object);
					foreach (var kvp in value) {
						JsonNode node = new JsonNode(JsonType.Array);
						foreach (TVal item in kvp.Value) {
							node.Add(write(item));
						}
						obj.Add(kvp.Key, node);
					}
				}
			}
			private void DoSerializeDictionaryCollection<TKey, TVal, TCol>(string name, IDictionary<TKey, TCol> value, JsonToValue<TVal> read, ValueToJson<TVal> write) where TKey : ISerialProxy<string>, new() where TCol : ICollection<TVal>, new() {

				if (IsReading) {
					if (Current.Contains(name)) {
						JsonNode node = Current[name];
						if (node.IsType(JsonType.Object)) {
							value.Clear();
							foreach (string child in node.Names) {
								TKey key = new TKey();
								key.SetSerialProxy(child);
								if (node[child].IsNull()) {
									value.Add(key, default);
								} else {
									TCol collection = new TCol();
									foreach (JsonNode item in node[child].Children) {
										collection.Add(read(item));
									}
									value.Add(key, collection);
								}
							}
						}
					}
				} else {
					JsonNode obj = new JsonNode(JsonType.Object);
					foreach (var kvp in value) {
						JsonNode node = new JsonNode(JsonType.Array);
						foreach (TVal item in kvp.Value) {
							node.Add(write(item));
						}
						obj.Add(kvp.Key.GetSerialProxy(), node);
					}
				}
			}
			private T JsonToSO<T>(JsonNode node) where T : ISerialObject, new() {
				if (node.IsNull()) {
					return default;
				} else {
					m_stack.Push(node);
					T obj = new T();
					obj.Serialize(this);
					m_stack.Pop();
					return obj;
				}
			}
			private JsonNode SOToJson<T>(T value) where T : ISerialObject, new() {
				if (value == null) {
					return new JsonNode(JsonType.Null);
				} else {
					JsonNode obj = new JsonNode(JsonType.Object);
					m_stack.Push(obj);
					value.Serialize(this);
					m_stack.Pop();
					return obj;
				}
			}
			private static double JsonToDouble(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsDouble();
				}
			}
			private static JsonNode DoubleToJson(double value) {
				return new JsonNode(value);
			}
			private static TVal JsonToProxyDouble<TVal>(JsonNode node) where TVal : ISerialProxy<double>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsDouble());
					return proxy;
				}
			}
			private static JsonNode ProxyDoubleToJson<TVal>(TVal value) where TVal : ISerialProxy<double>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static string JsonToString(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsString();
				}
			}
			private static JsonNode StringToJson(string value) {
				return new JsonNode(value);
			}
			private static TVal JsonToProxyString<TVal>(JsonNode node) where TVal : ISerialProxy<string>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsString());
					return proxy;
				}
			}
			private static JsonNode ProxyStringToJson<TVal>(TVal value) where TVal : ISerialProxy<string>, new() {
				return new JsonNode(value.GetSerialProxy());
			}
		}

	}

}