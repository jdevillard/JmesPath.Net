// Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

namespace StarodubOleg.GPPG.Runtime
{
	/// <summary>
	/// This is the default class that carries location
	/// information from the scanner to the parser.
	/// If you don't declare "%YYLTYPE Foo" the parser
	/// will expect to deal with this type.
	/// </summary>
	internal class LexLocation : IMerge<LexLocation>
	{
		private int startLine;   // start line
		private int startColumn; // start column
		private int endLine;     // end line
		private int endColumn;   // end column

		/// <summary>
		/// The line at which the text span starts.
		/// </summary>
		public int StartLine { get { return startLine; } }

		/// <summary>
		/// The column at which the text span starts.
		/// </summary>
		public int StartColumn { get { return startColumn; } }

		/// <summary>
		/// The line on which the text span ends.
		/// </summary>
		public int EndLine { get { return endLine; } }

		/// <summary>
		/// The column of the first character
		/// beyond the end of the text span.
		/// </summary>
		public int EndColumn { get { return endColumn; } }

		/// <summary>
		/// Default no-arg constructor.
		/// </summary>
		public LexLocation() { }

		/// <summary>
		/// Constructor for text-span with given start and end.
		/// </summary>
		/// <param name="sl">start line</param>
		/// <param name="sc">start column</param>
		/// <param name="el">end line </param>
		/// <param name="ec">end column</param>
		public LexLocation(int sl, int sc, int el, int ec) { startLine = sl; startColumn = sc; endLine = el; endColumn = ec; }

		/// <summary>
		/// Create a text location which spans from the 
		/// start of "this" to the end of the argument "last"
		/// </summary>
		/// <param name="last">The last location in the result span</param>
		/// <returns>The merged span</returns>
		public LexLocation Merge(LexLocation last) { return new LexLocation(this.startLine, this.startColumn, last.endLine, last.endColumn); }
	}
}
