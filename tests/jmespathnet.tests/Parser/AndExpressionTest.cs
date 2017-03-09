namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class AndExpressionTest : ParserTestBase
    {
        [Fact]
        public void ParseAndExpression()
        {
            const string json = "{\"one\": 1,\"two\": 2,\"three\": 3,\"emptylist\": [],\"boolvalue\": false}";
            const string expression = "one < two && three > one";
            const string expected = "true";

            Assert(expression, json, expected);
        }
    }
}