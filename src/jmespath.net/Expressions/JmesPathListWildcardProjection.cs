using System.Linq;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathListWildcardProjection : JmesPathProjection
    {
        public override JToken[] Project(JToken json)
        {
            var array = json as JArray;
            return array?.ToArray();
        }
    }
}