namespace PotatoSerializer {

	public interface ISerialProxy<T> {
		ushort Version { get; }
		T GetSerialProxy();
		void SetSerialProxy(T value, ushort version);
	}

}