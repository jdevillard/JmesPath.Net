using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public abstract class JmesPathProjection : JmesPathExpression
    {
        public abstract JToken[] Project(JToken json);
        
        public override bool IsProjection => true;

        protected override JToken Transform(JToken json)
        {
            var items = Project(json);
            return new JArray().AddRange(items);
        }
    }
}