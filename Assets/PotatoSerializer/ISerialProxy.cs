namespace PotatoSerializer {

	public interface ISerialProxy<T> {
		T GetSerialProxy();
		void SetSerialProxy(T value);
	}

}