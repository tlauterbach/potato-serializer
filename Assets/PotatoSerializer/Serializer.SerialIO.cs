using System;
using System.Collections.Generic;

namespace PotatoSerializer {


	public sealed partial class Serializer {

		internal delegate TVal JsonToValue<TVal>(JsonNode node);
		internal delegate JsonNode ValueToJson<TVal>(TVal value);

		private sealed partial class SerialIO : ISerializer {

			public bool IsWriting { get; private set; }
			public bool IsReading { get; private set; }

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

			public string WriteObject<T>(T obj, bool pretty) where T : ISerialObject, new() {
				IsWriting = true;
				IsReading = false;
				m_root = new JsonNode(JsonType.Object);
				m_stack.Clear();
				m_stack.Push(m_root);

				obj.Serialize(this);

				string result = m_stringifier.Stringify(m_root);
				if (pretty) {
					result = m_stringifier.Prettify(result);
				}
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

			/*
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
			*/


			#region Converters

			private T JsonToSerialObject<T>(JsonNode node) where T : ISerialObject, new() {
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
			private JsonNode SerialObjectToJson<T>(T value) where T : ISerialObject, new() {
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
			private static T JsonToEnum<T>(JsonNode node) where T : struct, Enum {
				if (node.IsNull()) {
					return default;
				} else {
					if (System.Enum.TryParse(node.AsString(), out T value)) {
						return value;
					} else {
						throw new Exception(string.Format(
							"Could not parse `{0}' as Enum type " +
							"`{1}'", node.AsString(), typeof(T).Name
						));
					}
				}
			}
			private static JsonNode EnumToJson<T>(T value) where T : struct, Enum {
				return new JsonNode(value.ToString());
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

			private static float JsonToFloat(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsSingle();
				}
			}
			private static JsonNode FloatToJson(float value) {
				return new JsonNode(value);
			}

			private static bool JsonToBool(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsBool();
				}
			}
			private static JsonNode BoolToJson(bool value) {
				return new JsonNode(value);
			}
			private static int JsonToInt32(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsInt32();
				}
			}
			private static JsonNode Int32ToJson(int value) {
				return new JsonNode(value);
			}
			private static short JsonToInt16(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsInt16();
				}
			}
			private static JsonNode Int16ToJson(short value) {
				return new JsonNode(value);
			}
			private static long JsonToInt64(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsInt64();
				}
			}
			private static JsonNode Int64ToJson(long value) {
				return new JsonNode(value);
			}

			private static uint JsonToUInt32(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsUInt32();
				}
			}
			private static JsonNode UInt32ToJson(uint value) {
				return new JsonNode(value);
			}
			private static ushort JsonToUInt16(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsUInt16();
				}
			}
			private static JsonNode UInt16ToJson(ushort value) {
				return new JsonNode(value);
			}
			private static ulong JsonToUInt64(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsUInt64();
				}
			}
			private static JsonNode UInt64ToJson(ulong value) {
				return new JsonNode(value);
			}
			private static byte JsonToByte(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsByte();
				}
			}
			private static JsonNode ByteToJson(byte value) {
				return new JsonNode(value);
			}
			private static sbyte JsonToSByte(JsonNode node) {
				if (node.IsNull()) {
					return default;
				} else {
					return node.AsSByte();
				}
			}
			private static JsonNode SByteToJson(sbyte value) {
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

			private static TVal JsonToProxySingle<TVal>(JsonNode node) where TVal : ISerialProxy<float>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsSingle());
					return proxy;
				}
			}
			private static JsonNode ProxySingleToJson<TVal>(TVal value) where TVal : ISerialProxy<float>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static TVal JsonToProxyBool<TVal>(JsonNode node) where TVal : ISerialProxy<bool>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsBool());
					return proxy;
				}
			}
			private static JsonNode ProxyBoolToJson<TVal>(TVal value) where TVal : ISerialProxy<bool>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static TVal JsonToProxyInt32<TVal>(JsonNode node) where TVal : ISerialProxy<int>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsInt32());
					return proxy;
				}
			}
			private static JsonNode ProxyInt32ToJson<TVal>(TVal value) where TVal : ISerialProxy<int>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static TVal JsonToProxyInt16<TVal>(JsonNode node) where TVal : ISerialProxy<short>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsInt16());
					return proxy;
				}
			}
			private static JsonNode ProxyInt16ToJson<TVal>(TVal value) where TVal : ISerialProxy<short>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static TVal JsonToProxyInt64<TVal>(JsonNode node) where TVal : ISerialProxy<long>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsInt64());
					return proxy;
				}
			}
			private static JsonNode ProxyInt64ToJson<TVal>(TVal value) where TVal : ISerialProxy<long>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static TVal JsonToProxyUInt32<TVal>(JsonNode node) where TVal : ISerialProxy<uint>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsUInt32());
					return proxy;
				}
			}
			private static JsonNode ProxyUInt32ToJson<TVal>(TVal value) where TVal : ISerialProxy<uint>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static TVal JsonToProxyUInt16<TVal>(JsonNode node) where TVal : ISerialProxy<ushort>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsUInt16());
					return proxy;
				}
			}
			private static JsonNode ProxyUInt16ToJson<TVal>(TVal value) where TVal : ISerialProxy<ushort>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static TVal JsonToProxyUInt64<TVal>(JsonNode node) where TVal : ISerialProxy<ulong>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsUInt64());
					return proxy;
				}
			}
			private static JsonNode ProxyUInt64ToJson<TVal>(TVal value) where TVal : ISerialProxy<ulong>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static TVal JsonToProxyByte<TVal>(JsonNode node) where TVal : ISerialProxy<byte>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsByte());
					return proxy;
				}
			}
			private static JsonNode ProxyByteToJson<TVal>(TVal value) where TVal : ISerialProxy<byte>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			private static TVal JsonToProxySByte<TVal>(JsonNode node) where TVal : ISerialProxy<sbyte>, new() {
				if (node.IsNull()) {
					return default;
				} else {
					TVal proxy = new TVal();
					proxy.SetSerialProxy(node.AsSByte());
					return proxy;
				}
			}
			private static JsonNode ProxySByteToJson<TVal>(TVal value) where TVal : ISerialProxy<sbyte>, new() {
				return new JsonNode(value.GetSerialProxy());
			}

			#endregion

		}

	}

}