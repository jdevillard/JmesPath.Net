using System;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public abstract class ByFunction : JmesPathFunction
    {
        protected ByFunction(string name)
            : base(name, 2)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            var array = args[0].Token;
            if (array.Type != JTokenType.Array)
                throw new Exception($"Error: invalid-type, function {Name} expects its first argument to be an array.");

            if (!args[1].IsExpressionType)
                throw new Exception($"Error: invalid-type, function {Name} expects its second argument to be an expression type.");
        }

        protected JToken Evaluate(JmesPathExpression expression, JToken token)
        {
            var eval = expression.Transform(token).AsJToken();
            var type = eval.GetTokenType();

            if (type != "number" && type != "string")
                throw new Exception($"Error: invalid-type, the expression argument of function {Name} should return a number or a string.");

            if (type == "number")
                return eval;

            double number;
            if (double.TryParse(eval.Value<string>(), out number))
                return new JValue(number);

            throw new Exception("Error: invalid-type, when evaluating the expression argument of function {Name}, some items could not be cast to a number.");
        }
    }
}