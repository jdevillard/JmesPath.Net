using DevLab.JmesPath;
using DevLab.JmesPath.Expressions;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathFlattenProjectionTest : JmesPathExpressionsTestBase
    {
        [Fact]
        public void JmesPathFlattenProjection_ToString()
        { 
            var flatten = new JmesPathFlattenProjection();
            var actual = flatten.ToString();

            Xunit.Assert.Equal("[]", actual);
        }

        [Theory]
        [InlineData("foo[].bar[]", "{\"foo\":[{\"bar\":[{\"qux\":2,\"baz\":1},{\"qux\":4,\"baz\":3}]},{\"bar\":[{\"qux\":6,\"baz\":5},{\"qux\":8,\"baz\":7}]}]}", "[{\"qux\":2,\"baz\":1},{\"qux\":4,\"baz\":3},{\"qux\":6,\"baz\":5},{\"qux\":8,\"baz\":7}]")]
        [InlineData("*.*.foo[]", "{\"top1\":{\"sub1\":{\"foo\":\"one\"}},\"top2\":{\"sub1\":{\"foo\":\"one\"}}}", "[\"one\",\"one\"]")]
        public void JmesPathFlattenProjection_Evaluate(string expression, string document, string expected)
            => Assert(new JmesPath().Parse(expression), document, expected);
    }
}