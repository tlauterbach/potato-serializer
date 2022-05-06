namespace PotatoSerializer {

	public sealed partial class Serializer : ISerializer {

		/// <summary>
		/// Version number of the object that is currently
		/// being read or written
		/// </summary>
		public ushort Version { get; private set; }


	}

}