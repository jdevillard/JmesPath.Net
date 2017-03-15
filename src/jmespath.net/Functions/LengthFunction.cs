using System;
using System.Linq;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class LengthFunction : JmesPathFunction
    {
        public LengthFunction()
            : base("length", 1)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            if (args[0].Type != JTokenType.Object
                && args[0].Type != JTokenType.Array 
                && args[0].Type != JTokenType.String)
                throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            var subject = args[0];
            switch (subject.Type)
            {
                case JTokenType.String:
                    return subject.Value<String>().Length;               
                case JTokenType.Array:
                    return ((JArray) subject).Count();
                case JTokenType.Object:
                    return ((JObject) subject).Count;
                default:
                    return 0;
            }
        }
    }
}