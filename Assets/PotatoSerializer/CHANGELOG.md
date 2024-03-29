# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.4.0] 2022-09-15
### Added
- helpers to directly ask type of `JsonNode` without using `IsType`
- changed `IsNull` to property instead of using function
- `Contains` overload that allows to confirm the type of the contained property

## [1.3.1] 2022-09-09
### Fixed
- issue where `ReadToNode` was missing from `Serializer`

## [1.3.0] 2022-09-07
### Changed
- exposed JsonNode so generic blobs can be readable by the user

## [1.2.0] 2022-08-29
### Changed
- removed exceptions on read for null or non-existing fields
- added `IsValid` function to `Serializer`

## [1.1.1] 2022-05-22
### Fixed
- Error handling for de/serializing collections and dictionaries

## [1.1.0] 2022-05-09
### Changed
- `ISerialProxy<T>` implementations no longer require a generic `ICollection` be defined when calling `Proxy{Name}` functions 

## [1.0.0] 2022-05-09
### Added
- Json strings can be deserialized and objects be serialized using the `Serializer` class provided that the input/output object implements `ISerialObject`
- Several serializable types