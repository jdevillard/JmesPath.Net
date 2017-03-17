using System;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MapFunction : JmesPathFunction
    {
        public MapFunction()
            : base("map", 2)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            if (!args[0].IsExpressionType)
                throw new Exception($"Error: invalid-type, function {Name} expects its first argument to be an expression type.");

            var array = args[1].Token;
            if (array.Type != JTokenType.Array)
                throw new Exception($"Error: invalid-type, function {Name} expects its second argument to be an array.");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var expression = args[0].Expression;
            var elements = (JArray) (args[1].Token);

            var items = elements.Select(e =>
                expression.Transform(e).AsJToken()
                ).ToArray();          

            return new JArray().AddRange(items);
        }     
    }
}