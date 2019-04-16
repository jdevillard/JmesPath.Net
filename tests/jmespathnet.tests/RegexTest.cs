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

            Assert.Equal(true, Match("foo_42", pattern));
            Assert.Equal(false, Match("42_should_not_start_with_digit", pattern));
            Assert.Equal(false, Match("should_not_contain_!_special_chars", pattern));
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

            Assert.Equal(true, Match("\"Hello, world!\"", pattern));
            Assert.Equal(true, Match("\"Enclosing \\\"name\\\" in double quotes\"", pattern));
            Assert.Equal(true, Match("\"Escaped \\\\ \"", pattern));
            Assert.Equal(true, Match("\"Escaped \\b \"", pattern));
            Assert.Equal(true, Match("\"Escaped \\f \"", pattern));
            Assert.Equal(true, Match("\"Escaped \\n \"", pattern));
            Assert.Equal(true, Match("\"Escaped \\r \"", pattern));
            Assert.Equal(true, Match("\"Escaped \\t \"", pattern));
            Assert.Equal(true, Match("\"Escaped \\u2713 \"", pattern));

            Assert.Equal(false, Match("\"	tab	character	in	string	\"", pattern));
            Assert.Equal(false, Match("\"Incomplete \\unicode sequence\"", pattern));
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

            Assert.Equal(true, Match("42", pattern));
            Assert.Equal(true, Match("42e12", pattern));
            Assert.Equal(true, Match("42e+12", pattern));
            Assert.Equal(true, Match("42e-12", pattern));
            Assert.Equal(true, Match("42E12", pattern));
            Assert.Equal(true, Match("42E+12", pattern));
            Assert.Equal(true, Match("42E-12", pattern));

            Assert.Equal(true, Match("-42", pattern));
            Assert.Equal(true, Match("-42e12", pattern));
            Assert.Equal(true, Match("-42e+12", pattern));
            Assert.Equal(true, Match("-42e-12", pattern));
            Assert.Equal(true, Match("-42E12", pattern));
            Assert.Equal(true, Match("-42E+12", pattern));
            Assert.Equal(true, Match("-42E-12", pattern));

            Assert.Equal(true, Match("42.3", pattern));
            Assert.Equal(true, Match("42.3e12", pattern));
            Assert.Equal(true, Match("42.3e+12", pattern));
            Assert.Equal(true, Match("42.3e-12", pattern));
            Assert.Equal(true, Match("42.3E12", pattern));
            Assert.Equal(true, Match("42.3E+12", pattern));
            Assert.Equal(true, Match("42.3E-12", pattern));

            Assert.Equal(true, Match("-42.3", pattern));
            Assert.Equal(true, Match("-42.3e12", pattern));
            Assert.Equal(true, Match("-42.3e+12", pattern));
            Assert.Equal(true, Match("-42.3e-12", pattern));
            Assert.Equal(true, Match("-42.3E12", pattern));
            Assert.Equal(true, Match("-42.3E+12", pattern));
            Assert.Equal(true, Match("-42.3E-12", pattern));

            Assert.Equal(false, Match("01.50", pattern));
            Assert.Equal(true, Match("1.05e+03", pattern));
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