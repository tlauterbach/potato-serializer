using PotatoSerializer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestScript : MonoBehaviour {

	[SerializeField]
	private TextAsset m_jsonFile;

	private class TestObj : ISerialObject {

		private class SubObj : ISerialObject {

			private double m_double;
			private string m_string;

			public void Serialize(ISerializer serializer) {
				serializer.Serialize("double", ref m_double);
				serializer.Serialize("string", ref m_string);
			}
		}

		private double m_exponentA;
		private double m_exponentB;
		private double m_exponentC;
		private double m_double;
		private SubObj m_subObj;

		public void Serialize(ISerializer serializer) {
			serializer.Serialize("exponentA", ref m_exponentA);
			serializer.Serialize("exponentB", ref m_exponentB);
			serializer.Serialize("exponentC", ref m_exponentC);
			serializer.Serialize("double", ref m_double);
			//serializer.Serialize("object", ref m_subObj);
		}
	}

	private void Awake() {

		Serializer serializer = new Serializer();
		TestObj obj = serializer.ReadObject<TestObj>(m_jsonFile.text);
		Debug.Log(serializer.WriteObject(obj));
	}

}
