using System.Linq;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathHashWildcardProjection : JmesPathProjection
    {
        public override JmesPathArgument Project(JmesPathArgument json)
        {
            if (json.Projection != null)
                return json;

            var item = json.Token as JObject;
            if (item == null) return JmesPathArgument.Null;

            var hashes =
                item
                .Properties()
                .Select(p => (JmesPathArgument) p.Value)
                ;

            return new JmesPathArgument(hashes);
        }
    }
}