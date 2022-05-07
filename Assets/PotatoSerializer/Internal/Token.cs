using System;

namespace PotatoSerializer {

	internal struct Token : IEquatable<TokenType> {
		
		public TokenType Type { get; }
		public FilePosition Position { get; }
		public SubString Value { get; }

		public Token(TokenType type, FilePosition position, SubString value) {
			Type = type;
			Value = value;
			Position = position;
		}

		public static bool operator ==(Token lhs, TokenType type) {
			return lhs.Equals(type);
		}
		public static bool operator !=(Token lhs, TokenType type) {
			return !lhs.Equals(type);
		}

		public bool Equals(TokenType other) {
			return Type == other;
		}
		public override bool Equals(object obj) {
			if (obj is TokenType type) {
				return Equals(type);
			} else {
				return false;
			}
		}

		public override int GetHashCode() {
			int hashCode = 1265339359;
			hashCode = hashCode * -1521134295 + Type.GetHashCode();
			hashCode = hashCode * -1521134295 + Value.GetHashCode();
			return hashCode;
		}
	}

}