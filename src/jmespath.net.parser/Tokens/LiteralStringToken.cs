using DevLab.JmesPath.Utils;
using JsonCheckerTool;

namespace DevLab.JmesPath.Tokens
{
    internal sealed class LiteralStringToken : Token
    {
        private readonly string value_;

        public LiteralStringToken(string literalText)
            : base(TokenType.T_LSTRING, literalText)
        {
            System.Diagnostics.Debug.Assert(literalText.Length >= 2);
            System.Diagnostics.Debug.Assert(literalText.StartsWith("`"));
            System.Diagnostics.Debug.Assert(literalText.EndsWith("`"));

            var literal = StringUtil.UnwrapLiteral(literalText);
            var checker = new JsonChecker();
            var lws = true;
            var scalar = false;
            foreach (var ch in literal)
            {
                if (lws) // leading white space?
                {
                    switch (ch)
                    {
                        case ' ': case '\t': case '\r': case '\n':
                            break; // ignore leading white space
                        default:   // first non-white-space
                            lws = false;
                            // if it's a scalar then embed in an array
                            if (scalar = ch != '[' && ch != '{')
                                checker.Check('[');
                            break;
                    }
                }
                checker.Check(ch);
            }
            if (scalar)
                checker.Check(']');
            checker.FinalCheck();
            value_ = literal;
        }

        public override object Value => value_;
    }
}