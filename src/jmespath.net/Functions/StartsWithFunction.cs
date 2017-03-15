using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class StartsWithFunction : JmesPathFunction
    {
        public StartsWithFunction()
            : base("starts_with", 2)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            if (args[0].Type != JTokenType.String
                || args[1].Type != JTokenType.String)
                throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            var subject = args[0].Value<String>();
            var prefix = args[1].Value<String>();

            return subject.StartsWith(prefix);
        }
    }
}