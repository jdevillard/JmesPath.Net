using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathExpressionsTestBase
    {
        protected virtual void Assert(JmesPathExpression expression, string input, string expected)
        {
            var json = JToken.Parse(input);
            var result = expression.Transform(json);
            var actual = result.AsJToken().AsString();

            Xunit.Assert.Equal(expected, actual);
        }
    }
}