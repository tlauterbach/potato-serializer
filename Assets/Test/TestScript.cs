using PotatoSerializer;
using System.Collections.Generic;
using UnityEngine;


public class TestScript : MonoBehaviour {

	[SerializeField]
	private TextAsset m_jsonFile;

	private class TestObj : ISerialObject {

		private class SubObj : ISerialObject {

			private double m_double;
			private string m_string;
			private uint m_integer;

			public void Serialize(ISerializer serializer) {
				serializer.Serialize("objectDouble", ref m_double);
				serializer.Serialize("objectString", ref m_string);
				serializer.Serialize("objectInteger", ref m_integer);
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

		public TestObj() {
			m_array = new List<int>();
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
		}
	}

	private void Awake() {

		Serializer serializer = new Serializer();
		TestObj obj = serializer.ReadObject<TestObj>(m_jsonFile.text);
		string result = serializer.WriteObject(obj);
		obj = serializer.ReadObject<TestObj>(result);
		Debug.Log(result);
	}

}
