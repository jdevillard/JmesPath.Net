using System;
using System.Linq;
using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ContainsFunction : JmesPathFunction
    {
        public ContainsFunction()
            : base("contains", 2)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            if (args[0].Type != JTokenType.Array && args[0].Type != JTokenType.String)
                throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            var subject = args[0];
            var search = args[1];
            if (subject.Type == JTokenType.String)
            {
                var subjectValue = subject.Value<String>();
                return subjectValue.Contains(search.Value<String>());
            }
            if (subject.Type == JTokenType.Array)
                return ((JArray) subject).Any(a=> JToken.DeepEquals(a, search));

            return false;
        }
    }
}