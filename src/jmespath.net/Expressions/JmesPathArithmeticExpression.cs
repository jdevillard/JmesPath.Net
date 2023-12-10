using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

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

            return Compute(rightToken, leftToken);
        }

        protected override async Task<JmesPathArgument> TransformAsync(JToken json)
        {
            var leftToken = (await Left.TransformAsync(json)).AsJToken();
            var rightToken = (await Right.TransformAsync(json)).AsJToken();

            return Compute(rightToken, leftToken);
        }
        
        private JmesPathArgument Compute(JToken rightToken, JToken leftToken)
        {
            if (rightToken.GetTokenType() != leftToken.GetTokenType() || leftToken.GetTokenType() != "number")
                return JTokens.Null;

            var l = leftToken.Value<double>();
            var r = rightToken.Value<double>();

            var result = Compute(l, r);
            if (Double.IsInfinity(result))
                throw new Exception($"Error: not-a-number, expression {this} overflow.");
            if (Double.IsNaN(result))
                throw new Exception($"Error: not-a-number, expression {this} is an illegal arithmetic operation.");

            return JToken.FromObject(result);
        }

        protected abstract double Compute(double left, double right);
    }
}