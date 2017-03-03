using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class RawStringTest
    {
        [Fact]
        public void ParseRawString()
        {
            /*
             * raw : 'foo\'\a\\'
             * C# : foo'\a\\
             * json : "foo'\\a\\\\"
             */
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "'foo\\'\\a\\\\'";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("\"foo'\\\\a\\\\\\\\\"", result);
        }
    }
}
