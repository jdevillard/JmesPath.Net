using System;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ToArrayFunction : JmesPathFunction
    {
        public ToArrayFunction()
            : base("to_array", 1)
        {

        }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var arg = args[0].Token;
            return arg.Type == JTokenType.Array
                ? arg
                : new JArray().AddRange(arg)
                ;
        }
    }
}