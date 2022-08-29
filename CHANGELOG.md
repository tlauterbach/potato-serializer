# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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