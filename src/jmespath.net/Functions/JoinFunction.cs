using System;
using System.Linq;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class JoinFunction : JmesPathFunction
    {
        public JoinFunction()
            : base("join", 2)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            if (args[0].Type != JTokenType.String 
                || args[1].Type != JTokenType.Array)
                throw new Exception("invalid-type");

            foreach (var item in (JArray)args[1])
                if (item.Type != JTokenType.String)
                    throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            var glue = args[0].Value<String>();
            var stringsArray = ((JArray)args[1]).Select(u => u.Value<String>());
            return new JValue(String.Join(glue,stringsArray));
        }
    }
}