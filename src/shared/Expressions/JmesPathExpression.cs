using System.Collections.Generic;
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
        /// The result cannot be null and is:
        /// either a valid JSON found in the resulting <see cref="JmesPathArgument"/>'s Token property.
        /// or a projection found in the resulting <see cref="JmesPathArgument"/>'s Projection property.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public virtual JmesPathArgument Transform(JmesPathArgument argument)
        {
            if (argument.IsProjection)
            {
                var items = new List<JmesPathArgument>();
                foreach (var projected in argument.Projection)
                {
                    var item = Transform(projected);
                    if (item.IsProjection)
                        items.Add(item);
                    else if (item.Token != JmesPathArgument.JNull)
                        items.Add(item);
                }

                return new JmesPathArgument(items);
            }

            return Transform(argument.Token) ?? JmesPathArgument.Null;
        }

        /// <summary>
        /// Evaluates the expression against the specified JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        protected abstract JmesPathArgument Transform(JToken json);
    }
}
