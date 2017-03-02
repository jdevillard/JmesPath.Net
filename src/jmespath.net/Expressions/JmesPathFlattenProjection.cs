using System.Collections.Generic;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathFlattenProjection : JmesPathProjection
    {
        public override JToken[] Project(JToken json)
        {
            var items = new List<JToken>();

            var array = json as JArray;
            if (array == null)
                return null;

            foreach (var item in array)
            {
                var nested = item as JArray;
                if (nested == null)
                    items.Add(item);

                else
                    items.AddRange(nested);
            }

            return items.ToArray();
        }

        public override JmesPathArgument Transform(JmesPathArgument argument)
        {
            if (!argument.IsProjection)
                return base.Transform(argument);

            var array = argument.Token as JArray;
            var items =  Project(array);
            return new JmesPathArgument(items, IsProjection);
        }
    }
}