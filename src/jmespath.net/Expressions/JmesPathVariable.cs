using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathVariable : JmesPathExpression
    {
          private readonly string name_;
        private readonly string expressionJmespath;

        public JmesPathVariable(string name, string expressionJmespath)
        {
            name_ = name;
            this.expressionJmespath = expressionJmespath;
        }

        public string Name => name_;

        protected override JmesPathArgument Transform(JToken json)
        {
            var jsonObject = json as JObject;
            var variableContent = new JmesPath().Transform(json, this.expressionJmespath);
            this.Context.Variable.Add(name_, variableContent);
            return new JmesPathArgument(json);
        }
    }
}