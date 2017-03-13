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
                    else if (item.Token != JTokens.Null)
                        items.Add(item);
                }

                return new JmesPathArgument(items);
            }

            return Transform(argument.Token);
        }

        /// <summary>
        /// Evaluates the expression against the specified JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        protected abstract JmesPathArgument Transform(JToken json);

        /// <summary>
        /// Called by the parser to perform post-parsing validation
        /// when the abstract syntax tree is fully constructed.
        /// This method supports post-parsing validation as required
        /// by the following compliance test case:
        /// https://github.com/jmespath/jmespath.test/blob/master/tests/slice.json
        /// foo[8:2:0:1] => error: syntax
        /// </summary>
        public virtual void Validate()
        {
            /* empty stub */
        }
    }
}
