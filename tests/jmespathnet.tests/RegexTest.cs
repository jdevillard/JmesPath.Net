using System.Text.RegularExpressions;
using Xunit;

namespace jmespath.net.tests
{
    public sealed class RegexTest
    {
        [Fact]
        public void Regex_UnquotedString()
        {
            /*
             * 
             * unquoted-string   = (%x41-5A / %x61-7A / %x5F) *(  ; a-zA-Z_
             *                         %x30-39  /  ; 0-9
             *                         %x41-5A /  ; A-Z
             *                         %x5F    /  ; _
             *                         %x61-7A)   ; a-z
             */

            // [A-Za-z_][0-9A-Za-z_]*

            const string pattern = "[A-Za-z_][0-9A-Za-z_]*";

            Assert.True(Match("foo_42", pattern));
            Assert.False(Match("42_should_not_start_with_digit", pattern));
            Assert.False(Match("should_not_contain_!_special_chars", pattern));
        }

        [Fact]
        public void Regex_QuotedString()
        {
            /*
             * 
             * quoted-string     = quote 1*(unescaped-char / escaped-char) quote
             * unescaped-char    = %x20-21 / %x23-5B / %x5D-10FFFF
             * escape            = %x5C   ; Back slash: \
             * quote             = %x22   ; Double quote: '"'
             * escaped-char      = escape (
             *                         %x22 /          ; "    quotation mark  U+0022
             *                         %x5C /          ; \    reverse solidus U+005C
             *                         %x2F /          ; /    solidus         U+002F
             *                         %x62 /          ; b    backspace       U+0008
             *                         %x66 /          ; f    form feed       U+000C
             *                         %x6E /          ; n    line feed       U+000A
             *                         %x72 /          ; r    carriage return U+000D
             *                         %x74 /          ; t    tab             U+0009
             *                         %x75 4HEXDIG )  ; uXXXX                U+XXXX             * 
             */

            Regex_JsonString();
        }

        [Fact]
        public void Regex_JsonString()
        {
            /*
             * RFC 7159:
             * 
             * string = quotation-mark *char quotation-mark
             * char = unescaped /
             *     escape (
             *         %x22 /          ; "    quotation mark  U+0022
             *         %x5C /          ; \    reverse solidus U+005C
             *         %x2F /          ; /    solidus         U+002F
             *         %x62 /          ; b    backspace       U+0008
             *         %x66 /          ; f    form feed       U+000C
             *         %x6E /          ; n    line feed       U+000A
             *         %x72 /          ; r    carriage return U+000D
             *         %x74 /          ; t    tab             U+0009
             *         %x75 4HEXDIG )  ; uXXXX                U+XXXX
             * escape = %x5C              ; \
             * quotation-mark = %x22      ; "
             * unescaped = %x20-21 / %x23-5B / %x5D-10FFFF
             * 
             */

            // "[^"\\\b\f\n\r\t]*((\\["\\/bfnrt]|\\u[0-9a-fA-F]{4})+[^"\\\b\f\n\r\t]*)*"

            const string pattern = @"""[^""\\\b\f\n\r\t]*((\\[""\\/bfnrt]|\\u[0-9a-fA-F]{4})+[^""\\\b\f\n\r\t]*)*""";

            Assert.True(Match("\"Hello, world!\"", pattern));
            Assert.True(Match("\"Enclosing \\\"name\\\" in double quotes\"", pattern));
            Assert.True(Match("\"Escaped \\\\ \"", pattern));
            Assert.True(Match("\"Escaped \\b \"", pattern));
            Assert.True(Match("\"Escaped \\f \"", pattern));
            Assert.True(Match("\"Escaped \\n \"", pattern));
            Assert.True(Match("\"Escaped \\r \"", pattern));
            Assert.True(Match("\"Escaped \\t \"", pattern));
            Assert.True(Match("\"Escaped \\u2713 \"", pattern));

            Assert.False(Match("\"	tab	character	in	string	\"", pattern));
            Assert.False(Match("\"Incomplete \\unicode sequence\"", pattern));
        }

