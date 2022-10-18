using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;
using Xunit;

namespace jmespath.net.tests.Utils
{
    public sealed class TextTest
    {
        [Theory]
        [InlineData("𝌆", 1, "U+1D306 TETRAGRAM FOR CENTER")]
        [InlineData("😀", 1, "U+1F600 GRINNING FACE")]
        public void Length(string text, int expected, string name)
        {
            var t = new Text(text);
            Assert.Equal(expected, t.Length);
        }
    }

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
    }
}