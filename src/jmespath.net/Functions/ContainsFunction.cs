using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class ContainsFunction : JmesPathFunction
    {
        public ContainsFunction()
            : base("contains", 2)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            if (args[0].AsJToken().Type != JTokenType.Array && args[0].AsJToken().Type != JTokenType.String)
                throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var subject = args[0].AsJToken();
            var search = args[1].AsJToken();
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