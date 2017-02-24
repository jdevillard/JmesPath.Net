using System;

namespace DevLab.JmesPath
{
    public class Token
    {
        public Token(TokenType type, string rawText)
        {
            Type = type;
            RawText = rawText;
        }

        public TokenType Type { get; }
        public string RawText { get; }
        public virtual object Value => RawText;

        public static Token Create(TokenType tokenType, string yytext)
        {
            switch (tokenType)
            {
                case TokenType.error:
                    break;
                case TokenType.EOF:
                    break;

                case TokenType.T_NUMBER:
                    return new NumberToken(yytext);

                case TokenType.T_QSTRING:
                    return new QuotedStringToken(yytext);

                case TokenType.T_LBRACKET:
                case TokenType.T_RBRACKET:
                case TokenType.T_USTRING:
                default:
                    return new Token(tokenType, yytext);
            }

            System.Diagnostics.Debug.Assert(false);
            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return $"{RawText} ({Type})";
        }
    }
}
