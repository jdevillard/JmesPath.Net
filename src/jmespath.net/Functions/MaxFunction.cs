using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MaxFunction : MathArrayFunction
    {
        public MaxFunction()
            : base("max", 1)
        {

        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var s = ((JArray)(args[0].AsJToken()))
                .Select(u => Extensions.Value<double>(u));
            return s.Any() ? new JValue(s.Max()) : null;
        }
    }
}