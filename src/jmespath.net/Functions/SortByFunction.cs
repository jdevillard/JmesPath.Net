using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SortByFunction : ByFunction
    {
        public SortByFunction()
            : base("sort_by")
        {
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
            return new JArray().AddRange(ordered);
        }
    }
}