using System;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoSerializer {
	public sealed partial class Serializer {
		private sealed partial class SerialIO : ISerializer { // Serializer.Serialize.cs

			#region ISerialObject

			public void Serialize<TVal>(string name, ref TVal value) where TVal : ISerialObject, new() {
				DoSerialize(name, ref value, JsonToSerialObject<TVal>, SerialObjectToJson);
			}
			public void Serialize<TVal>(string name, ICollection<TVal> value) where TVal : ISerialObject, new() {
				DoSerializeCollection(name, value, JsonToSerialObject<TVal>, SerialObjectToJson);
			}
			public void Serialize<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialObject, new() {
				DoSerializeDictionary(name, value, JsonToSerialObject<TVal>, SerialObjectToJson);
			}
			public void Serialize<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialObject, new() {
				DoSerializeDictionary(name, value, JsonToSerialObject<TVal>, SerialObjectToJson);
			}

			#endregion

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

			#region float

			public void Serialize(string name, ref float value) {
				DoSerialize(name, ref value, JsonToFloat, FloatToJson);
			}
			public void Serialize(string name, ICollection<float> value) {
				DoSerializeCollection(name, value, JsonToFloat, FloatToJson);
			}
			public void Serialize(string name, IDictionary<string, float> value) {
				DoSerializeDictionary(name, value, JsonToFloat, FloatToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, float> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToFloat, FloatToJson);
			}

			#endregion

			#region bool

			public void Serialize(string name, ref bool value) {
				DoSerialize(name, ref value, JsonToBool, BoolToJson);
			}
			public void Serialize(string name, ICollection<bool> value) {
				DoSerializeCollection(name, value, JsonToBool, BoolToJson);
			}
			public void Serialize(string name, IDictionary<string, bool> value) {
				DoSerializeDictionary(name, value, JsonToBool, BoolToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, bool> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToBool, BoolToJson);
			}

			#endregion

			#region int

			public void Serialize(string name, ref int value) {
				DoSerialize(name, ref value, JsonToInt32, Int32ToJson);
			}
			public void Serialize(string name, ICollection<int> value) {
				DoSerializeCollection(name, value, JsonToInt32, Int32ToJson);
			}
			public void Serialize(string name, IDictionary<string, int> value) {
				DoSerializeDictionary(name, value, JsonToInt32, Int32ToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, int> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToInt32, Int32ToJson);
			}

			#endregion

			#region short

			public void Serialize(string name, ref short value) {
				DoSerialize(name, ref value, JsonToInt16, Int16ToJson);
			}
			public void Serialize(string name, ICollection<short> value) {
				DoSerializeCollection(name, value, JsonToInt16, Int16ToJson);
			}
			public void Serialize(string name, IDictionary<string, short> value) {
				DoSerializeDictionary(name, value, JsonToInt16, Int16ToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, short> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToInt16, Int16ToJson);
			}

			#endregion

			#region long

			public void Serialize(string name, ref long value) {
				DoSerialize(name, ref value, JsonToInt64, Int64ToJson);
			}
			public void Serialize(string name, ICollection<long> value) {
				DoSerializeCollection(name, value, JsonToInt64, Int64ToJson);
			}
			public void Serialize(string name, IDictionary<string, long> value) {
				DoSerializeDictionary(name, value, JsonToInt64, Int64ToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, long> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToInt64, Int64ToJson);
			}

			#endregion

			#region uint

			public void Serialize(string name, ref uint value) {
				DoSerialize(name, ref value, JsonToUInt32, UInt32ToJson);
			}
			public void Serialize(string name, ICollection<uint> value) {
				DoSerializeCollection(name, value, JsonToUInt32, UInt32ToJson);
			}
			public void Serialize(string name, IDictionary<string, uint> value) {
				DoSerializeDictionary(name, value, JsonToUInt32, UInt32ToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, uint> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToUInt32, UInt32ToJson);
			}

			#endregion

			#region ushort

			public void Serialize(string name, ref ushort value) {
				DoSerialize(name, ref value, JsonToUInt16, UInt16ToJson);
			}
			public void Serialize(string name, ICollection<ushort> value) {
				DoSerializeCollection(name, value, JsonToUInt16, UInt16ToJson);
			}
			public void Serialize(string name, IDictionary<string, ushort> value) {
				DoSerializeDictionary(name, value, JsonToUInt16, UInt16ToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, ushort> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToUInt16, UInt16ToJson);
			}

			#endregion

			#region ulong

			public void Serialize(string name, ref ulong value) {
				DoSerialize(name, ref value, JsonToUInt64, UInt64ToJson);
			}
			public void Serialize(string name, ICollection<ulong> value) {
				DoSerializeCollection(name, value, JsonToUInt64, UInt64ToJson);
			}
			public void Serialize(string name, IDictionary<string, ulong> value) {
				DoSerializeDictionary(name, value, JsonToUInt64, UInt64ToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, ulong> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToUInt64, UInt64ToJson);
			}

			#endregion

			#region byte

			public void Serialize(string name, ref byte value) {
				DoSerialize(name, ref value, JsonToByte, ByteToJson);
			}
			public void Serialize(string name, ICollection<byte> value) {
				DoSerializeCollection(name, value, JsonToByte, ByteToJson);
			}
			public void Serialize(string name, IDictionary<string, byte> value) {
				DoSerializeDictionary(name, value, JsonToByte, ByteToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, byte> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToByte, ByteToJson);
			}

			#endregion

			#region sbyte

			public void Serialize(string name, ref sbyte value) {
				DoSerialize(name, ref value, JsonToSByte, SByteToJson);
			}
			public void Serialize(string name, ICollection<sbyte> value) {
				DoSerializeCollection(name, value, JsonToSByte, SByteToJson);
			}
			public void Serialize(string name, IDictionary<string, sbyte> value) {
				DoSerializeDictionary(name, value, JsonToSByte, SByteToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, sbyte> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToSByte, SByteToJson);
			}

			#endregion

			#region Enum

			public void Enum<TVal>(string name, ref TVal value) where TVal : struct, Enum {
				DoSerialize(name, ref value, JsonToEnum<TVal>, EnumToJson);
			}
			public void Enum<TVal>(string name, ICollection<TVal> value) where TVal : struct, Enum {
				DoSerializeCollection(name, value, JsonToEnum<TVal>, EnumToJson);
			}
			public void Enum<TVal>(string name, IDictionary<string, TVal> value) where TVal : struct, Enum {
				DoSerializeDictionary(name, value, JsonToEnum<TVal>, EnumToJson);
			}
			public void Enum<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : struct, Enum {
				DoSerializeDictionary(name, value, JsonToEnum<TVal>, EnumToJson);
			}

			#endregion

			#region Vector2

			public void Serialize(string name, ref Vector2 value) {
				DoSerialize(name, ref value, JsonToVector2, Vector2ToJson);
			}
			public void Serialize(string name, ICollection<Vector2> value) {
				DoSerializeCollection(name, value, JsonToVector2, Vector2ToJson);
			}
			public void Serialize(string name, IDictionary<string, Vector2> value) {
				DoSerializeDictionary(name, value, JsonToVector2, Vector2ToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, Vector2> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToVector2, Vector2ToJson);
			}

			#endregion

			#region Vector2Int

			public void Serialize(string name, ref Vector2Int value) {
				DoSerialize(name, ref value, JsonToVector2Int, Vector2IntToJson);
			}
			public void Serialize(string name, ICollection<Vector2Int> value) {
				DoSerializeCollection(name, value, JsonToVector2Int, Vector2IntToJson);
			}
			public void Serialize(string name, IDictionary<string, Vector2Int> value) {
				DoSerializeDictionary(name, value, JsonToVector2Int, Vector2IntToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, Vector2Int> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToVector2Int, Vector2IntToJson);
			}

			#endregion

			#region Vector3

			public void Serialize(string name, ref Vector3 value) {
				DoSerialize(name, ref value, JsonToVector3, Vector3ToJson);
			}
			public void Serialize(string name, ICollection<Vector3> value) {
				DoSerializeCollection(name, value, JsonToVector3, Vector3ToJson);
			}
			public void Serialize(string name, IDictionary<string, Vector3> value) {
				DoSerializeDictionary(name, value, JsonToVector3, Vector3ToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, Vector3> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToVector3, Vector3ToJson);
			}

			#endregion

			#region Vector3Int

			public void Serialize(string name, ref Vector3Int value) {
				DoSerialize(name, ref value, JsonToVector3Int, Vector3IntToJson);
			}
			public void Serialize(string name, ICollection<Vector3Int> value) {
				DoSerializeCollection(name, value, JsonToVector3Int, Vector3IntToJson);
			}
			public void Serialize(string name, IDictionary<string, Vector3Int> value) {
				DoSerializeDictionary(name, value, JsonToVector3Int, Vector3IntToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, Vector3Int> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToVector3Int, Vector3IntToJson);
			}

			#endregion

			#region Color

			public void Serialize(string name, ref Color value) {
				DoSerialize(name, ref value, JsonToColor, ColorToJson);
			}
			public void Serialize(string name, ICollection<Color> value) {
				DoSerializeCollection(name, value, JsonToColor, ColorToJson);
			}
			public void Serialize(string name, IDictionary<string, Color> value) {
				DoSerializeDictionary(name, value, JsonToColor, ColorToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, Color> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToColor, ColorToJson);
			}

			#endregion

			#region Rect

			public void Serialize(string name, ref Rect value) {
				DoSerialize(name, ref value, JsonToRect, RectToJson);
			}
			public void Serialize(string name, ICollection<Rect> value) {
				DoSerializeCollection(name, value, JsonToRect, RectToJson);
			}
			public void Serialize(string name, IDictionary<string, Rect> value) {
				DoSerializeDictionary(name, value, JsonToRect, RectToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, Rect> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToRect, RectToJson);
			}

			#endregion

			#region RectInt

			public void Serialize(string name, ref RectInt value) {
				DoSerialize(name, ref value, JsonToRectInt, RectIntToJson);
			}
			public void Serialize(string name, ICollection<RectInt> value) {
				DoSerializeCollection(name, value, JsonToRectInt, RectIntToJson);
			}
			public void Serialize(string name, IDictionary<string, RectInt> value) {
				DoSerializeDictionary(name, value, JsonToRectInt, RectIntToJson);
			}

			public void Serialize<TKey>(string name, IDictionary<TKey, RectInt> value) where TKey : ISerialProxy<string>, new() {
				DoSerializeDictionary(name, value, JsonToRectInt, RectIntToJson);
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

			public void ProxyDouble<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<double>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyDouble<TVal>, ProxyDoubleToJson);
			}
			public void ProxyDouble<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<double>, new() {
				DoSerializeDictionary(name, value, JsonToProxyDouble<TVal>, ProxyDoubleToJson);
			}

			public void ProxyDouble<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<double>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyDouble<TVal>, ProxyDoubleToJson);
			}

			#endregion

			#region ISerialProxy<float>

			public void ProxySingle<TVal>(string name, ref TVal value) where TVal : ISerialProxy<float>, new() {
				DoSerialize(name, ref value, JsonToProxySingle<TVal>, ProxySingleToJson);
			}

			public void ProxySingle<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<float>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxySingle<TVal>, ProxySingleToJson);
			}
			public void ProxySingle<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<float>, new() {
				DoSerializeDictionary(name, value, JsonToProxySingle<TVal>, ProxySingleToJson);
			}

			public void ProxySingle<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<float>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxySingle<TVal>, ProxySingleToJson);
			}

			#endregion

			#region ISerialProxy<bool>

			public void ProxyBool<TVal>(string name, ref TVal value) where TVal : ISerialProxy<bool>, new() {
				DoSerialize(name, ref value, JsonToProxyBool<TVal>, ProxyBoolToJson);
			}

			public void ProxyBool<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<bool>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyBool<TVal>, ProxyBoolToJson);
			}
			public void ProxyBool<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<bool>, new() {
				DoSerializeDictionary(name, value, JsonToProxyBool<TVal>, ProxyBoolToJson);
			}

			public void ProxyBool<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<bool>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyBool<TVal>, ProxyBoolToJson);
			}

			#endregion

			#region ISerialProxy<int>

			public void ProxyInt32<TVal>(string name, ref TVal value) where TVal : ISerialProxy<int>, new() {
				DoSerialize(name, ref value, JsonToProxyInt32<TVal>, ProxyInt32ToJson);
			}

			public void ProxyInt32<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<int>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyInt32<TVal>, ProxyInt32ToJson);
			}
			public void ProxyInt32<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<int>, new() {
				DoSerializeDictionary(name, value, JsonToProxyInt32<TVal>, ProxyInt32ToJson);
			}

			public void ProxyInt32<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<int>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyInt32<TVal>, ProxyInt32ToJson);
			}

			#endregion

			#region ISerialProxy<short>

			public void ProxyInt16<TVal>(string name, ref TVal value) where TVal : ISerialProxy<short>, new() {
				DoSerialize(name, ref value, JsonToProxyInt16<TVal>, ProxyInt16ToJson);
			}

			public void ProxyInt16<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<short>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyInt16<TVal>, ProxyInt16ToJson);
			}
			public void ProxyInt16<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<short>, new() {
				DoSerializeDictionary(name, value, JsonToProxyInt16<TVal>, ProxyInt16ToJson);
			}

			public void ProxyInt16<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<short>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyInt16<TVal>, ProxyInt16ToJson);
			}

			#endregion

			#region ISerialProxy<long>

			public void ProxyInt64<TVal>(string name, ref TVal value) where TVal : ISerialProxy<long>, new() {
				DoSerialize(name, ref value, JsonToProxyInt64<TVal>, ProxyInt64ToJson);
			}

			public void ProxyInt64<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<long>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyInt64<TVal>, ProxyInt64ToJson);
			}
			public void ProxyInt64<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<long>, new() {
				DoSerializeDictionary(name, value, JsonToProxyInt64<TVal>, ProxyInt64ToJson);
			}

			public void ProxyInt64<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<long>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyInt64<TVal>, ProxyInt64ToJson);
			}

			#endregion

			#region ISerialProxy<uint>

			public void ProxyUInt32<TVal>(string name, ref TVal value) where TVal : ISerialProxy<uint>, new() {
				DoSerialize(name, ref value, JsonToProxyUInt32<TVal>, ProxyUInt32ToJson);
			}

			public void ProxyUInt32<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<uint>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyUInt32<TVal>, ProxyUInt32ToJson);
			}
			public void ProxyUInt32<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<uint>, new() {
				DoSerializeDictionary(name, value, JsonToProxyUInt32<TVal>, ProxyUInt32ToJson);
			}

			public void ProxyUInt32<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<uint>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyUInt32<TVal>, ProxyUInt32ToJson);
			}

			#endregion

			#region ISerialProxy<ushort>

			public void ProxyUInt16<TVal>(string name, ref TVal value) where TVal : ISerialProxy<ushort>, new() {
				DoSerialize(name, ref value, JsonToProxyUInt16<TVal>, ProxyUInt16ToJson);
			}

			public void ProxyUInt16<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<ushort>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyUInt16<TVal>, ProxyUInt16ToJson);
			}
			public void ProxyUInt16<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<ushort>, new() {
				DoSerializeDictionary(name, value, JsonToProxyUInt16<TVal>, ProxyUInt16ToJson);
			}

			public void ProxyUInt16<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<ushort>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyUInt16<TVal>, ProxyUInt16ToJson);
			}

			#endregion

			#region ISerialProxy<ulong>

			public void ProxyUInt64<TVal>(string name, ref TVal value) where TVal : ISerialProxy<ulong>, new() {
				DoSerialize(name, ref value, JsonToProxyUInt64<TVal>, ProxyUInt64ToJson);
			}

			public void ProxyUInt64<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<ulong>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyUInt64<TVal>, ProxyUInt64ToJson);
			}
			public void ProxyUInt64<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<ulong>, new() {
				DoSerializeDictionary(name, value, JsonToProxyUInt64<TVal>, ProxyUInt64ToJson);
			}

			public void ProxyUInt64<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<ulong>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyUInt64<TVal>, ProxyUInt64ToJson);
			}

			#endregion

			#region ISerialProxy<byte>

			public void ProxyByte<TVal>(string name, ref TVal value) where TVal : ISerialProxy<byte>, new() {
				DoSerialize(name, ref value, JsonToProxyByte<TVal>, ProxyByteToJson);
			}

			public void ProxyByte<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<byte>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxyByte<TVal>, ProxyByteToJson);
			}
			public void ProxyByte<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<byte>, new() {
				DoSerializeDictionary(name, value, JsonToProxyByte<TVal>, ProxyByteToJson);
			}

			public void ProxyByte<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<byte>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxyByte<TVal>, ProxyByteToJson);
			}

			#endregion

			#region ISerialProxy<sbyte>

			public void ProxySByte<TVal>(string name, ref TVal value) where TVal : ISerialProxy<sbyte>, new() {
				DoSerialize(name, ref value, JsonToProxySByte<TVal>, ProxySByteToJson);
			}

			public void ProxySByte<TVal, TCol>(string name, TCol value) where TVal : ISerialProxy<sbyte>, new() where TCol : ICollection<TVal> {
				DoSerializeCollection(name, value, JsonToProxySByte<TVal>, ProxySByteToJson);
			}
			public void ProxySByte<TVal>(string name, IDictionary<string, TVal> value) where TVal : ISerialProxy<sbyte>, new() {
				DoSerializeDictionary(name, value, JsonToProxySByte<TVal>, ProxySByteToJson);
			}

			public void ProxySByte<TKey, TVal>(string name, IDictionary<TKey, TVal> value) where TKey : ISerialProxy<string>, new() where TVal : ISerialProxy<sbyte>, new() {
				DoSerializeDictionary<TKey, TVal>(name, value, JsonToProxySByte<TVal>, ProxySByteToJson);
			}

			#endregion

		}
	}
}