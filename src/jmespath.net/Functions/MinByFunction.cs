using System;
using System.Linq;
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
            var list = (JArray)args[0].Token;
            var max = list.Aggregate((i1, i2) =>
            {
                var e1 = Transform(args[1], i1);
                var e2 = Transform(args[1], i2);

                var compare = e1.Value<double>() < e2.Value<double>();
                if (compare)
                    return i1;
                else
                    return i2;
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
                if (double.TryParse(e.Value<String>(), out d))
                    return new JValue(d);
                else
                    throw new Exception("invalid-type");
            }

            return e;
        }
    }
}