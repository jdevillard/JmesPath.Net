using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using DevLab.JmesPath;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathFunctionTest
    {
       [Fact]
        public void ParseJsonToFunction()
        {
            /*
             * raw : `\"foo\"`
             * json : "\"foo\""
             */
            
            //JmesPathFunctionFactory.Register("to_number", new ToNumberFunction());
          //  JmesPathFunctionFactory.Register<ToNumberFunction>();

            const string json = "{\"foo\": [\"1\",\"second\"]}";
            string expression = "{\"baz\":to_number(`{\"foo\":[\"42\",\"12\"]}`.foo[0])}";
            

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("{\"baz\":42}", result);
        }

    }
}