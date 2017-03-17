using System;
using DevLab.JmesPath.Expressions;
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
            if (arg.Type == JTokenType.Array)
                return arg;
            return new JArray().AddRange(arg);
        }
    }
}