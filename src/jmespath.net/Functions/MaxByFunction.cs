using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MaxByFunction : ByFunction
    {
        public MaxByFunction()
            : base("max_by")
        {
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 2);
            System.Diagnostics.Debug.Assert(args[0].IsToken);
            System.Diagnostics.Debug.Assert(args[1].IsExpressionType);

            var array = (JArray)args[0].Token;
            var expression = args[1].Expression;

            var max = array.Aggregate(
                (left, right) =>
                {
                    var evalLeft = Evaluate(expression, left);
                    var evalRight = Evaluate(expression, right);

                    return evalLeft.Value<double>() > evalRight.Value<double>()
                        ? left
                        : right
                        ;

                });
            return max;
        }
    }
}