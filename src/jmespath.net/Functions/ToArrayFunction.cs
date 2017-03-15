using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ToArrayFunction : JmesPathFunction
    {
        public ToArrayFunction()
            : base("to_array", 1)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            return new JArray(args[0]);
        }
    }
}