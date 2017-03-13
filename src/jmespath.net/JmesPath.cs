using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json;

namespace DevLab.JmesPath
{
    public sealed class JmesPath
    {
        private readonly JmesPathFunctionFactory repositoryFactory_;

        public JmesPath()
        {
            repositoryFactory_ = new JmesPathFunctionFactory();
            foreach (var name in JmesPathFunctionFactory.Default.Names)
            {
                repositoryFactory_.Register(name, JmesPathFunctionFactory.Default[name]);
            }
        }

        public IRegisterFunctions FunctionRepository => repositoryFactory_;
        
        public JToken Transform(JToken token, string expression)
        {
            var jmesPath = Parse(expression);
            if (jmesPath == null)
                return null;

            var result = jmesPath.Transform(token);
            return result.AsJToken();
        }

        public String Transform(string json, string expression)
        {
            var token = JToken.Parse(json);
            var result = Transform(token, expression);
            return result.AsString();
        }

        private static JmesPathExpression Parse(string expression)
        {
            return Parse(new MemoryStream(Encoding.UTF8.GetBytes(expression)));
        }

        private static JmesPathExpression Parse(Stream stream)
        {
            var scanner = new JmesPathScanner(stream);
            var analyzer = new JmesPathParser(scanner);
            if (!analyzer.Parse())
                return null;

            return analyzer.Expression;
        }
    }
}
