using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathCurrentNodeExpressionTest
    {
        [Fact]
        public void JmesPathCurrentNodeExpression_ToString()
        {
            var current = new JmesPathCurrentNodeExpression();
            var actual = current.ToString();

            Xunit.Assert.Equal("@", actual);
        }
    }
}