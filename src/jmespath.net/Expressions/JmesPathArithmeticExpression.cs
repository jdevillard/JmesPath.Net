using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public abstract class JmesPathArithmeticExpression : JmesPathCompoundExpression
    {
        public JmesPathArithmeticExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        {
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            var leftToken = Left.Transform(json).AsJToken();
            var rightToken = Right.Transform(json).AsJToken();

            if (rightToken.GetTokenType() != leftToken.GetTokenType() || leftToken.GetTokenType() != "number")
                return JTokens.Null;

            var l = leftToken.Value<double>();
            var r = rightToken.Value<double>();

            return JToken.FromObject(Compute(l, r));
        }

        protected abstract double Compute(double left, double right);
    }
}