using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents a JmesPath slice expression.
    /// </summary>
    public class JmesPathSliceExpression : JmesPathExpression
    {
        private readonly JmesPathExpression expression_;

        public JmesPathSliceExpression(JmesPathExpression number)
        {
            expression_ = number;
        }

        public override JToken Transform(JToken json)
        {
            if (json.Type != JTokenType.Array)
                return null;

            // slice expression adhere to the following rule:
            // if the element being sliced is not an array, the result is null.

            var array = json as JArray;
            if (array == null)
                return null;

            if (expression_ is JmesPathNumber)
            {
                var index = ((JmesPathNumber)expression_).Value;
                return array[index];
            }
            else
                return null;
        }
    }
}