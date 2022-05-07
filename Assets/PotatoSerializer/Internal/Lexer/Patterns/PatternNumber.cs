namespace PotatoSerializer {

	internal class PatternNumber : IPattern {

		private const char CHAR_DASH = '-';
		private const char CHAR_ZERO = '0';
		private const char CHAR_DOT = '.';
		private const string DIGIT = "0123456789";
		private const string EXPONENT = "Ee";
		private const string SIGN = "+-";

		public bool Matches(CharStream stream, out int length) {
			char current = stream.Peek();
			if (!IsValidStartChar(current)) {
				length = 0;
				return false;
			}
			length = 1;
			// NUMBER SECTION
			if (current == CHAR_DASH) { 
				MoveNext(stream, ref current, ref length);
			}
			if (current == CHAR_ZERO) {
				MoveNext(stream, ref current, ref length);
			} else if (IsDigit(current)) {
				MoveNext(stream, ref current, ref length);
				while (!stream.IsEndOfFile(length) && IsDigit(current)) {
					MoveNext(stream, ref current, ref length);
				}
			} else {
				throw new System.Exception(string.Format(
					"Invalid character `{0}' in json number. " +
					"Character after a `-' must be a digit.",
					current
				));
			}

			// FRACTION SECTION
			if (current == CHAR_DOT) {
				MoveNext(stream, ref current, ref length);
				if (!IsDigit(current)) {
					throw new System.Exception(string.Format(
						"Invalid character `{0}' in json number. " +
						"Character after a `.' must be a digit."
					));
				}
				while (!stream.IsEndOfFile(length) && IsDigit(current)) {
					MoveNext(stream, ref current, ref length);
				}
			}

			// EXPONENT SECTION
			if (IsExponent(current)) {
				MoveNext(stream, ref current, ref length);
				if (IsSign(current)) {
					MoveNext(stream, ref current, ref length);
				}
				if (!IsDigit(current)) {
					throw new System.Exception(string.Format(
						"Invalid character `{0}' in json number. " +
						"Character after an exponent must be a digit."
					));
				}
				while (!stream.IsEndOfFile(length) && IsDigit(current)) {
					MoveNext(stream, ref current, ref length);
				}
			}
			return true; 
		}
		private void MoveNext(CharStream stream, ref char current, ref int length) {
			length++;
			current = stream.Peek(length);
		}

		private bool IsValidStartChar(char c) {
			return c == CHAR_DASH || IsDigit(c);
		}
		private bool IsDigit(char c) {
			for (int ix = 0; ix < DIGIT.Length; ix++) {
				if (c == DIGIT[ix]) {
					return true;
				}
			}
			return false;
		}
		private bool IsExponent(char c) {
			for (int ix = 0; ix < EXPONENT.Length; ix++) {
				if (c == EXPONENT[ix]) {
					return true;
				}
			}
			return false;
		}
		private bool IsSign(char c) {
			for (int ix = 0; ix < SIGN.Length; ix++) {
				if (c == SIGN[ix]) {
					return true;
				}
			}
			return false;
		}
	}

}