using System;
using StarodubOleg.GPPG.Runtime;

namespace DevLab.JmesPath
{
    internal partial class JmesPathScanner
    {
        private PushbackQueue<ScanObj> pushback_;

        /// <summary>
        /// This method enable extended lookahead of more that one
        /// token to resolve ambiguities in the grammar.
        /// see: gppg manual, page 51, 10 AppendixC:PushingBackInputSymbols.
        /// see: http://softwareautomata.blogspot.fr/2011/12/doing-ad-hoc-lookahead-in-gppg-parsers_25.html
        /// 
        /// </summary>
        public void InitializeLookaheadQueue()
        {
            pushback_ = PushbackQueue<ScanObj>.NewPushbackQueue(
                () => new ScanObj(yylex(), yylval, yylloc),
                tk => new ScanObj(tk, yylval, yylloc)
                );
        }

        public int EnqueueAndReturnInitialToken(int token)
        {
            return pushback_.EnqueueAndReturnInitialSymbol(token)
                .token
                ;
        }

        public int GetAndEnqueue()
        {
            return pushback_.GetAndEnqueue()
                .token
                ;
        }

        public void AddPushbackBufferToQueue()
        {
            pushback_.AddPushbackBufferToQueue();
        }

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
