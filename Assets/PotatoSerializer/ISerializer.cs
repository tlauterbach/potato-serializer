using System.Collections.Generic;

namespace PotatoSerializer {
	public interface ISerializer {
		bool IsReading { get; }
		bool IsWriting { get; }

		void ProxyDouble<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<double>, new();
		void ProxyDouble<TVal, TCol>(string name, TCol value)
			where TVal : ISerialProxy<double>, new()
			where TCol : ICollection<TVal>;
		void ProxyDouble<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<double>, new();
		void ProxyDouble<TVal>(string name, ref TVal value) where TVal : ISerialProxy<double>, new();
		void ProxyString<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<string>, new();
		void ProxyString<TVal, TCol>(string name, TCol value)
			where TVal : ISerialProxy<string>, new()
			where TCol : ICollection<TVal>;
		void ProxyString<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<string>, new();
		void ProxyString<TVal>(string name, ref TVal value) where TVal : ISerialProxy<string>, new();
		void Serialize(string name, ICollection<double> value);
		void Serialize(string name, ICollection<string> value);
		void Serialize(string name, IDictionary<string, double> value);
		void Serialize(string name, IDictionary<string, string> value);
		void Serialize(string name, ref double value);
		void Serialize(string name, ref string value);
		void Serialize<TKey>(string name, IDictionary<TKey, double> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, string> value) where TKey : ISerialProxy<string>, new();
	}
}