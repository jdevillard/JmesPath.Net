using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SumFunction : MathArrayFunction
    {
        public SumFunction()
            : base("sum", 1)
        {

        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var arg = ((JArray)(args[0].AsJToken()));
            if(arg.Count == 0)
                return new JValue(0);

            var s= arg.Select(u => u?.Value<double>() ?? 0);
            return s.Any() ? new JValue(s.Sum()) : null;
        }
    }
}