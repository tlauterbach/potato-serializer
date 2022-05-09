using System.Collections.Generic;

namespace PotatoSerializer {
	public sealed partial class Serializer {
		private sealed partial class SerialIO {

			#region string

			public void Serialize(string name, ref string value) {
				DoSerialize(name, ref value, JsonToString, StringToJson);
			}
			public void Serialize(string name, ICollection<string> value) {
				DoSerializeCollection(name, value, JsonToString, StringToJson);
			}
			public void Serialize(string name, IDictionary<string, string> value) {
				DoSerializeDictionary(name, value, JsonToString, StringToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, string> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToString, StringToJson);
			}

			#endregion

			#region double

			public void Serialize(string name, ref double value) {
				DoSerialize(name, ref value, JsonToDouble, DoubleToJson);
			}
			public void Serialize(string name, ICollection<double> value) {
				DoSerializeCollection(name, value, JsonToDouble, DoubleToJson);
			}
			public void Serialize(string name, IDictionary<string, double> value) {
				DoSerializeDictionary(name, value, JsonToDouble, DoubleToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, double> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToDouble, DoubleToJson);
			}

			#endregion

			#region ISerialProxy<string>

			public void ProxyString<TVal>(string name, ref TVal value) where TVal : ISerialProxy<string>, new() {
				DoSerialize(name, ref value, JsonToProxyString<TVal>, ProxyStringToJson);
			}

			public void ProxyString<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<string>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyString<TVal>, ProxyStringToJson);
			}
			public void ProxyString<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToProxyString<TVal>, ProxyStringToJson);
			}

			public void ProxyString<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<string>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyString<TVal>, ProxyStringToJson);
			}

			#endregion

			#region ISerialProxy<double>

			public void ProxyDouble<TVal>(string name, ref TVal value) where TVal : ISerialProxy<double>, new() {
				DoSerialize(name, ref value, JsonToProxyDouble<TVal>, ProxyDoubleToJson);
			}

			public void ProxyDouble<TVal,TCol>(string name, TCol value) where TVal : ISerialProxy<double>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyDouble<TVal>, ProxyDoubleToJson);
			}
			public void ProxyDouble<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<double>, new() {
				DoSerializeDictionary(name, value, JsonToProxyDouble<TVal>, ProxyDoubleToJson);
			}

			public void ProxyDouble<TKey,TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<double>, new() {
				DoSerializeDictionary<TKey,TVal>(name, value, JsonToProxyDouble<TVal>, ProxyDoubleToJson);
			}

			#endregion

		}
	}
}