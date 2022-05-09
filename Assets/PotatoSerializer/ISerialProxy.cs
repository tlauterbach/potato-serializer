namespace PotatoSerializer {

	public interface ISerialProxy {
	}

	public interface ISerialProxy<T> : ISerialProxy {
		T GetSerialProxy();
		void SetSerialProxy(T value);
	}

}