using PotatoSerializer;
using System.Collections.Generic;
using UnityEngine;


public class TestScript : MonoBehaviour {

	[SerializeField]
	private TextAsset m_jsonFile;

	private class TestObj : ISerialObject {

		private struct StringProxy : ISerialProxy<string> {
			private string m_value;
			public StringProxy(string value) {
				m_value = value;
			}
			public string GetSerialProxy() {
				return m_value;
			}
			public void SetSerialProxy(string value) {
				m_value = value;
			}
		}
		private struct Int16Proxy : ISerialProxy<short> {
			private short m_value;
			public short GetSerialProxy() {
				return m_value;
			}
			public void SetSerialProxy(short value) {
				m_value = value;
			}
		}

		private class SubObj : ISerialObject {
			private double m_double;
			private string m_string;
			private uint m_integer;
			private List<Vector3Int> m_array;
			private List<StringProxy> m_stringProxy;
			private HashSet<Int16Proxy> m_shortProxy;
			public SubObj() {
				m_array = new List<Vector3Int>();
				m_stringProxy = new List<StringProxy>();
				m_shortProxy = new HashSet<Int16Proxy>();
			}
			public void Serialize(ISerializer serializer) {
				serializer.Serialize("objectDouble", ref m_double);
				serializer.Serialize("objectString", ref m_string);
				serializer.Serialize("objectInteger", ref m_integer);
				serializer.Serialize("objectArray", m_array);
				serializer.ProxyString("stringProxy", m_stringProxy);
				serializer.ProxyInt16("shortProxy", m_shortProxy);
			}
		}

		private double m_exponentA;
		private double m_exponentB;
		private double m_exponentC;
		private int m_integer;
		private double m_double;
		private string m_string;
		private bool m_booleanA;
		private bool m_booleanB;
		private SubObj m_null;
		private SubObj m_subObj;
		private List<int> m_array;
		private List<ulong> m_nullArray;

		public TestObj() {
			m_array = new List<int>();
			m_nullArray = new List<ulong>();
		}

		public void Serialize(ISerializer serializer) {
			serializer.Serialize("exponentA", ref m_exponentA);
			serializer.Serialize("exponentB", ref m_exponentB);
			serializer.Serialize("exponentC", ref m_exponentC);
			serializer.Serialize("integer", ref m_integer);
			serializer.Serialize("double", ref m_double);
			serializer.Serialize("string", ref m_string);
			serializer.Serialize("booleanA", ref m_booleanA);
			serializer.Serialize("booleanB", ref m_booleanB);
			serializer.Serialize("null", ref m_null);
			serializer.Serialize("object", ref m_subObj);
			serializer.Serialize("array", m_array);
			serializer.Serialize("nullArray", m_nullArray);
		}
	}

	private void Awake() {

		Serializer serializer = new Serializer();
		TestObj obj = serializer.ReadObject<TestObj>(m_jsonFile.text);
		string result = serializer.WriteObject(obj,true);
		serializer.ReadObject<TestObj>(result);
		Debug.Log(result);
	}

}
