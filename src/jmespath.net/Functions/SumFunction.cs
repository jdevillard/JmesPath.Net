using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SumFunction : JmesPathFunction
    {
        public SumFunction()
            : base("sum", 1)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureArrayOf(args[0], "number");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var array = (JArray)args[0].Token;

            if (array.Count == 0)
                return new JValue(0);

            var sum = 0.0;
            foreach (var item in array)
            {
                sum += item?.Value<double>() ?? 0.0;
            }

            return IsInteger(sum) 
                ? new JValue(Convert.ToInt32(sum))
                : new JValue(sum)
                ;
        }
    }
}