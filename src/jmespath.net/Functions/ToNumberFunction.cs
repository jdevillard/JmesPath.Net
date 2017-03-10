using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ToNumberFunction : JFunction
    {
        public ToNumberFunction():base("to_number")
        {
            
        }
        public override bool Validate(params JToken[] args)
        {
            if(args==null || args.Length !=1)
                throw new Exception("invalid-arity");
            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            return new JValue(Convert.ToInt32(args[0].Value<Int32>()));
        }
    }
}