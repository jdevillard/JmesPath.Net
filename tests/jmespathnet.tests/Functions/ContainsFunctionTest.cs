using jmespath.net.tests.Expressions;
using jmespath.net.tests.Parser;
using Newtonsoft.Json.Linq;

namespace jmespath.net.tests.Functions
{
    using FactAttribute = Xunit.FactAttribute;

    public class ContainsFunctionTest : ParserTestBase
    {
        [Fact]
        public void StringComparison()
        {
            const string json = "{\"foo\":[\"first\",\"second\"]}";
            const string expression = "contains('abc', 'a')";
            const string expected = "true";

            Assert(expression, json, expected);
        }

        [Fact]
        public void StringInArray()
        {
            const string json = "[\"a\", \"b\", \"c\"]";
            const string expression = "contains(@, 'a')";
            const string expected = "true";

            Assert(expression, json, expected);
        }



    }
}