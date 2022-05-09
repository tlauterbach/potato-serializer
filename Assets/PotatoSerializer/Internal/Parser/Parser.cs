using System;
using System.Collections.Generic;

namespace PotatoSerializer {

	internal class Parser {

		private class ParseException : Exception {
			public ParseException(FilePosition position, string text) : base(
				string.Format("pos{0}: {1}", position, text
			)) { }
		}

		public JsonNode Parse(IEnumerable<Token> tokens) {
			
			TokenStream stream = new TokenStream(tokens);
			JsonNode root;
			if (stream.IsEndOfFile()) {
				root = null;
			} else if (stream.Peek(TokenType.OpenBrace)) {
				root = ParseObject(stream);
			} else if (stream.Peek(TokenType.OpenSquare)) {
				root = ParseArray(stream);
			} else {
				Token token = stream.Peek();
				throw new ParseException(token.Position,
					"Root Json value must be either " +
					"an Object or Array"
				);
			}
			return root;
		}

		private JsonNode ParseObject(TokenStream stream) {
			stream.Expect(TokenType.OpenBrace);
			JsonNode obj = new JsonNode(JsonType.Object);
			bool isFirst = true;
			while (!stream.IsEndOfFile() && !stream.Peek(TokenType.CloseBrace)) {
				if (isFirst) {
					isFirst = false;
				} else {
					stream.Expect(TokenType.Comma);
				}
				string name = RemoveDoubleQuotes(stream.Peek());
				stream.Expect(TokenType.String);
				stream.Expect(TokenType.Colon);
				obj.Add(name, ParseValue(stream));
			}
			stream.Expect(TokenType.CloseBrace);
			return obj;
		}

		private JsonNode ParseArray(TokenStream stream) {
			stream.Expect(TokenType.OpenSquare);
			JsonNode array = new JsonNode(JsonType.Array);
			bool isFirst = true;
			while (!stream.IsEndOfFile() && !stream.Peek(TokenType.CloseSquare)) {
				if (isFirst) {
					isFirst = false;
				} else {
					stream.Expect(TokenType.Comma);
				}
				array.Add(ParseValue(stream));
			}
			stream.Expect(TokenType.CloseSquare);
			return array;
		}

		private JsonNode ParseValue(TokenStream stream) {
			Token peek = stream.Peek();
			if (stream.Peek(TokenType.Number)) {
				if (double.TryParse(peek.Value.ToString(), out double result)) {
					stream.Advance();
					return new JsonNode(result);
				} else {
					throw new ParseException(peek.Position, "Malformed number");
				}
			} else if (stream.Peek(TokenType.String)) {
				stream.Advance();
				return new JsonNode(RemoveDoubleQuotes(peek));
			} else if (stream.Peek(TokenType.True)) {
				stream.Advance();
				return new JsonNode(true);
			} else if (stream.Peek(TokenType.False)) {
				stream.Advance();
				return new JsonNode(false);
			} else if (stream.Peek(TokenType.Null)) {
				stream.Advance();
				return new JsonNode(JsonType.Null);
			} else if (stream.Peek(TokenType.OpenBrace)) {
				return ParseObject(stream);
			} else if (stream.Peek(TokenType.OpenSquare)) {
				return ParseArray(stream);
			}  else {
				throw new ParseException(peek.Position, string.Format(
					"Unexpected token `{0}'", peek.Value
				));
			}
		}

		private string RemoveDoubleQuotes(Token token) {
			string value = token.Value.ToString();
			value = value.Substring(1, value.Length - 2);
			return value;
		}

	}

}