using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class ParserTestBase
    {
        protected void Assert(string expression, string input, string expected)
        {
            var path = new JmesPath();
            RegisterFunction(path);

            var result = path.Transform(input, expression);
            Xunit.Assert.Equal(expected, result);
        }

        protected virtual void RegisterFunction(JmesPath parser)
        {
            
        }
    }
}