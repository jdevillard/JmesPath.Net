using System.Collections.Generic;
using DevLab.JmesPath.Interop;
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

        public bool IsExpressionType { get; private set; }

        public static void MakeExpressionType(JmesPathExpression expression)
        {
            expression.IsExpressionType = true;
        }

        /// <summary>
        /// Perform a traversal of the abstract syntax tree.
        /// </summary>
        public virtual void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
