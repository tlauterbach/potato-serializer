using System;
using System.Collections.Generic;

namespace PotatoSerializer {

	public class JsonNode {
		public enum Type {
			String,
			Number,
			Bool,
			Object,
			Array,
			Null
		}

		private class InvalidCastException : System.InvalidCastException {
			public InvalidCastException(JsonNode source, System.Type dest) : base(
				string.Format("Cannot convert JsonNode of type `{0}' to `{1}'",
				source.m_type, dest.Name
			)) { }
		}
		private class InvalidTypeException : InvalidOperationException {
			public InvalidTypeException(string operation, Type desired) : base(
				string.Format("Cannot perform `{0}' because JsonNode is " +
				"not required type `{2}'", operation, desired
			)) { }
		}

		public JsonNode this[string name] {
			get { return GetValue(name); }
		}
		public JsonNode this[int index] {
			get { return GetValue(index); }
		}

		private readonly Type m_type;

		private Dictionary<string, JsonNode> m_object;
		private List<JsonNode> m_list;
		private string m_stringValue;
		private double m_numberValue;

		public JsonNode(string value) {
			m_type = Type.String;
			m_stringValue = value;
		}
		public JsonNode(double value) {
			m_type = Type.Number;
			m_numberValue = value;
		}
		public JsonNode(bool value) {
			m_type = Type.Bool;
			m_numberValue = value ? 1 : 0;
		}
		public JsonNode(Type type) {
			switch (type) {
				case Type.Array:
					m_list = new List<JsonNode>();
					break;
				case Type.Object:
					m_list = new List<JsonNode>();
					break;
				case Type.String:
				case Type.Number:
				case Type.Bool:
					throw new Exception("Please use a different " +
					"constructor for String, Number, or Bool " +
					"types."
				);
			}
			m_type = type;
		}

		public JsonNode GetValue(string name) {
			if (IsType(Type.Object)) {
				if (m_object.TryGetValue(name, out JsonNode node)) {
					return node;
				} else {
					throw new KeyNotFoundException(string.Format(
						"No value with name `{0}' exists " +
						"in JsonNode", name
					));
				}
			} else {
				throw new InvalidTypeException(nameof(GetValue), Type.Object);
			}
		}
		public JsonNode GetValue(int index) {
			if (IsType(Type.Array)) {
				if (index < 0 || index >= m_list.Count) {
					throw new IndexOutOfRangeException();
				}
				return m_list[index];
			} else {
				throw new InvalidTypeException(nameof(GetValue), Type.Array);
			}
		}
		public void AddValue(string name, JsonNode value) {
			if (IsType(Type.Object)) {
				if (m_object.ContainsKey(name)) {
					throw new Exception(string.Format(
						"Value with name `{0}' already " +
						"exists in JsonNode", 
						name
					));
				}
				m_object.Add(name, value);
			} else {
				throw new InvalidTypeException(nameof(AddValue), Type.Object);
			}
		}
		public void AddValue(JsonNode value) {
			if (IsType(Type.Array)) {
				m_list.Add(value);
			} else {
				throw new InvalidTypeException(nameof(AddValue), Type.Object);
			}
		}

		public bool IsType(Type type) {
			return m_type == type;
		}
		public bool IsNull() {
			return m_type == Type.Null;
		}

		public string AsString() {
			if (IsType(Type.String)) {
				return m_stringValue;
			} else {
				throw new InvalidCastException(this, typeof(string));
			}
		}
		public short AsInt16() {
			if (IsType(Type.Number)) {
				return Convert.ToInt16(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(short));
			}
		}
		public int AsInt32() {
			if (IsType(Type.Number)) {
				return Convert.ToInt32(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(int));
			}
		}
		public long AsInt64() {
			if (IsType(Type.Number)) {
				return Convert.ToInt64(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(long));
			}
		}
		public ushort AsUInt16() {
			if (IsType(Type.Number)) {
				return Convert.ToUInt16(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(ushort));
			}
		}
		public uint AsUInt32() {
			if (IsType(Type.Number)) {
				return Convert.ToUInt32(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(uint));
			}
		}
		public ulong AsUInt64() {
			if (IsType(Type.Number)) {
				return Convert.ToUInt64(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(ulong));
			}
		}
		public float AsSingle() {
			if (IsType(Type.Number)) {
				return Convert.ToSingle(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(float));
			}
		}
		public double AsDouble() {
			if (IsType(Type.Number)) {
				return Convert.ToDouble(m_numberValue);
			} else {
				throw new InvalidCastException(this, typeof(double));
			}
		}
		public bool AsBool() {
			if (IsType(Type.Bool)) {
				return m_numberValue > 0;
			} else {
				throw new InvalidCastException(this, typeof(bool));
			}
		}
	}

}