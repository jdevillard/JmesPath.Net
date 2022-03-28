using jmespath.net.tests.Parser;

namespace jmespath.net.tests.Functions
{
    using FactAttribute = Xunit.FactAttribute;

    public class LetFunctionTest : ParserTestBase
    {
        [Fact]
        public void JmesPathLetFunction_CurrentContext()
        {
            // search(let({a: `x`}, &a, {"b": "y"})) -> "x"

            const string json = "{\"b\": \"y\"}";
            const string expression = "let({a: `\"x\"`}, &b)";
            const string expected = "\"y\"";

            Assert(expression, json, expected);
        }
        [Fact]
        public void JmesPathLetFunction_Scope()
        {
            // search(let({a: `x`}, &a, {"b": "y"})) -> "x"

            const string json = "{\"b\": \"y\"}";
            const string expression = "let({a: `\"x\"`}, &a)";
            const string expected = "\"x\"";

            Assert(expression, json, expected);
        }
        [Fact]
        public void JmesPathLetFunction_NestedScopes()
        {
            // search(let({a: `x`}, &let({b: `y`}, &{a: a, b: b, c: c})), {"c": "z"})
            //      -> {"a": "x", "b": "y", "c": "z"}

            const string json = "{\"c\": \"z\"}";
            const string expression = "let({a: `\"x\"`}, &let({b: `\"y\"`}, &{a: a, b: b, c: c}))";
            const string expected = "{\"a\":\"x\",\"b\":\"y\",\"c\":\"z\"}";

            Assert(expression, json, expected);
        }
    }
}