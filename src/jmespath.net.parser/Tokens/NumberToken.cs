using System;

namespace DevLab.JmesPath.Tokens
{
    internal sealed class NumberToken : Token
    {
        private readonly int number_;

        public NumberToken(string rawText)
            : base(TokenType.T_NUMBER, rawText)
        {
            var succeeded = Int32.TryParse(RawText, out number_);
            System.Diagnostics.Debug.Assert(succeeded);
        }
        public override object Value => number_;
    }
}