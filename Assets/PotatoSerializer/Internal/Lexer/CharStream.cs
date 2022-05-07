using System;

namespace PotatoSerializer {

	internal class CharStream {

		public int Index { get; private set; }

		public FilePosition Position { get; private set; }

		private readonly string m_input;
		private readonly int m_tabSize;


		public CharStream(string input, int tabSize = 4) {
			Position = new FilePosition(1, 1);
			Index = 0;
			m_input = input;
			m_tabSize = tabSize;
		}

		public char Peek() {
			return m_input[Index];
		}
		public char Peek(int distance) {
			if (IsEndOfFile(distance)) {
				throw new IndexOutOfRangeException();
			}
			return m_input[Index + distance];
		}
		public SubString SubString(int length) {
			return new SubString(m_input, Index, length);
		}
		public void Tab() {
			Position = Position.Tab(m_tabSize);
		}
		public void Space() {
			Position = Position.Space();
		}
		public void LineFeed() {
			Position = Position.LineFeed();
		}
		public void Advance(int distance = 1) {
			Index += distance;
			Position = Position.Advance(distance);
		}

		public bool IsEndOfFile(int distance = 0) {
			return Index + distance >= m_input.Length;
		}

	}

}