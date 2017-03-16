
using System;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class MergeFunction : JmesPathFunction
    {
        public MergeFunction()
            : base("merge", 1, true)
        {

        }

        public override bool Validate(params JmesPathArgument[] args)
        {
            foreach (var jmesPathArgument in args)
                if (jmesPathArgument.Token.Type != JTokenType.Object)
                    throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            JObject last = new JObject();
            foreach (var jmesPathArgument in args)
            {
                var token = (JObject)jmesPathArgument.Token;
                last.Merge(token);
            }

            return last;
        }
    }
}