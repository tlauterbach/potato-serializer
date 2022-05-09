namespace PotatoSerializer {

	internal class PatternString : IPattern {

		private const char CHAR_QUOTE = '"';
		private const char CHAR_ESCAPE = '\\';

		public bool Matches(CharStream stream, out int length) {
			char first = stream.Peek();
			if (first != CHAR_QUOTE) {
				length = 0;
				return false;
			}
			length = 1;
			while (!stream.IsEndOfFile(length)) {
				char prev = stream.Peek(length - 1);
				char current = stream.Peek(length);
				if (current == CHAR_QUOTE && prev != CHAR_ESCAPE) {
					length++;
					return true;
				}
				length++;
			}
			throw new System.Exception(string.Format(
				"String was not properly closed with a double quote character"
			));
		}

	}

}