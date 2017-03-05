using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class HashWildcardTest
    {
        [Fact]
        public void ParseProjection()
        {
            ParseProjection_Transform("*.foo", "{\"a\": {\"foo\": 1}, \"b\": {\"foo\": 2}, \"c\": {\"bar\": 1}}", "[1,2]");
        }

        [Fact]
        public void ParseProjection_Nested_SubExpressions()
        {
            ParseProjection_Transform("obj.*.foo[0]", "{\"obj\": {\"a\": {\"foo\": 1}, \"b\": {\"foo\": 2}, \"c\": {\"bar\": 1}}}", "[]");
            ParseProjection_Transform("foo.*.*.*", "{\"foo\": {\"first-1\": {\"second-1\": \"val\"},\"first-2\": {\"second-1\": \"val\"},\"first-3\": {\"second-1\": \"val\"}}}", "[[],[],[]]");
        }

        [Fact]
        public void ParseProjection_Null()
        {
            ParseProjection_Transform("nullvalue.*", "{\"nullvalue\": null}", "null");
        }

        private void ParseProjection_Transform(string expression, string json, string expected)
        {
            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal(expected, result);
        }
    }
}