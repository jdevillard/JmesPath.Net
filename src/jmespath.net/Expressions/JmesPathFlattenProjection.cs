using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathFlattenProjection : JmesPathProjection
    {
        protected override JmesPathArgument Project(IEnumerable<JmesPathArgument> arguments)
        {
            // flatten the array before
            // projecting again

            var projection = new JmesPathArgument(arguments);
            var array = projection.AsJToken();
            return Project(array);
        }
        protected override Task<JmesPathArgument> ProjectAsync(IEnumerable<JmesPathArgument> arguments)
        {
            // flatten the array before
            // projecting again

            var projection = new JmesPathArgument(arguments);
            var array = projection.AsJToken();
            return Task.FromResult(Project(array));
        }
        
        protected override JmesPathArgument Project(JmesPathArgument argument)
        {
            if (argument.IsProjection)
                argument = argument.AsJToken();

            var items = new List<JmesPathArgument>();

            var array = argument.Token as JArray;
            if (array == null)
                return null;

            foreach (var item in array)
            {
                if (JTokens.IsNull(item))
                    continue;

                var nested = item as JArray;
                if (nested == null)
                    items.Add(item);

                else
                    items.AddRange(
                        nested
                            .Where(i => !JTokens.IsNull(i))
                            .Select(i => (JmesPathArgument) i)
                    );
            }

            return new JmesPathArgument(items);
        }

        protected override string Format()
            => "[]";
    }
}