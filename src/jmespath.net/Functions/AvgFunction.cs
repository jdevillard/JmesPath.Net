using System;
using System.Linq;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class AvgFunction : JmesPathFunction
    {
        public AvgFunction()
            : base("avg", 1)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            if(args[0].Type != JTokenType.Array)
                throw new Exception("invalid-type");

            foreach (var item in (JArray)args[0])
                if (item.Type != JTokenType.Integer && item.Type != JTokenType.Float)
                    throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            var s = ((JArray) (args[0]))
                .Select(u => u.Value<double>());
            return s.Any() ? new JValue(s.Average()) : null;
        }
    }
}