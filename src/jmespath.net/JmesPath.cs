using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Utils;

namespace DevLab.JmesPath
{
    public sealed class JmesPath
    {
        private readonly JmesPathFunctionFactory repository_;

        public JmesPath()
        {
            repository_ = new JmesPathFunctionFactory();
            foreach (var name in JmesPathFunctionFactory.Default.Names)
                repository_.Register(name, JmesPathFunctionFactory.Default[name]);
        }

        public IRegisterFunctions FunctionRepository => repository_;

        public JToken Transform(JToken token, string expression)
        {
            var jmesPath = Parse(expression);
            var result = jmesPath.Transform(token);
            return result.AsJToken();
        }

        public String Transform(string json, string expression)
        {
            var token = JToken.Parse(json);
            var result = Transform(token, expression);
            return result.AsString();
        }

        private JmesPathExpression Parse(string expression)
        {
            return Parse(new MemoryStream(Encoding.UTF8.GetBytes(expression)));
        }

        private JmesPathExpression Parse(Stream stream)
        {
            var scanner = new JmesPathScanner(stream);
            var analyzer = new JmesPathParser(scanner, repository_);
            if (!analyzer.Parse())
            {
                System.Diagnostics.Debug.Assert(false);
                throw new Exception("Error: syntax.");
            }

            // perform post-parsing syntax validation

            var syntax = new SyntaxVisitor();
            analyzer.Expression.Accept(syntax);

            return analyzer.Expression;
        }
        private sealed class SyntaxVisitor : IVisitor
        {
            public void Visit(JmesPathExpression expression)
            {
                var projection = expression as JmesPathSliceProjection;
                if (projection?.Step != null && projection.Step.Value == 0)
                    throw new Exception("Error: invalid-value, a slice projection step cannot be 0.");
            }
        }
    }
}
