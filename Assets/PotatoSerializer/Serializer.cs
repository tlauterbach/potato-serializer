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

		private Dictionary<string, object> m_map;
		private Dictionary<string, object> m_currentObject;
		private IList<object> m_currentList;
		private int m_currentIndex;

		private const string JSON_STRING = "\"{0}\"";

		private delegate string StringConverter<T>(T value);

		private delegate double NumberConverter<T>(T value);


		private delegate T ValueConverter<T>(string value);

		private void DoString<T>(string name, T value, StringConverter<T> toString, ValueConverter<T> fromString) {
			if (IsReading) {
				
			} else {
				WriteString(name, toString(value));
			}
		}
		//private void DoNumber<T>(string name, )

		private void DoStringArray<T>(string name, T[] array) {
			if (IsReading) {
				
			} else {
				
			}
		}
		private void DoStringList<T>(string name, IList<T> list) {
			
		}

		

		private string StringToJsonName(string value) {
			return string.Format(JSON_STRING, value);
		}
		private string StringToJsonString(string value) {
			return string.Format(JSON_STRING, value);
		}
		private string BoolToJsonBool(bool value) {
			return value ? "true" : "false";
		}
		private bool JsonBoolToBool(string value) {
			if (bool.TryParse(value, out bool result)) {
				return result;
			} else {
				throw new ValueReadException();
			}

		}


	}

}