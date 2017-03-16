using System;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class ToArrayFunction : JmesPathFunction
    {
        public ToArrayFunction()
            : base("to_array", 1)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            return new JArray(args[0].Token);
        }
    }
}