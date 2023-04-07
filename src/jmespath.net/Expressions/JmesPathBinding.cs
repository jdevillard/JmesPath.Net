namespace DevLab.JmesPath.Expressions {
    public class JmesPathBinding {
        private readonly string name_;
        private readonly JmesPathExpression expression_;

        public JmesPathBinding(string name, JmesPathExpression expression) {
            System.Diagnostics.Debug.Assert(name.StartsWith("$"));
            name_ = name.Substring(1);
            expression_ = expression; 
        }

        public string Name => name_;
        public JmesPathExpression Expression => expression_;

        public override string ToString()
            => $"${name_} = {expression_}";
    }
}