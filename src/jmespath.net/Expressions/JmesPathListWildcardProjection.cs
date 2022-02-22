using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathListWildcardProjection : JmesPathProjection
    {
        public override JmesPathArgument Project(JmesPathArgument argument)
        {
            if (argument.IsProjection)
                return argument;

            var array = argument.Token as JArray;
            if (array == null)
                return null;

            var items = array
                .Where(i => !JTokens.IsNull(i))
                .Select(i => (JmesPathArgument)i)
                ;

            return new JmesPathArgument(items);
        }
    }
}