using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PotatoSerializer {

	internal class Lexer {

		private static readonly Dictionary<TokenType, PatternKeyword> m_keywords = new Dictionary<TokenType, PatternKeyword>() {
			{ TokenType.True,  new PatternKeyword("true") },
			{ TokenType.False, new PatternKeyword("false") },
			{ TokenType.Null,  new PatternKeyword("null") },
		};
		private static readonly Dictionary<TokenType, PatternSymbol> m_symbols = new Dictionary<TokenType, PatternSymbol>() {
			{ TokenType.OpenBrace, new PatternSymbol("{") },
			{ TokenType.CloseBrace, new PatternSymbol("}") },
			{ TokenType.OpenSquare, new PatternSymbol("[") },
			{ TokenType.CloseSquare, new PatternSymbol("]") },
			{ TokenType.Colon, new PatternSymbol(":") },
			{ TokenType.Comma, new PatternSymbol(",") }
		};
		private static readonly PatternString m_stringPattern = new PatternString();
		private static readonly PatternNumber m_numberPattern = new PatternNumber();

		private List<Token> m_tokens;

		public Lexer() {
			m_tokens = new List<Token>();
		}

		public IEnumerable<Token> Tokenize(string input, int tabSize = 4) {

			string replaced = input.Replace("\r\n", "\n");

			CharStream stream = new CharStream(replaced, tabSize);
			m_tokens.Clear();

			while (!stream.IsEndOfFile()) {
				// ignore whitespace
				try {
					if (TryIgnoreWhiteSpace(stream)) {
						continue;
					}
					// handle keywords and symbols
					if (TraverseDictionary(stream, m_keywords)) {
						continue;
					}
					if (TraverseDictionary(stream, m_symbols)) {
						continue;
					}
					// handle strings and numbers
					if (TryAddToken(stream, TokenType.String, m_stringPattern)) {
						continue;
					}
					if (TryAddToken(stream, TokenType.Number, m_numberPattern)) {
						continue;
					}
				} catch (Exception e) {
					throw new Exception(string.Format("at {0}: {1}", stream.Position, e.Message));
				}
				
				throw new Exception(string.Format(
					"Unrecognized character `{0}' at {1} in input string", 
					stream.Peek(), stream.Position
				));
			}
			//StringBuilder builder = new StringBuilder();
			foreach (Token token in m_tokens) {
				//builder.Append(token.Value).Append('\n');
				yield return token;
			}
			//Debug.Log(builder.ToString());
			m_tokens.Clear();
		}

		private bool TryIgnoreWhiteSpace(CharStream stream) {
			char c = stream.Peek();
			switch (c) {
				case '\t': stream.Tab(); return true;
				case '\r': stream.LineFeed(); return true;
				case '\n': stream.LineFeed(); return true;
				case ' ': stream.Space(); return true;
				default: return false;
			}
		}

		private bool TraverseDictionary<T>(CharStream stream, IDictionary<TokenType,T> dictionary) where T : IPattern {
			foreach (TokenType type in dictionary.Keys) {
				if (TryAddToken(stream, type, dictionary[type])) {
					return true;
				}
			}
			return false;
		}

		private bool TryAddToken(CharStream stream, TokenType type, IPattern pattern) {
			if (pattern.Matches(stream, out int length)) {
				Token token = new Token(type, stream.Position, stream.SubString(length));
				stream.Advance(length);
				m_tokens.Add(token);
				return true;
			} else {
				return false;
			}
		}

	}

}