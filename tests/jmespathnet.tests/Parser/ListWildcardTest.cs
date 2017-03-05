using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class ListWildcardTest
    {
        [Fact]
        public void ParseProjection()
        {
            ParseProjection_Transform("[*].foo", "[{\"foo\": 1}, {\"foo\": 2}, {\"foo\": 3}]", "[1,2,3]");
            ParseProjection_Transform("foo[*].bar[*].kind", "{\"foo\": [{\"bar\": [{\"kind\": \"basic\"}, {\"kind\": \"intermediate\"}]},{\"bar\": [{\"kind\": \"advanced\"}, {\"kind\": \"expert\"}]},{\"bar\": \"string\"}]}", "[[\"basic\",\"intermediate\"],[\"advanced\",\"expert\"]]");
        }

        private void ParseProjection_Transform(string expression, string input, string expected)
        {
            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal(expected, result);
        }
    }
}