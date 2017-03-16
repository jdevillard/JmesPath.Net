using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class MaxByFunction : JmesPathFunction
    {
        public MaxByFunction()
            : base("max_by", 2)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            var list = args[0].Token;
            if (list.Type != JTokenType.Array)
                throw new Exception("invalid-type");
            if (!args[1].IsExpressionType)
                throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var list = (JArray)(args[0].Token).AsJEnumerable();
            var max = list.Aggregate((i1, i2) =>
            {
                var e1 = Transform(args[1], i1);
                var e2 = Transform(args[1], i2);

                var compare = e1.Value<double>() > e2.Value<double>();
                if (compare)
                    return i1;
                else
                    return i2;
            });
            return max;
        }

        private static JToken Transform(JmesPathArgument arg, JToken i1)
        {
            var e = arg.Expression.Transform(i1);
            if (e.Token.Type != JTokenType.Float
                && e.Token.Type != JTokenType.Integer
                )
                throw new Exception("invalid-type");
            return e.Token;
        }
    }
}