using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class AvgFunction : MathArrayFunction
    {
        public AvgFunction()
            : base("avg", 1)
        {

        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var s = ((JArray)(args[0].Token))
                .Select(u => u.Value<double>());
            return s.Any() ? new JValue(s.Average()) : null;
        }
    }
}