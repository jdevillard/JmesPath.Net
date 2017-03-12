using System.Linq;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathListWildcardProjection : JmesPathProjection
    {
        public override JmesPathArgument Project(JmesPathArgument argument)
        {
            if (argument.Projection != null)
                return argument;

            var array = argument.Token as JArray;
            if (array == null) return JmesPathArgument.Null;

            var items = array
                .Select(i => (JmesPathArgument)i)
                ;

            return new JmesPathArgument(items);
        }
    }
}