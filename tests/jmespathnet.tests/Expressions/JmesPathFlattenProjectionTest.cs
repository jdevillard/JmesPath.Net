using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathFlattenProjectionTest
    {
        [Fact]
        public void JmesPathFlattenProjection_ToString()
        { 
            var flatten = new JmesPathFlattenProjection();
            var actual = flatten.ToString();

            Xunit.Assert.Equal("[]", actual);
        }
    }
}