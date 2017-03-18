using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Interop;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathFunctionExpression : JmesPathExpression
    {
        private readonly string name_;
        private readonly JmesPathExpression[] expressions_;
        private readonly JmesPathFunction function_;

        public JmesPathFunctionExpression(string name, params JmesPathExpression[] expressions)
            : this(JmesPathFunctionFactory.Default, name, expressions)
        {

        }

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
            var expected = function_.MinArgumentCount;
            var actual = expressions?.Length;

            if (actual < expected || (!variadic && actual > expected))
            {
                var more = variadic ? "or more " : "";
                var only = variadic ? "only " : "";
                var report = actual == 0 ? "none" : $"{only}{actual}";
                var plural = expected > 1 ? "s" : "";

                throw new Exception($"Error: invalid-arity, the function {name} expects {expected} argument{plural} {more}but {report} were supplied.");
            }

            name_ = name;
            expressions_ = expressions;
        }

        public JToken Name => name_;

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

            return function_.Execute(arguments);
        }
    }
}