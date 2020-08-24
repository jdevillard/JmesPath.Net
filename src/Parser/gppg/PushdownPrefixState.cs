// Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

namespace StarodubOleg.GPPG.Runtime
{
	/// <summary>
	/// Stack utility for the shift-reduce parser.
	/// GPPG parsers have three instances:
	/// (1) The parser state stack, T = State,
	/// (2) The semantic value stack, T = TValue,
	/// (3) The location stack, T = TSpan.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal class PushdownPrefixState<T>
	{
		//  Note that we cannot use the BCL Stack<T> class
		//  here as derived types need to index into stacks.
		//
		private T[] array = new T[8];
		private int tos = 0;

		/// <summary>
		/// Indexer for values of the stack below the top.
		/// </summary>
		/// <param name="index">index of the element, starting from the bottom</param>
		/// <returns>the selected element</returns>
		public T this[int index] { get { return array[index]; } }

		/// <summary>
		/// The current depth of the stack.
		/// </summary>
		public int Depth { get { return tos; } }

		internal void Push(T value)
		{
			if (tos >= array.Length)
			{
				T[] newarray = new T[array.Length * 2];
				System.Array.Copy(array, newarray, tos);
				array = newarray;
			}
			array[tos++] = value;
		}

		internal T Pop()
		{
			T rslt = array[--tos];
			array[tos] = default(T);
			return rslt;
		}

		internal T TopElement() { return array[tos - 1]; }

		internal bool IsEmpty() { return tos == 0; }
	}
}
