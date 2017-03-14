using jmespath.net.tests.Expressions;
using jmespath.net.tests.Parser;
using Newtonsoft.Json.Linq;

namespace jmespath.net.tests.Functions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathCurrentNodeTest :ParserTestBase
    {
        [Fact]
        public void JmesPathCurrentNodeRoot()
        {
            /*
           * raw : `\"foo\"`
           * json : "\"foo\""
           */
            const string json = "{\"foo\":[\"first\",\"second\"]}";
            const string expression = "@";
            const string expected = json;

            Assert(expression, json, expected);
        }


        [Fact]
        public void JmesPathCurrentNodeProjection()
        {
            /*
           * raw : `\"foo\"`
           * json : "\"foo\""
           */
            const string json = " [    {      \"age\": 20,      \"other\": \"foo\",      \"name\": \"Bob\"    },    {      \"age\": 25,      \"other\": \"bar\",      \"name\": \"Fred\"    },    {      \"age\": 30,      \"other\": \"baz\",      \"name\": \"George\"    }  ]";
            //const string expression = "[*][@.age]";
            const string expression = "[*].{a:@.age}";
            const string expected = "[{\"a\":20},{\"a\":25},{\"a\":30}]";

            Assert(expression, json, expected);
        }
    }
}