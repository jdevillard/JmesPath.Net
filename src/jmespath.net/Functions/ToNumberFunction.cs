using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ToNumberFunction : JFunction
    {
        public ToNumberFunction()
            : base("to_number", 1)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            return new JValue(Convert.ToInt32(args[0].Value<int>()));
        }
    }
}