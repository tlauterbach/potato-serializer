namespace PotatoSerializer {

	public interface ISerialObject {
		ushort Version { get; }
		void Serialize(Serializer serializer);
	}

}