using System.Collections.Generic;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathFlattenProjection : JmesPathProjection
    {
        public override JmesPathArgument Project(JmesPathArgument argument)
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

        public override JmesPathArgument Transform(JmesPathArgument argument)
        {
            return argument.IsProjection 
                ? Project(argument) 
                : base.Transform(argument)
                ;
        }
    }
}