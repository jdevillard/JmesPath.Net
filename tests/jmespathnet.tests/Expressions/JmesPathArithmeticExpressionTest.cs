using DevLab.JmesPath;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathArithmeticExpressionTest : JmesPathExpressionsTestBase
    {
        [Theory]
        [InlineData("foo + bar", "{\"foo\": 40, \"bar\": 2}", "42.0")]
        [InlineData("foo - bar", "{\"foo\": 44, \"bar\": 2}", "42.0")] // U+002D HYPHEN-MINUS
        [InlineData("foo − bar", "{\"foo\": 44, \"bar\": 2}", "42.0")] // U+2212 MINUS SIGN
        [InlineData("foo * bar", "{\"foo\": 40, \"bar\": 2}", "80.0")]
        [InlineData("foo × bar", "{\"foo\": 40, \"bar\": 2}", "80.0")]
        [InlineData("foo / bar", "{\"foo\": 40, \"bar\": 2}", "20.0")]
        [InlineData("foo ÷ bar", "{\"foo\": 40, \"bar\": 2}", "20.0")]
        [InlineData("foo % bar", "{\"foo\": 40, \"bar\": 3}", "1.0")]
        [InlineData("foo // bar", "{\"foo\": 40, \"bar\": 3}", "13.0")]

        [InlineData("+foo", "{\"foo\": 40, \"bar\": 3}", "40.0")]
        [InlineData("-foo", "{\"foo\": 40, \"bar\": 3}", "-40.0")]
        [InlineData("+foo + +bar", "{\"foo\": 40, \"bar\": 3}", "43.0")]
        [InlineData("+foo + -bar", "{\"foo\": 40, \"bar\": 3}", "37.0")]
        [InlineData("-foo + +bar", "{\"foo\": 40, \"bar\": 3}", "-37.0")]
        [InlineData("-foo + -bar", "{\"foo\": 40, \"bar\": 3}", "-43.0")]

        [InlineData("- `40.0` + - `3.0`", "{\"foo\": 40, \"bar\": 3}", "-43.0")]

        [InlineData("a.b + c.d", "{ \"a\": { \"b\": 1 }, \"c\": { \"d\": 2 } }", "3.0")]
        
        [InlineData("{ ab: a.b, cd: c.d } | ab + cd", "{ \"a\": { \"b\": 1 }, \"c\": { \"d\": 2 } }", "3.0")]
        [InlineData("{ ab: a.b, cd: c.d } | ab + cd × cd", "{ \"a\": { \"b\": 1 }, \"c\": { \"d\": 2 } }", "5.0")]
        [InlineData("{ ab: a.b, cd: c.d } | (ab + cd) × cd", "{ \"a\": { \"b\": 1 }, \"c\": { \"d\": 2 } }", "6.0")]
        public void JmesPathArithmeticExpression_Transform(string expression, string document, string expected)
            => Assert(new JmesPath().Parse(expression), document, expected);
    }
}