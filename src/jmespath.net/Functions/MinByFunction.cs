using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MinByFunction : ByFunction
    {
        public MinByFunction()
            : base("min_by")
        {
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 2);
            System.Diagnostics.Debug.Assert(args[0].IsToken);
            System.Diagnostics.Debug.Assert(args[1].IsExpressionType);

            var array = (JArray)args[0].Token;
            var expression = args[1].Expression;

            if (array.Count() == 0)
                return null;

            var min = array.Aggregate(
                (left, right) =>
                {
                    var evalLeft = Evaluate(expression, left);
                    var evalRight = Evaluate(expression, right);

                    return evalLeft.Value<double>() < evalRight.Value<double>()
                        ? left
                        : right
                        ;
                });
            return min;
        }

        public override async Task<JToken> ExecuteAsync(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 2);
            System.Diagnostics.Debug.Assert(args[0].IsToken);
            System.Diagnostics.Debug.Assert(args[1].IsExpressionType);

            var array = (JArray)args[0].Token;
            var expression = args[1].Expression;

            if (array.Count == 0)
                return null;

            var evalLeft = await EvaluateAsync(expression, array[0]);
            for (var i = 1; i < array.Count; i++)
            {
                var evalRight = await EvaluateAsync(expression, array[i]);
                if (evalLeft.Value<double>() >= evalRight.Value<double>())
                {
                    evalLeft = evalRight;
                }
            }

            return evalLeft;
        }
    }
}