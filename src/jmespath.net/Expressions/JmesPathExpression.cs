using System.Collections.Generic;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents the base class for a JmesPath expression.
    /// </summary>
    public abstract class JmesPathExpression
    {
        /// <summary>
        /// Evaluates the expression against the specified JSON object.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public virtual JmesPathArgument Transform(JmesPathArgument argument)
        {
            if (argument.IsProjection)
            {
                var array = argument.Token as JArray;
                System.Diagnostics.Debug.Assert(array != null);
                var items = new List<JToken>();
                foreach (var token in array)
                {
                    var item = Transform(token);
                    if (item != null)
                        items.Add(item);
                }

                return new JmesPathArgument(items, IsProjection);
            }

            var result = Transform(argument.Token);
            return new JmesPathArgument(result, IsProjection);
        }

        public virtual bool IsProjection { get; } = false;

        protected abstract JToken Transform(JToken json);
    }
}
