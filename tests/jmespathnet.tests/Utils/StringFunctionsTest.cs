using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Functions;
using Newtonsoft.Json.Linq;
using System.Linq;
using Xunit;

namespace jmespath.net.tests.Utils
{

    public sealed class StringFunctionsTest
    {
        [Theory]
        [InlineData("𝌆", 1, "U+1D306 TETRAGRAM FOR CENTER")]
        [InlineData("😀", 1, "U+1F600 GRINNING FACE")]
        public void Length(string text, int expected, string name)
        {
            var length = new LengthFunction();
            var result = length.Execute(new JmesPathFunctionArgument(JToken.FromObject(text)));

            Assert.Equal(expected, result.Value<int>());
        }

        [Theory]
        [InlineData(new[] { "𝌆", "\xfb06", "\xfb06yle", "\xfb03" }, new[] { "\xfb03", "\xfb06", "\xfb06yle", "𝌆" })]
        public void Sort(string[] strings, string[] expected)
        {
            var sort = new SortFunction();
            var result = (JArray)sort.Execute(new JmesPathFunctionArgument(JArray.FromObject(strings)));
            var actual = result.Select(u => u.Value<string>()).ToArray();

            Assert.True(Enumerable.SequenceEqual(expected, actual));
        }

        [Theory]
        [InlineData(new[] { "𝌆", "\xfb06", "\xfb06yle", "\xfb03" }, new[] { "\xfb03", "\xfb06", "\xfb06yle", "𝌆" })]
        public void SortBy(string[] strings, string[] expected)
        {
            var sortBy = new SortByFunction();

            JToken MakeObject(string text)
                => JToken.Parse($"{{\"foo\": \"{text}\"}}");

            var expectedArray = JArray.FromObject(expected.Select(MakeObject).ToArray());
            var inputArray = JArray.FromObject(strings.Select(MakeObject).ToArray());

            var by = new JmesPathIdentifier("foo");
            JmesPathExpression.MakeExpressionType(by);

            var actualArray = (JArray)sortBy.Execute(
                new JmesPathFunctionArgument(inputArray),
                new JmesPathFunctionArgument(by)
                );

            Assert.True(JToken.DeepEquals(expectedArray, actualArray));
        }

        [Theory]
        [InlineData("a𝌆b", "b𝌆a")]

        public void Reverse(string text, string expected)
        {
            var reverse = new ReverseFunction();
            var argument = new JmesPathFunctionArgument(JToken.FromObject(text));

            var actual = reverse
                .Execute(argument)
                .Value<string>()
                ;

            Assert.Equal(expected, actual);
        }
    }
}