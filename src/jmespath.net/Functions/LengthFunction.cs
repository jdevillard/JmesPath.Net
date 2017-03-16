using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class LengthFunction : JmesPathFunction
    {
        public LengthFunction()
            : base("length", 1)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            if (args[0].AsJToken().Type != JTokenType.Object
                && args[0].AsJToken().Type != JTokenType.Array 
                && args[0].AsJToken().Type != JTokenType.String)
                throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var subject = args[0].AsJToken();
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