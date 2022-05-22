using System;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoSerializer {
	public interface ISerializer {
		bool IsReading { get; }
		bool IsWriting { get; }

		void Enum<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : struct, Enum;
		void Enum<TVal>(string name, ICollection<TVal> value) where TVal : struct, Enum;
		void Enum<TVal>(string name, IDictionary<string, TVal> value) where TVal : struct, Enum;
		void Enum<TVal>(string name, ref TVal value) where TVal : struct, Enum;
		void ProxyBool<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<bool>, new();
		void ProxyBool<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<bool>, new();
		void ProxyBool<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<bool>, new();
		void ProxyBool<TVal>(string name, ref TVal value) where TVal : ISerialProxy<bool>, new();
		void ProxyByte<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<byte>, new();
		void ProxyByte<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<byte>, new();
		void ProxyByte<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<byte>, new();
		void ProxyByte<TVal>(string name, ref TVal value) where TVal : ISerialProxy<byte>, new();
		void ProxyDouble<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<double>, new();
		void ProxyDouble<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<double>, new();
		void ProxyDouble<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<double>, new();
		void ProxyDouble<TVal>(string name, ref TVal value) where TVal : ISerialProxy<double>, new();
		void ProxyInt16<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<short>, new();
		void ProxyInt16<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<short>, new();
		void ProxyInt16<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<short>, new();
		void ProxyInt16<TVal>(string name, ref TVal value) where TVal : ISerialProxy<short>, new();
		void ProxyInt32<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<int>, new();
		void ProxyInt32<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<int>, new();
		void ProxyInt32<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<int>, new();
		void ProxyInt32<TVal>(string name, ref TVal value) where TVal : ISerialProxy<int>, new();
		void ProxyInt64<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<long>, new();
		void ProxyInt64<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<long>, new();
		void ProxyInt64<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<long>, new();
		void ProxyInt64<TVal>(string name, ref TVal value) where TVal : ISerialProxy<long>, new();
		void ProxySByte<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<sbyte>, new();
		void ProxySByte<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<sbyte>, new();
		void ProxySByte<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<sbyte>, new();
		void ProxySByte<TVal>(string name, ref TVal value) where TVal : ISerialProxy<sbyte>, new();
		void ProxySingle<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<float>, new();
		void ProxySingle<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<float>, new();
		void ProxySingle<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<float>, new();
		void ProxySingle<TVal>(string name, ref TVal value) where TVal : ISerialProxy<float>, new();
		void ProxyString<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<string>, new();
		void ProxyString<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<string>, new();
		void ProxyString<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<string>, new();
		void ProxyString<TVal>(string name, ref TVal value) where TVal : ISerialProxy<string>, new();
		void ProxyUInt16<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<ushort>, new();
		void ProxyUInt16<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<ushort>, new();
		void ProxyUInt16<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<ushort>, new();
		void ProxyUInt16<TVal>(string name, ref TVal value) where TVal : ISerialProxy<ushort>, new();
		void ProxyUInt32<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<uint>, new();
		void ProxyUInt32<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<uint>, new();
		void ProxyUInt32<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<uint>, new();
		void ProxyUInt32<TVal>(string name, ref TVal value) where TVal : ISerialProxy<uint>, new();
		void ProxyUInt64<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialProxy<ulong>, new();
		void ProxyUInt64<TVal>(string name, ICollection<TVal> value) where TVal : ISerialProxy<ulong>, new();
		void ProxyUInt64<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<ulong>, new();
		void ProxyUInt64<TVal>(string name, ref TVal value) where TVal : ISerialProxy<ulong>, new();
		void Serialize(string name, ICollection<bool> value);
		void Serialize(string name, ICollection<byte> value);
		void Serialize(string name, ICollection<Color> value);
		void Serialize(string name, ICollection<double> value);
		void Serialize(string name, ICollection<float> value);
		void Serialize(string name, ICollection<int> value);
		void Serialize(string name, ICollection<long> value);
		void Serialize(string name, ICollection<Quaternion> value);
		void Serialize(string name, ICollection<Rect> value);
		void Serialize(string name, ICollection<RectInt> value);
		void Serialize(string name, ICollection<sbyte> value);
		void Serialize(string name, ICollection<short> value);
		void Serialize(string name, ICollection<string> value);
		void Serialize(string name, ICollection<uint> value);
		void Serialize(string name, ICollection<ulong> value);
		void Serialize(string name, ICollection<ushort> value);
		void Serialize(string name, ICollection<Vector2> value);
		void Serialize(string name, ICollection<Vector2Int> value);
		void Serialize(string name, ICollection<Vector3> value);
		void Serialize(string name, ICollection<Vector3Int> value);
		void Serialize(string name, IDictionary<string, bool> value);
		void Serialize(string name, IDictionary<string, byte> value);
		void Serialize(string name, IDictionary<string, Color> value);
		void Serialize(string name, IDictionary<string, double> value);
		void Serialize(string name, IDictionary<string, float> value);
		void Serialize(string name, IDictionary<string, int> value);
		void Serialize(string name, IDictionary<string, long> value);
		void Serialize(string name, IDictionary<string, Quaternion> value);
		void Serialize(string name, IDictionary<string, Rect> value);
		void Serialize(string name, IDictionary<string, RectInt> value);
		void Serialize(string name, IDictionary<string, sbyte> value);
		void Serialize(string name, IDictionary<string, short> value);
		void Serialize(string name, IDictionary<string, string> value);
		void Serialize(string name, IDictionary<string, uint> value);
		void Serialize(string name, IDictionary<string, ulong> value);
		void Serialize(string name, IDictionary<string, ushort> value);
		void Serialize(string name, IDictionary<string, Vector2> value);
		void Serialize(string name, IDictionary<string, Vector2Int> value);
		void Serialize(string name, IDictionary<string, Vector3> value);
		void Serialize(string name, IDictionary<string, Vector3Int> value);
		void Serialize(string name, ref bool value);
		void Serialize(string name, ref byte value);
		void Serialize(string name, ref Color value);
		void Serialize(string name, ref double value);
		void Serialize(string name, ref float value);
		void Serialize(string name, ref int value);
		void Serialize(string name, ref long value);
		void Serialize(string name, ref Quaternion value);
		void Serialize(string name, ref Rect value);
		void Serialize(string name, ref RectInt value);
		void Serialize(string name, ref sbyte value);
		void Serialize(string name, ref short value);
		void Serialize(string name, ref string value);
		void Serialize(string name, ref uint value);
		void Serialize(string name, ref ulong value);
		void Serialize(string name, ref ushort value);
		void Serialize(string name, ref Vector2 value);
		void Serialize(string name, ref Vector2Int value);
		void Serialize(string name, ref Vector3 value);
		void Serialize(string name, ref Vector3Int value);
		void Serialize<TKey, TVal>(string name, IDictionary<TKey, TVal> value)
			where TKey : ISerialProxy<string>, new()
			where TVal : ISerialObject, new();
		void Serialize<TKey>(string name, IDictionary<TKey, bool> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, byte> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, Color> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, double> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, float> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, int> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, long> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, Quaternion> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, Rect> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, RectInt> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, sbyte> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, short> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, string> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, uint> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, ulong> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, ushort> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, Vector2> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, Vector2Int> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, Vector3> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TKey>(string name, IDictionary<TKey, Vector3Int> value) where TKey : ISerialProxy<string>, new();
		void Serialize<TVal>(string name, ICollection<TVal> value) where TVal : ISerialObject, new();
		void Serialize<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialObject, new();
		void Serialize<TVal>(string name, ref TVal value) where TVal : ISerialObject, new();
	}
}