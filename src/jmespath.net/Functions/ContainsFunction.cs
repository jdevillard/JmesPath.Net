using System;
using System.Linq;
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
        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureArrayOrString(args[0]);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 2);
            System.Diagnostics.Debug.Assert(args[0].IsToken);
            System.Diagnostics.Debug.Assert(args[1].IsToken);

            var subject = args[0].Token;
            var search = args[1].Token;
            if (subject.Type == JTokenType.String)
            {
                var subjectValue = subject.Value<String>();
                return subjectValue.Contains(search.Value<String>());
            }
            if (subject.Type == JTokenType.Array)
                return ((JArray) subject).Any(a=> JToken.DeepEquals(a, search));

            return JTokens.False;
        }
    }
}