using System;
using StarodubOleg.GPPG.Runtime;

namespace DevLab.JmesPath
{
    internal partial class JmesPathScanner
    {
        public override void yyerror(string format, params object[] args)
        {
            var line = yyline;
            var column = yycol;
            var text = yytext;

            throw new Exception($"Error({line}, {column}): syntax, near '{text}'.");
        }

        internal int MakeToken(TokenType tokenType)
        {
            yylloc = new LexLocation(tokLin, tokCol, tokELin, tokECol);

            try
            {
                yylval.Token = Token.Create(tokenType, yytext);
                yylval.Token.Location = yylloc;
                return (int)tokenType;
            }
            catch (Exception e)
            {
                throw new Exception($"Error({tokLin}, {tokCol}): syntax, near '{yytext}'.", e);
            }
        }
    }
}
