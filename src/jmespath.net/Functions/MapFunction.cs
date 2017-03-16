using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class MapFunction : JmesPathFunction
    {
        public MapFunction()
            : base("map", 2)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            if (!args[0].IsExpressionType)
                throw new Exception("invalid-type");
            var list = args[1].AsJToken();
            if (list.Type != JTokenType.Array)
                throw new Exception("invalid-type");
            
            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var expression = args[0].Expression;
            var elements = (JArray) (args[1].AsJToken());
            var result = elements.Select(e =>
                expression.Transform(e).AsJToken()
                ).ToArray();
            

            return new JArray(result);
        }     
    }
}