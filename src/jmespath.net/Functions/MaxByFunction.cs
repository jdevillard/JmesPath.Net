using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MaxByFunction : JmesPathFunction
    {
        public MaxByFunction()
            : base("max_by", 2)
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

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var list = (JArray)args[0].Token;
            var max = list.Aggregate((i1, i2) =>
            {
                var e1 = Transform(args[1], i1);
                var e2 = Transform(args[1], i2);

                var compare = e1.Value<double>() > e2.Value<double>();
                return compare ? i1 : i2;
            });
            return max;
        }

        private static JToken Transform(JmesPathFunctionArgument arg, JToken i1)
        {
            var e = arg.Expression.Transform(i1).AsJToken();
            if (e.Type != JTokenType.Float
                && e.Type != JTokenType.Integer
                && e.Type != JTokenType.String
                )
                throw new Exception("invalid-type");
            if (e.Type == JTokenType.String)
            {
                double d;
                if(double.TryParse(e.Value<String>(),out d))
                    return new JValue(d);
                else
                    throw new Exception("invalid-type");
            }
                
            return e;
        }
    }
}