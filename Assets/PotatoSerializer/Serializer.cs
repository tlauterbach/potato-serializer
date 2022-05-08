using System;
using System.Collections.Generic;

namespace PotatoSerializer {

	public sealed partial class Serializer : ISerializer {

		/// <summary>
		/// Version number of the object that is currently
		/// being read or written
		/// </summary>
		public ushort Version { get; private set; }

		public bool IsReading { get; private set; }
		public bool IsWriting { get; private set; }

		private Lexer m_lexer;
		private Parser m_parser;
		private Stringifier m_stringifier;
		private readonly int m_tabSize;

		private JsonNode m_root;
		private JsonNode m_current;

		private delegate T JsonToValue<T>(JsonNode node);
		private delegate JsonNode ValueToJson<T>(T value);

		public Serializer(int tabSize = 4) {
			m_lexer = new Lexer();
			m_parser = new Parser();
			m_stringifier = new Stringifier();
			m_tabSize = tabSize;
		}

		public T ReadObject<T>(string json) where T : ISerialObject, new() {
			IsReading = true;
			IsWriting = false;

			m_root = m_parser.Parse(m_lexer.Tokenize(json, m_tabSize));
			if (m_root.IsType(JsonType.Array)) {
				throw new Exception("Cannot read a " +
					"Json Array with this function. " +
					"Please use `ReadArray' instead."
				);
			}
			m_current = m_root;
			T obj = new T();
			obj.Serialize(this);
			m_root = null;
			m_current = null;
			IsReading = false;
			return obj;
		}
		public T[] ReadArray<T>(string json) where T : ISerialObject, new() {
			IsReading = true;
			IsWriting = false;

			m_root = m_parser.Parse(m_lexer.Tokenize(json, m_tabSize));
			if (m_root.IsType(JsonType.Array)) {
				throw new Exception("Cannot read a " +
					"Json Object with this function. " +
					"Please use `ReadObject' instead."
				);
			}
			m_current = m_root;
			T[] obj = new T[m_root.Count];
			for (int ix = 0; ix < m_root.Count; ix++) {
				obj[ix] = new T();
				obj[ix].Serialize(this);
			}
			m_root = null;
			m_current = null;
			IsReading = false;
			return obj;
		}
		public string WriteObject<T>(T value) where T : ISerialObject, new() {
			IsReading = false;
			IsWriting = true;

			m_root = new JsonNode(JsonType.Object);
			m_current = m_root;
			value.Serialize(this);

			string result = m_stringifier.Stringify(m_root);
			m_root = null;
			m_current = null;
			IsWriting = false;
			return result;
		}
		public string WriteArray<T>(T[] array) where T : ISerialObject, new() {
			IsReading = false;
			IsWriting = true;

			m_root = new JsonNode(JsonType.Array);
			foreach (T item in array) {
				m_current = new JsonNode(JsonType.Object);
				item.Serialize(this);
				m_root.Add(m_current);
			}
			string result = m_stringifier.Stringify(m_root);
			m_root = null;
			m_current = null;
			IsWriting = false;
			return result;
		}

		public bool IsArray(string json) {
			JsonNode node = m_parser.Parse(m_lexer.Tokenize(json,m_tabSize));
			return node.IsType(JsonType.Array);
		}
		public bool IsObject(string json) {
			JsonNode node = m_parser.Parse(m_lexer.Tokenize(json,m_tabSize));
			return node.IsType(JsonType.Object);
		}

		/*
		private void DoBase<T>(string name, ref T value, JsonToValue<T> toValue, ValueToJson<T> toJson) {

		}
		private void DoList<T>(string name, ref List<T> value, JsonToValue<T> toValue, ValueToJson<T> toJson) {

		}
		*/

	}

}