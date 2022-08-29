using System;
using System.Collections.Generic;

namespace PotatoSerializer {

	internal sealed class JsonNode {

		#region Exceptions

		private class InvalidCastException : System.InvalidCastException {
			public InvalidCastException(JsonNode source, System.Type dest) : base(
				string.Format("Cannot convert JsonNode of type `{0}' to `{1}'",
				source.Type, dest.Name
			)) { }
		}
		private class InvalidTypeException : InvalidOperationException {
			public InvalidTypeException(string operation, JsonType desired) : base(
				string.Format("Cannot perform `{0}' because JsonNode is " +
				"not required type `{2}'", operation, desired
			)) { }
		}

		#endregion

		public JsonNode this[string name] {
			get { return Get(name); }
		}
		public JsonNode this[int index] {
			get { return Get(index); }
		}
		public IEnumerable<JsonNode> Children {
			get {
				switch (Type) {
					case JsonType.Array:
						foreach (JsonNode node in m_list) {
							yield return node;
						}
						break;
					case JsonType.Object:
						foreach (JsonNode node in m_object.Values) {
							yield return node;
						}
						break;
					default:
						yield break;
				}
			}
		}
		public int Count {
			get {
				switch (Type) {
					case JsonType.Array:
						return m_list.Count;
					case JsonType.Object:
						return m_object.Count;
					default:
						return 0;
				}
			}
		}
		public IEnumerable<string> Names {
			get {
				if (IsType(JsonType.Object)) {
					foreach (string name in m_object.Keys) {
						yield return name;
					}
				} else {
					yield break;
				}
			}
		}

		public JsonType Type { get; }

		private Dictionary<string, JsonNode> m_object;
		private List<JsonNode> m_list;
		private string m_stringValue;
		private double m_numberValue;

		#region Constructors

		public JsonNode(string value) {
			Type = JsonType.String;
			m_stringValue = value;
		}
		public JsonNode(double value) {
			Type = JsonType.Number;
			m_numberValue = value;
		}
		public JsonNode(bool value) {
			Type = JsonType.Bool;
			m_numberValue = value ? 1 : 0;
		}
		public JsonNode(JsonType type) {
			switch (type) {
				case JsonType.Array:
					m_list = new List<JsonNode>();
					break;
				case JsonType.Object:
					m_object = new Dictionary<string, JsonNode>();
					break;
				case JsonType.String:
				case JsonType.Number:
				case JsonType.Bool:
					throw new Exception("Please use a different " +
					"constructor for String, Number, or Bool " +
					"types."
				);
			}
			Type = type;
		}

		#endregion

		public JsonNode Get(string name) {
			if (IsType(JsonType.Object)) {
				if (m_object.TryGetValue(name, out JsonNode node)) {
					return node;
				} else {
					return new JsonNode(JsonType.Null);
					/*
					throw new KeyNotFoundException(string.Format(
						"No value with name `{0}' exists " +
						"in JsonNode", name
					));
					*/
				}
			} else {
				throw new InvalidTypeException(nameof(Get), JsonType.Object);
			}
		}
		public JsonNode Get(int index) {
			if (IsType(JsonType.Array)) {
				if (index < 0 || index >= m_list.Count) {
					throw new IndexOutOfRangeException();
				}
				return m_list[index];
			} else {
				throw new InvalidTypeException(nameof(Get), JsonType.Array);
			}
		}
		public void Add(string name, JsonNode value) {
			if (IsType(JsonType.Object)) {
				if (m_object.ContainsKey(name)) {
					throw new Exception(string.Format(
						"Value with name `{0}' already " +
						"exists in JsonNode",
						name
					));
				}
				m_object.Add(name, value);
			} else {
				throw new InvalidTypeException(nameof(Add), JsonType.Object);
			}
		}
		public void Add(JsonNode value) {
			if (IsType(JsonType.Array)) {
				m_list.Add(value);
			} else {
				throw new InvalidTypeException(nameof(Add), JsonType.Object);
			}
		}

		public bool Contains(string name) {
			if (IsType(JsonType.Object)) {
				return m_object.ContainsKey(name);
			} else {
				throw new InvalidTypeException(nameof(Contains), JsonType.Object);
			}
		}

		public bool IsType(JsonType type) {
			return Type == type;
		}
		public bool IsNull() {
			return Type == JsonType.Null;
		}

		#region Conversions

		public string AsString() {
			if (IsType(JsonType.String)) {
				return m_stringValue;
			} else {
				throw new InvalidCastException(this, typeof(string));
			}
		}
		public short AsInt16() {
			if (IsType(JsonType.Number)) {
				return Convert.ToInt16(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(short));
			}
		}
		public int AsInt32() {
			if (IsType(JsonType.Number)) {
				return Convert.ToInt32(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(int));
			}
		}
		public long AsInt64() {
			if (IsType(JsonType.Number)) {
				return Convert.ToInt64(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(long));
			}
		}
		public ushort AsUInt16() {
			if (IsType(JsonType.Number)) {
				return Convert.ToUInt16(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(ushort));
			}
		}
		public uint AsUInt32() {
			if (IsType(JsonType.Number)) {
				return Convert.ToUInt32(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(uint));
			}
		}
		public ulong AsUInt64() {
			if (IsType(JsonType.Number)) {
				return Convert.ToUInt64(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(ulong));
			}
		}
		public float AsSingle() {
			if (IsType(JsonType.Number)) {
				return Convert.ToSingle(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(float));
			}
		}
		public double AsDouble() {
			if (IsType(JsonType.Number)) {
				return Convert.ToDouble(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(double));
			}
		}
		public bool AsBool() {
			if (IsType(JsonType.Bool)) {
				return m_numberValue > 0;
			} else {
				throw new InvalidCastException(this, typeof(bool));
			}
		}
		public byte AsByte() {
			if (IsType(JsonType.Number)) {
				return Convert.ToByte(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(byte));
			}
		}
		public sbyte AsSByte() {
			if (IsType(JsonType.Number)) {
				return Convert.ToSByte(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(sbyte));
			}
		}

		#endregion

	}

}