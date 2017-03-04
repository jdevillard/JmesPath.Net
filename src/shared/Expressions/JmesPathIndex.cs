using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathIndex : JmesPathExpression
    {
        private readonly int index_;

        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathIndex"/>
        /// with the given index.
        /// </summary>
        /// <param name="index"></param>
        public JmesPathIndex(int index)
        {
            index_ = index;
        }

        protected override JToken Transform(JToken json)
        {
            if (json.Type != JTokenType.Array)
                return null;

            var array = json as JArray;
            if (array == null)
                return null;

            var index = index_;

            if (index < 0)
                index = array.Count + index;
            if (index < 0 || index >= array.Count)
                return null;

            return array[index];
        }
    }
}