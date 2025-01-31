using jmespath.net.tests.Expressions;
using jmespath.net.tests.Parser;
using Newtonsoft.Json.Linq;

namespace jmespath.net.tests.Functions
{
    using FactAttribute = Xunit.FactAttribute;

    public class MergeFunctionTest : ParserTestBase
    {
        [Fact]
        public void MergeOverwriteValue()
        {
            const string json = "{\"bar\":2}";
            const string expression = "merge(@, { bar: `3` } )";
            const string expected = "{\"bar\":3}";

            Assert(expression, json, expected);
        }

        [Fact]
        public void MergeNewValue() {
            const string json = "{\"bar\":2}";
            const string expression = "merge(@, { foo: `3` } )";
            const string expected = "{\"bar\":2,\"foo\":3}";

            Assert(expression, json, expected);
        }

        [Fact]
        public void MergeNewNullValue() {
            const string json = "{\"bar\":2}";
            const string expression = "merge(@, { foo: `null` } )";
            const string expected = "{\"bar\":2,\"foo\":null}";

            Assert(expression, json, expected);
        }

        [Fact]
        public void MergeOverwriteWithNull() {
            const string json = "{\"bar\":2}";
            const string expression = "merge(@, { bar: `null` } )";
            const string expected = "{\"bar\":null}";

            Assert(expression, json, expected);
        }
    }
}