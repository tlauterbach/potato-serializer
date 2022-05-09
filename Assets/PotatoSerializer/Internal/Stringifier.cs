using System;
using System.Globalization;
using System.Text;

namespace PotatoSerializer {

	internal class Stringifier {

		private const string FORMAT_STRING = "\"{0}\"";
		private StringBuilder m_builder;
		private int m_indent = 0;

		public Stringifier() {
			m_builder = new StringBuilder();
		}

		public string Stringify(JsonNode node) {
			m_builder.Clear();
			if (node.IsType(JsonType.Array)) {
				AppendArray(node);
			} else if (node.IsType(JsonType.Object)) {
				AppendObject(node);
			} else {
				throw new Exception(
					"Cannot stringify a Json root " +
					"that is not an Object or Array"
				);
			}
			string result = m_builder.ToString();
			m_builder.Clear();
			return result;
		}

		public string Prettify(string str) {
			m_indent = 0;
			bool quoted = false;
			for (var i = 0; i < str.Length; i++) {
				var ch = str[i];
				switch (ch) {
					case '{':
					case '[':
						m_builder.Append(ch);
						if (!quoted) {
							m_builder.AppendLine();
							++m_indent;
							for (int ix = 0; ix < m_indent; ix++) {
								m_builder.Append('\t');
							}
						}
						break;
					case '}':
					case ']':
						if (!quoted) {
							m_builder.AppendLine();
							--m_indent;
							for (int ix = 0; ix < m_indent; ix++) {
								m_builder.Append('\t');
							}
						}
						m_builder.Append(ch);
						break;
					case '"':
						m_builder.Append(ch);
						bool escaped = false;
						var index = i;
						while (index > 0 && str[--index] == '\\') {
							escaped = !escaped;
						}
						if (!escaped) {
							quoted = !quoted;
						}
						break;
					case ',':
						m_builder.Append(ch);
						if (!quoted) {
							m_builder.AppendLine();
							for (int ix = 0; ix < m_indent; ix++) {
								m_builder.Append('\t');
							}
						}
						break;
					case ':':
						m_builder.Append(ch);
						if (!quoted) {
							m_builder.Append(" ");
						}
						break;
					default:
						m_builder.Append(ch);
						break;
				}
			}
			return m_builder.ToString();
		}

		private void AppendObject(JsonNode node) {
			m_builder.Append('{');
			bool isFirst = true;
			foreach (string name in node.Names) {
				if (isFirst) {
					isFirst = false;
				} else {
					m_builder.Append(',');
				}
				m_builder.Append(string.Format(FORMAT_STRING,name)).Append(':');
				AppendValue(node[name]);
			}
			m_builder.Append('}');
		}
		private void AppendArray(JsonNode node) {
			m_builder.Append('[');
			bool isFirst = true;
			foreach (JsonNode child in node.Children) {
				if (isFirst) {
					isFirst = false;
				} else {
					m_builder.Append(',');
				}
				AppendValue(child);
			}
			m_builder.Append(']');
		}
		private void AppendValue(JsonNode node) {
			switch (node.Type) {
				case JsonType.Null:
					m_builder.Append("null");
					break;
				case JsonType.Bool:
					m_builder.Append(node.AsBool() ? "true" : "false");
					break;
				case JsonType.Array:
					AppendArray(node);
					break;
				case JsonType.Object:
					AppendObject(node);
					break;
				case JsonType.Number:
					double value = node.AsDouble();
					m_builder.Append(value.ToString(CultureInfo.InvariantCulture.NumberFormat));
					break;
				case JsonType.String:
					m_builder.Append(string.Format(FORMAT_STRING, node.AsString()));
					break;
				default:
					throw new NotImplementedException();
			}
		}

	}

}