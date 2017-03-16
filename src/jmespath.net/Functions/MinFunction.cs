using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MinFunction : MathArrayFunction
    {
        public MinFunction()
            : base("min", 1)
        {

        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var s = ((JArray)(args[0].Token))
                .Select(u => Extensions.Value<double>(u));
            return s.Any() ? new JValue(s.Min()) : null;
        }
    }
}