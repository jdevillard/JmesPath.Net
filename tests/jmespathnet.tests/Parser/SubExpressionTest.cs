
namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class SubExpressionTest : ParserTestBase
    {
        [Fact]
        public void ParseSubExpression_Identifier()
        {
            const string input = "{\"foo\": {\"bar\": \"baz\"}}";
            const string expression = "foo.bar";
            const string expected = "\"baz\"";

            Assert(expression, input, expected);
        }

        [Fact]
        public void ParseSubExpression_Wildcard()
        {
            const string input = "{\"foo\": {\"a\": 1, \"b\": 2}}";
            const string expression = "foo.*";
            const string expected = "[1,2]";

            Assert(expression, input, expected);
        }

        [Fact]
        public void ParseSubExpression_Dotless()
        {
            Assert("*.[foo, `2`]", "{\"prop\": {\"foo\": 1}}", "[[1,2]]");
            Assert("[?age > `20`][name, age]", "[{\"age\": 20,\"name\": \"Bob\",\"other\": \"foo\"},{\"age\": 25,\"name\": \"Fred\",\"other\": \"bar\"},{\"age\": 30,\"name\": \"George\",\"other\": \"baz\"}]", "[[\"Fred\",25],[\"George\",30]]");
            Assert("[*][@.age]", "[{\"age\": 20,\"name\": \"Bob\",\"other\": \"foo\"},{\"age\": 25,\"name\": \"Fred\",\"other\": \"bar\"},{\"age\": 30,\"name\": \"George\",\"other\": \"baz\"}]", "[[20],[25],[30]]");
        }
    }
}