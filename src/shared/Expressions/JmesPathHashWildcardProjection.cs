using System.Linq;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathHashWildcardProjection : JmesPathProjection
    {
        public override JToken[] Project(JToken json)
        {
            var item = json as JObject;

            return item?.Properties()
                .Select(p => p.Value)
                .ToArray()
                ;
        }
    }
}