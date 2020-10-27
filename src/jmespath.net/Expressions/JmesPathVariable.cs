using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathVariableExpression : JmesPathExpression
    {
          private readonly string name_;
        private readonly JmesPathExpression expressionJmespath;

        public JmesPathVariableExpression(string name, JmesPathExpression expressionJmespath)
        {
            name_ = name;
            this.expressionJmespath = expressionJmespath;
        }

        public string Name => name_;

        protected override JmesPathArgument Transform(JToken json)
        {
            var variableContent = expressionJmespath.Transform(json);
            this.Context.Variable.Add(name_, variableContent.Token);
            return json;
        }
    }
}