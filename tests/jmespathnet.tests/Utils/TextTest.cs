using DevLab.JmesPath.Utils;
using System.Collections.Generic;
using System.Linq;
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

        [Theory]
        [InlineData("a😀b", new[] { "a", "😀", "b" })]
        public void AsEnumerable(string text, string[] array)
        {
            var t = new Text(text);

            var collection = new List<string>();
            foreach (var uc in t)
                collection.Add(uc);

            Assert.True(Enumerable.SequenceEqual(collection, array));
        }

        [Theory]
        [InlineData("a😀b", new[] { 0x61, 0x1f600, 0x62 })]
        [InlineData("élément", new[] { 0xe9, 0x6c, 0xe9, 0x6d, 0x65, 0x6e, 0x74 })]
        [InlineData("e\x0301lément", new[] { 0x65, 0x0301, 0x6c, 0xe9, 0x6d, 0x65, 0x6e, 0x74 })]
        public void CodePoints(string text, int[] array)
        {
            var t = new Text(text);
            var codePoints = t.CodePoints.ToArray();
            Assert.True(Enumerable.SequenceEqual(codePoints, array));
        }

        [Theory]
        [InlineData(new[] { 0x61, 0x1f600, 0x62 }, "a😀b")]
        [InlineData(new[] { 0xe9, 0x6c, 0xe9, 0x6d, 0x65, 0x6e, 0x74 }, "élément")]
        [InlineData(new[] { 0x65, 0x0301, 0x6c, 0xe9, 0x6d, 0x65, 0x6e, 0x74 }, "e\x0301lément")]
        public void FromCodePoints(int[] array, string expected)
            => Assert.Equal(expected, new Text(array));

        [Theory]
        [InlineData(new[] { "less than", "less than or equal"}, -1)]
        [InlineData(new[] { "identical", "identical"}, 0)]
        [InlineData(new[] { "greater than", "greater"}, 1)]
        public void Compare(string[] texts, int expected)
            => Assert.Equal(expected, new Text(texts[0]).CompareTo(new Text(texts[1])));
    }
}