# potato-serializer
A JSON Serializer/Deserializer for Unity/C#

| Package Name | Package Version | Unity Version |
|-----|-----|-----|
| com.potatointeractive.serializer | 1.3.0 | 2019.4.x |

[Changelog](CHANGELOG.md)

# OpenUPM
This project is available as an Open UPM Package: [![openupm](https://img.shields.io/npm/v/com.potatointeractive.serializer?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.potatointeractive.util/)

Visit [Open UPM](https://openupm.com) to learn more about the Open Unity Package Manager project and how to install the package in your Unity Project.

# Supported Types
This is a list of types and whether they have support for serialization and deserialization. Any of the types listed also support `IList`, `ISet`, and `IDictionary<string>` collections that contain them.
	
| Primitive Type | Is Supported | Proxy Support |
|-----|-----|-----|
| enum | ✓ |  | 
| string | ✓ | ✓ |
| char |   |   |
| decimal |   |   |
| double | ✓ | ✓ |
| float | ✓ | ✓ |
| int | ✓ | ✓ |
| short | ✓ | ✓ |
| long | ✓ | ✓ |
| uint | ✓ | ✓ |
| ushort | ✓ | ✓ |
| ulong | ✓ | ✓ |
| byte | ✓ | ✓ |
| sbyte | ✓ | ✓ |

| Unity Struct | Is Supported | Proxy Support |
|-----|-----|-----|
| Vector2 | ✓ |   |
| Vector2Int | ✓ |   |
| Vector3 | ✓ |   |
| Vector3Int | ✓ |   |
| Vector4 |   |   |
| Vector4Int |   |   |
| Quaternion | ✓ |   |
| Color | ✓ |   |
| Color32 |   |   |
| Rect | ✓ |   |
| RectInt | ✓ |   |
| Bounds |   |   |
| BoundsInt |   |   |