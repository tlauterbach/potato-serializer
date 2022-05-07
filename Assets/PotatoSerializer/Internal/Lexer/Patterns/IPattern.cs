namespace PotatoSerializer {

	internal interface IPattern {
		bool Matches(CharStream stream, out int length);
	}

}