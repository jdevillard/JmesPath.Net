using System.Linq;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SumFunction : MathArrayFunction
    {
        public SumFunction()
            : base("sum", 1)
        {

        }

        public override JToken Execute(params JToken[] args)
        {
            var arg = ((JArray)(args[0]));
            if(arg.Count == 0)
                return new JValue(0);

            var s= arg.Select(u => u?.Value<double>() ?? 0);
            return s.Any() ? new JValue(s.Sum()) : null;
        }
    }
}