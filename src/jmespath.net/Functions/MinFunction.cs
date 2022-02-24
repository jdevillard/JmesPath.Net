using System.Linq;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MinFunction : MinOrMaxFunction
    {
        public MinFunction()
            : base("min")
        {
        }

        protected override JToken GetMinOrMax<T>(JArray array)
        {
            var sequence = array
                .Select(u => u.Value<T>())
                .ToArray()
                ;

            return sequence.Length > 0
                ? new JValue(sequence.Min())
                : null
                ;
        }
    }
}