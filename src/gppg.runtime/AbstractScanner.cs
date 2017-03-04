// Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

using System.Diagnostics.CodeAnalysis;

namespace StarodubOleg.GPPG.Runtime
{
	/// <summary>
	/// Abstract scanner class that GPPG expects its scanners to 
	/// extend.
	/// </summary>
	/// <typeparam name="TValue">Semantic value type YYSTYPE</typeparam>
	/// <typeparam name="TSpan">Source location type YYLTYPE</typeparam>
	internal abstract class AbstractScanner<TValue, TSpan> where TSpan : IMerge<TSpan>
	{
		/// <summary>
		/// Lexical value optionally set by the scanner. The value
		/// is of the %YYSTYPE type declared in the parser spec.
		/// </summary>
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "yylval")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "yylval")]
		// Reason for FxCop message suppression -
		// This is a traditional name for YACC-like functionality
		// A field must be declared for this value of parametric type,
		// since it may be instantiated by a value struct.  If it were 
		// implemented as a property, machine generated code in derived
		// types would not be able to select on the returned value.
#pragma warning disable 649
		public TValue yylval;                     // Lexical value: set by scanner
#pragma warning restore 649

		/// <summary>
		/// Current scanner location property. The value is of the
		/// type declared by %YYLTYPE in the parser specification.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "yylloc")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "yylloc")]
		// Reason for FxCop message suppression -
		// This is a traditional name for YACC-like functionality
		public virtual TSpan yylloc
		{
			get { return default(TSpan); }       // Empty implementation allowing
			set { /* skip */ }                   // yylloc to be ignored entirely.
		}

		/// <summary>
		/// Main call point for LEX-like scanners.  Returns an int
		/// corresponding to the token recognized by the scanner.
		/// </summary>
		/// <returns>An int corresponding to the token</returns>
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "yylex")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "yylex")]
		// Reason for FxCop message suppression -
		// This is a traditional name for YACC-like functionality
		public abstract int yylex();

		/// <summary>
		/// Traditional error reporting provided by LEX-like scanners
		/// to their YACC-like clients.
		/// </summary>
		/// <param name="format">Message format string</param>
		/// <param name="args">Optional array of args</param>
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "yyerror")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "yyerror")]
		// Reason for FxCop message suppression -
		// This is a traditional name for YACC-like functionality
		public virtual void yyerror(string format, params object[] args) { }
	}

}
