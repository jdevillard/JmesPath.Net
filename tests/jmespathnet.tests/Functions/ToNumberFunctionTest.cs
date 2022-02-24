using jmespath.net.tests.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace jmespath.net.tests.Functions
{
    using FactAttribute = Xunit.FactAttribute;

    public class ToNumberFunctionTest : ParserTestBase
    {
        [Fact]
        public void ToNumberPositive()
        {
            const string json = "{\"foo\": \"1234\"}";
            const string expression = "to_number(foo)";
            const string expected = "1234";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ToNumberNegative()
        {
            const string json = "{\"foo\": \"-1234\"}";
            const string expression = "to_number(foo)";
            const string expected = "-1234";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ToNumberDecimalPositive()
        {
            const string json = "{\"foo\": \"1234.5\"}";
            const string expression = "to_number(foo)";
            const string expected = "1234.5";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ToNumberDecimalNegative()
        {
            const string json = "{\"foo\": \"-1234.5\"}";
            const string expression = "to_number(foo)";
            const string expected = "-1234.5";

            Assert(expression, json, expected);
        }
    }
}
