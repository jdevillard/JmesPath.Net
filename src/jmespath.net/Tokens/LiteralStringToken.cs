using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Tokens
{
    internal sealed class LiteralStringToken : Token
    {
        private readonly JToken value_;

        public LiteralStringToken(string rawText)
            : base(TokenType.T_LSTRING, rawText)
        {
            System.Diagnostics.Debug.Assert(rawText.Length >= 2);
            System.Diagnostics.Debug.Assert(rawText.StartsWith("`"));
            System.Diagnostics.Debug.Assert(rawText.EndsWith("`"));

            var literal = StringUtil.UnescapeLiteral(rawText);
            value_ = JToken.Parse(literal);
        }

        public override object Value => value_;
    }
}