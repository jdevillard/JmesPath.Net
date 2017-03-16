using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class JoinFunction : JmesPathFunction
    {
        public JoinFunction()
            : base("join", 2)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            if (args[0].AsJToken().Type != JTokenType.String 
                || args[1].AsJToken().Type != JTokenType.Array)
                throw new Exception("invalid-type");

            foreach (var item in (JArray)(args[1].AsJToken()))
                if (item.Type != JTokenType.String)
                    throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var glue = args[0].AsJToken().Value<String>();
            var stringsArray = ((JArray)args[1].AsJToken()).Select(u => u.Value<String>());
            return new JValue(String.Join(glue,stringsArray));
        }
    }
}