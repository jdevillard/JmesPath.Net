
namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class SliceExpressionTest : ParserTestBase
    {
        [Fact]
        public void ParseSliceExpression_IndexExpression()
        {
            const string json = "{\"foo\": [0, 1, 2, 3]}";
            const string expression = "foo[0:4:1]";
            const string expected = "[0,1,2,3]";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ParseSliceExpression_BracketSpecifier()
        {
            const string json = "[0, 1, 2, 3]";
            const string expression = "[0:4:1]";
            const string expected= "[0,1,2,3]";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ParseSliceExpression_BracketSpecifier_Empty()
        {
            const string json = "[0, 1, 2, 3]";
            const string expression = "[]";
            const string expected = "[0,1,2,3]";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ParseSliceExpression_Compliance()
        {
            Assert("foo[:10:]", "{\"foo\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],\"bar\": {\"baz\": 1}}", "[0,1,2,3,4,5,6,7,8,9]");
            Assert("foo[1:9]", "{\"foo\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],\"bar\": {\"baz\": 1}}", "[1,2,3,4,5,6,7,8]");
            Assert("foo[::1]", "{\"foo\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],\"bar\": {\"baz\": 1}}", "[0,1,2,3,4,5,6,7,8,9]");
            Assert("foo[10:0:-1]", "{\"foo\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],\"bar\": {\"baz\": 1}}", "[9,8,7,6,5,4,3,2,1]");
            Assert("foo[10:5:-1]", "{\"foo\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],\"bar\": {\"baz\": 1}}", "[9,8,7,6]");
            Assert("foo[:2].a", "{\"foo\": [{\"a\": 1}, {\"a\": 2}, {\"a\": 3}],\"bar\": [{\"a\": {\"b\": 1}}, {\"a\": {\"b\": 2}},{\"a\": {\"b\": 3}}],\"baz\": 50}", "[1,2]");
        }
    }
}