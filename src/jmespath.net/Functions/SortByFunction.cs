using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class SortByFunction : JmesPathFunction
    {
        public SortByFunction()
            : base("sort_by", 2)
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
            var ordered = list.OrderBy(u =>
            {
                var e = args[1].Expression.Transform(u);
                if (e.AsJToken().Type != JTokenType.Float
                        && e.AsJToken().Type != JTokenType.Integer
                        && e.AsJToken().Type != JTokenType.String)
                    throw new Exception("invalid-type");
                return e.AsJToken();
            }).ToArray();
            return new JArray(ordered.AsJEnumerable());
        }
    }
}