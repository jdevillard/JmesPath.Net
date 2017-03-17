using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SortByFunction : JmesPathFunction
    {
        public SortByFunction()
            : base("sort_by", 2)
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
            var init = false;
            JTokenType type = JTokenType.None;
            var list = (JArray)args[0].Token;
            var ordered = list.OrderBy(u =>
            {
                var e = args[1].Expression.Transform(u);

                if (!init)
                {
                    if (e.AsJToken().Type != JTokenType.Float
                        && e.AsJToken().Type != JTokenType.Integer
                        && e.AsJToken().Type != JTokenType.String)
                        throw new Exception("invalid-type");

                    type = e.AsJToken().Type;
                    init = true;
                }
                if(type != e.AsJToken().Type)
                    throw new Exception("invalid-type");
                                
                return e.AsJToken();
            }).ToArray();
            return new JArray(ordered.AsJEnumerable());
        }
    }
}