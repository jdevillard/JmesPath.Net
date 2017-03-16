using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class MinByFunction : JmesPathFunction
    {
        public MinByFunction()
            : base("min_by", 2)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            var list = args[0].AsJToken();
            if (list.Type != JTokenType.Array)
                throw new Exception("invalid-type");
            if (!args[1].IsExpressionType)
                throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var list = (JArray)(args[0].AsJToken()).AsJEnumerable();
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

        private static JToken Transform(JmesPathArgument arg, JToken i1)
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