using DevLab.JmesPath.Utils;

namespace DevLab.JmesPath.Tokens
{
    internal sealed class RawStringToken : Token
    {
        private readonly string value_;

        public RawStringToken(string rawText)
            : base(TokenType.T_RSTRING, rawText)
        {
            System.Diagnostics.Debug.Assert(rawText.Length >= 2);
            System.Diagnostics.Debug.Assert(rawText.StartsWith("'"));
            System.Diagnostics.Debug.Assert(rawText.EndsWith("'"));

            value_ = StringUtil.UnescapeRaw(rawText);
        }

        public override object Value => value_;
    }
}