using System.Linq;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MaxFunction : MinOrMaxFunction
    {
        public MaxFunction()
            : base("max")
        {
        }

        protected override JToken GetMinOrMax<T>(JArray array)
        {
            var sequence = array
                .Select(u => u.Value<T>())
                .ToArray()
                ;

            return sequence.Length > 0
                ? new JValue(sequence.Max())
                : null
                ;
        }
    }
}