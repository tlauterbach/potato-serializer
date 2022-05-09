using System;
using System.Collections.Generic;

namespace PotatoSerializer {

	public sealed partial class Serializer {

		private SerialIO m_io;

		public Serializer(int tabSize = 4) {
			m_io = new SerialIO(tabSize);
		}

		public T ReadObject<T>(string json) where T : ISerialObject, new() {
			return m_io.ReadObject<T>(json);
		}
		public T[] ReadArray<T>(string json) where T : ISerialObject, new() {
			return m_io.ReadArray<T>(json);
		}
		public string WriteObject<T>(T value, bool pretty = false) where T : ISerialObject, new() {
			return m_io.WriteObject(value, pretty);
		}

		public string WriteArray<T>(T[] array, bool pretty = false) where T : ISerialObject, new() {
			return m_io.WriteArray(array, pretty);
		}
		public bool IsArray(string json) {
			return m_io.IsArray(json);
		}
		public bool IsObject(string json) {
			return m_io.IsObject(json);
		}

	}

}