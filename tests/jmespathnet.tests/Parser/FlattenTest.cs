namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class FlattenTest : ParserTestBase
    {
        [Fact]
        public void ParseProjection()
        {
            const string json = "{\"foo\": [[0, 1], 2, [3], 4, [5, [6, 7]]] }";
            const string expression = "foo[]";
            const string expected = "[0,1,2,3,4,5,[6,7]]";

            Assert(expression, json, expected);
        }
    }
}