        [Fact]
        public void Regex_JsonNumber()
        {

            /*
             * RFC 7159:
             * number = [ minus ] int [ frac ] [ exp ]
             * decimal-point = %x2E       ; .
             * digit1-9 = %x31-39         ; 1-9
             * e = %x65 / %x45            ; e E
             * exp = e [ minus / plus ] 1*DIGIT
             * frac = decimal-point 1*DIGIT
             * int = zero / ( digit1-9 *DIGIT )
             * minus = %x2D               ; -
             * plus = %x2B                ; +
             * zero = %x30                ; 0
             * 
             */

            const string pattern = @"\-?(0|[1-9][0-9]*)(\.[0-9]+)?([eE][\+\-]?[0-9]+)?";

            Assert.True(Match("42", pattern));
            Assert.True(Match("42e12", pattern));
            Assert.True(Match("42e+12", pattern));
            Assert.True(Match("42e-12", pattern));
            Assert.True(Match("42E12", pattern));
            Assert.True(Match("42E+12", pattern));
            Assert.True(Match("42E-12", pattern));

            Assert.True(Match("-42", pattern));
            Assert.True(Match("-42e12", pattern));
            Assert.True(Match("-42e+12", pattern));
            Assert.True(Match("-42e-12", pattern));
            Assert.True(Match("-42E12", pattern));
            Assert.True(Match("-42E+12", pattern));
            Assert.True(Match("-42E-12", pattern));

            Assert.True(Match("42.3", pattern));
            Assert.True(Match("42.3e12", pattern));
            Assert.True(Match("42.3e+12", pattern));
            Assert.True(Match("42.3e-12", pattern));
            Assert.True(Match("42.3E12", pattern));
            Assert.True(Match("42.3E+12", pattern));
            Assert.True(Match("42.3E-12", pattern));

            Assert.True(Match("-42.3", pattern));
            Assert.True(Match("-42.3e12", pattern));
            Assert.True(Match("-42.3e+12", pattern));
            Assert.True(Match("-42.3e-12", pattern));
            Assert.True(Match("-42.3E12", pattern));
            Assert.True(Match("-42.3E+12", pattern));
            Assert.True(Match("-42.3E-12", pattern));

            Assert.False(Match("01.50", pattern));
            Assert.True(Match("1.05e+03", pattern));
        }

        [Fact]
        public void Regex_RawString()
        {
            /*
             * 
             * raw-string        = "'" *raw-string-char "'"
             * raw-string-char   = (%x20-26 / %x28-5B / %x5D-10FFFF) / preserved-escape /
             *                       raw-string-escape
             * preserved-escape  = escape (%x20-26 / %28-5B / %x5D-10FFFF)
             * raw-string-escape = escape ("'" / escape)           * 
             * escape            = %x5C   ; Back slash: \
             */

            // '(\\?[^'\\])*((\\['\\])+(\\?[^'\\])*)*'

            const string pattern = @"'(\\?[^'\\])*((\\['\\])+(\\?[^'\\])*)*'";

            Assert.True(Match("'abcd'", pattern));
            Assert.True(Match("'\\a\\b\\c'", pattern));
            Assert.True(Match("'\\'Hello\\''", pattern));
        }

        [Fact]
        public void Regex_JsonLiteral_String()
        {
            /*
             *  ; The ``json-value`` is any valid JSON value with the one exception that the
                ; ``%x60`` character must be escaped.  While it's encouraged that implementations
                ; use any existing JSON parser for this grammar rule (after handling the escaped
                ; literal characters), the grammar rule is shown below for completeness::

            */

            // `[^`]*((\\`)*[^`]*)*`

            const string pattern = @"`[^`]*((\\`)*[^`]*)*`";

            Assert.True(Match("`abcd`", pattern));
            Assert.True(Match("`\\a\\b\\c`", pattern));
            Assert.True(Match("`\\`Hello\\``", pattern));
            Assert.False(Match("`\\`Hel`lo\\``", pattern));
        }
        private static bool Match(string input, string pattern)
        {
            var regex = new Regex("^" + pattern + "$", RegexOptions.Singleline);
            var match = regex.Match(input);

            return match.Success;
        }
    }
}