using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class ParserTestBase
    {
        protected void Assert(string expression, string input, string expected)
        {
            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Xunit.Assert.Equal(expected, result);
        }
    }
}