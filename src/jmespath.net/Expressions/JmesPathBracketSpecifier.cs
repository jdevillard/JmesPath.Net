using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathBracketSpecifier : JmesPathExpression
    {
        private readonly JmesPathExpression expression_;

        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathBracketSpecifier"/>
        /// with the given index.
        /// </summary>
        /// <param name="index"></param>
        public JmesPathBracketSpecifier(JmesPathNumber index)
        {
            expression_ = index;
        }

        public override JToken Transform(JToken json)
        {
            if (json.Type != JTokenType.Array)
                return null;

            var array = json as JArray;
            if (array == null)
                return null;

            if (expression_ is JmesPathNumber)
            {
                var index = ((JmesPathNumber)expression_).Value;
                if (index < 0)
                    index = array.Count + index;
                if (index < 0 || index >= array.Count)
                    return null;
                return array[index];
            }
            else
                return null;
        }
    }
}