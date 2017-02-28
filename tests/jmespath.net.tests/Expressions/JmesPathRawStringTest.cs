using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json;

namespace jmespath.net.tests.Expressions
{
    
    public class JmesPathRawStringTest
    {
        [Fact]
        public void JmesPathRawString_NoSpecialChar()
        {
            var rawString = new JmesPathRawString("foo");
            const string input = "{\"foo\": \"value\"}";
                
            var json = JToken.Parse(input);
           
            Assert.Equal("\"foo\"", rawString.Transform(json).AsString());
        }
    }
}
