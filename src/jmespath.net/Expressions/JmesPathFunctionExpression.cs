using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Interop;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathFunctionExpression : JmesPathExpression
    {
        private readonly string name_;
        private readonly JmesPathExpression[] expressions_;
        private readonly JmesPathFunction function_;

        public JmesPathFunctionExpression(IFunctionRepository repository, string name, IList<JmesPathExpression> expressions)
            : this(repository, name, expressions.ToArray())
        {
        }

        public JmesPathFunctionExpression(IFunctionRepository repository, string name, params JmesPathExpression[] expressions)
        {
            if (!repository.Contains(name))
                throw new Exception($"Error: unknown-function, no function named {name} has been registered.");

            function_ = repository[name];

            var variadic = function_.Variadic;
            var minExpected = function_.MinArgumentCount;
            var maxExpected = function_.MaxArgumentCount;
            var actual = expressions?.Length;

            if (actual < minExpected || (!variadic && maxExpected == null && actual > minExpected))
            {
                var more = variadic ? "or more " : "";
                var only = variadic ? "only " : "";
                var report = actual == 0 ? "none" : $"{only}{actual}";
                var plural = minExpected > 1 ? "s" : "";

                throw new Exception($"Error: invalid-arity, the function {name} expects {minExpected} argument{plural} {more}but {report} were supplied.");
            }

            if (maxExpected != null && actual > maxExpected)
                throw new Exception($"Error: invalid-arity, the function {name} expects at most {maxExpected} arguments but {actual} were supplied.");

            name_ = name;
            expressions_ = expressions;
        }

        public string Name => name_;
        public JmesPathExpression[] Arguments => expressions_;

        protected override JmesPathArgument Transform(JToken json)
        {
            var arguments = expressions_.Select(
                expression =>
                {
                    if (expression.IsExpressionType)
                        return new JmesPathFunctionArgument(expression);
                    else
                        return new JmesPathFunctionArgument(expression.Transform(json).AsJToken());
                })
                .ToArray()
                ;

            function_.Validate(arguments);
            function_.SetContext(json);

            return function_.Execute(arguments);
        }

        protected override async Task<JmesPathArgument> TransformAsync(JToken json)
        {
            var arguments = await Task.WhenAll(expressions_.Select(
                        async expression =>
                        {
                            if (expression.IsExpressionType)
                                return new JmesPathFunctionArgument(expression);
                            else
                                return new JmesPathFunctionArgument((await expression.TransformAsync(json)).AsJToken());
                        })
                    .ToArray());

            function_.Validate(arguments);
            function_.SetContext(json);

            return await function_.ExecuteAsync(arguments);
        }
        
        public override void Accept(IVisitor visitor)
        {
            base.Accept(visitor);
            foreach (var expression in expressions_)
                expression.Accept(visitor);
        }

        protected override string Format()
            => $"{name_}({string.Join(", ", expressions_.AsEnumerable())})";
    }
}