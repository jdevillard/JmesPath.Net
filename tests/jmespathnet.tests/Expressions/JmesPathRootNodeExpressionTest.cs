using DevLab.JmesPath;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathRootNodeExpressionTest : JmesPathExpressionsTestBase
    {

        [Theory]
        [InlineData(
            "states[?name == $.first_choice].cities[]",
            "{\"first_choice\": \"WA\", \"states\": [{\"name\": \"WA\", \"cities\": [\"Seattle\", \"Bellevue\", \"Olympia\"]}, {\"name\": \"CA\", \"cities\": [\"Los Angeles\", \"San Francisco\"]}, {\"name\": \"NY\", \"cities\": [\"New York City\", \"Albany\"]}]}",
            "[\"Seattle\",\"Bellevue\",\"Olympia\"]"
        )]
        public void JmesPathRootNodeExpression_Evaluate(string expression, string document, string expected)
            => Assert(new JmesPath().Parse(expression), document, expected);
    }
}