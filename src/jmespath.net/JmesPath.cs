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
        private readonly Encoding _encoding;
        private readonly JmesPathFunctionFactory repository_;
        private readonly ScopeParticipantVisitor evaluator_ = new ScopeParticipantVisitor();

        public JmesPath() : this(Encoding.UTF8)
        {
        }

        public JmesPath(Encoding encoding)
        {
            _encoding = encoding;
            repository_ = JmesPathFunctionFactory.Create(evaluator_);
        }

        public IRegisterFunctions FunctionRepository => repository_;

        [Obsolete("Please, use the Transform(string, string) overload instead.")]
        public JToken Transform(JToken token, string expression)
            => Parse(expression).Transform(token).AsJToken();

        public String Transform(string json, string expression)
            => Transform(ParseJson(json), expression)?.AsString();

        public JmesPathExpression Parse(string expression)
            => Parse(new MemoryStream(_encoding.GetBytes(expression)));

        public JmesPathExpression Parse(Stream stream)
        {
            var analyzer = new JmesPathGenerator(repository_);
            Parser.Parse(stream, _encoding, analyzer);

            // perform post-parsing syntax validation

            var syntax = new SyntaxVisitor();
            analyzer.Expression.Accept(syntax);

            // inject scope evaluator to all expressions

            var evaluator = new ContextEvaluatorVisitor(evaluator_);
            analyzer.Expression.Accept(evaluator);

            return analyzer.Expression;
        }

        public static JToken ParseJson(string input)
        {
            using (var textReader = new StringReader(input))
            using (var reader = new JsonTextReader(textReader))
            {
                reader.DateParseHandling = DateParseHandling.None;

                if (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartArray)
                        return JArray.Load(reader);
                    else if (reader.TokenType == JsonToken.StartObject)
                        return JObject.Load(reader);
                    else
                        return JToken.Load(reader);
                }

                throw new JsonReaderException("Unable to read the JSON string.");
            }
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

        private sealed class ContextEvaluatorVisitor : IVisitor
        {
            private readonly IContextEvaluator evaluator_;

            public ContextEvaluatorVisitor(IContextEvaluator evaluator)
            {
                evaluator_ = evaluator;
            }
            public void Visit(JmesPathExpression expression)
            {
                if (expression is JmesPathIdentifier identifier)
                    identifier.evaluator_ = evaluator_;
            }
        }
    }

    public static class JmesPathExpressionExtensions
    {
        /// <summary>
        /// Helper method that transforms the specified JSON
        /// document by applying the JMESPath expression.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="expression"></param>
        /// <returns>Result as a string</returns>
        public static string Transform(this JmesPathExpression expression, JToken document)
            => expression.Transform(document)
                .AsJToken()
                ?.AsString()
                ;
    }
}
