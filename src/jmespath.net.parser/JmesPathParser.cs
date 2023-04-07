using System;
using StarodubOleg.GPPG.Runtime;
using DevLab.JmesPath.Tokens;

namespace DevLab.JmesPath
{
    partial class JmesPathParser
    {
        readonly IJmesPathGenerator generator_;

        public JmesPathParser(AbstractScanner<ValueType, LexLocation> scanner,
                              IJmesPathGenerator generator)
            : base(scanner)
        {
            generator_ = generator;
        }

        #region Implementation

        void OnExpression() => generator_.OnExpression();

        void ResolveParsingState()
        {
            // the grammar does not specify this but the canonical
            // implementation accepts constructs that consist of
            // projections immediately followed by a multi_select_list.
            // these constructs should be separated with a T_DOT
            // because these really are sub_expressions in disguise.

            // when this method is called, upon each successful
            // parse of an expression, we need to identify
            // whether we are in such a case and insert a T_DOT
            // in the token stream if necessary

            // only interested in cases where previous construct is a projection

            if (!generator_.IsProjection())
                return;

            var scanner = this.Scanner as JmesPathScanner;
            System.Diagnostics.Debug.Assert(scanner != null);

            var next = scanner.EnqueueAndReturnInitialToken(NextToken);
            NextToken = 0;

            if (next == (int)TokenType.T_LBRACKET && IsParsingMultiSelectList())
                ResolveSubExpressionToMultiSelectList();

            scanner.AddPushbackBufferToQueue();
        }

        bool IsParsingMultiSelectList()
        {
            var scanner = this.Scanner as JmesPathScanner;
            System.Diagnostics.Debug.Assert(scanner != null);

            // this method is called upon each successfull parse of an expression
            //
            // (T_LBRACKET) T_STAR T_RBRACKET => list_wildcard => bracket_specifier
            // (T_LBRACKET) T_STAR ... <any>  => hash_wildcard => in the context of a multi_select_list
            // (T_LBRACKET) T_COLON           => slice_expression => bracket_specifier
            // (T_LBRACKET) T_NUMBER          => number => bracket_specifier
            // (T_LBRACKET) <any>             => multi_select_list

            var next = scanner.GetAndEnqueue();

            switch (next)
            {
                case (int)TokenType.T_STAR:
                    {
                        var lookahead = scanner.GetAndEnqueue();
                        if (lookahead != (int)TokenType.T_RBRACKET)
                            return true;
                    }
                    break;

                case (int)TokenType.T_COLON:
                case (int)TokenType.T_NUMBER:
                    {
                        // next construct is a bracket specifier
                        // do nothing
                    }
                    break;

                // otherwise, resolve to a multi_select_list

                default:
                    return true;
            }

            return false;
        }

        void ResolveSubExpressionToMultiSelectList()
        {
            // next construct is a multi_select_list
            // check that the previous construct is a projection
            // and prepend a "." to parse as a sub_expression

            NextToken = (int)TokenType.T_DOT;
        }

        #endregion

        #region Expressions

        void OnSubExpression() => generator_.OnSubExpression();

        #region index_expression

        void OnIndex(Token token)
        {
            var number = token as NumberToken;

            System.Diagnostics.Debug.Assert(token.Type == TokenType.T_NUMBER);
            System.Diagnostics.Debug.Assert(number != null);

            var index = (int)number.Value;
            generator_.OnIndex(index);
        }

        void OnFilterProjection() => generator_.OnFilterProjection();

        void OnFlattenProjection() => generator_.OnFlattenProjection();

        void OnListWildcardProjection() => generator_.OnListWildcardProjection();

        void OnIndexExpression() => generator_.OnIndexExpression();

        void OnSliceExpression(Token start, Token stop, Token step)
        {
            System.Diagnostics.Debug.Assert(start == null || start is NumberToken);
            System.Diagnostics.Debug.Assert(stop == null || stop is NumberToken);
            System.Diagnostics.Debug.Assert(step == null || step is NumberToken);

            var startIndex = (int?)start?.Value;
            var stopIndex = (int?)stop?.Value;
            var stepIndex = (int?)step?.Value;

            generator_.OnSliceExpression(startIndex, stopIndex, stepIndex);
        }

        #endregion

        #region comparator_expression

        void OnComparisonExpression(Token token)
        {
            switch (token.Type)
            {
                case TokenType.T_EQ: generator_.OnComparisonEqual(); break;
                case TokenType.T_GE: generator_.OnComparisonGreaterOrEqual(); break;
                case TokenType.T_GT: generator_.OnComparisonGreater(); break;
                case TokenType.T_LE: generator_.OnComparisonLesserOrEqual(); break;
                case TokenType.T_LT: generator_.OnComparisonLesser(); break;
                case TokenType.T_NE: generator_.OnComparisonNotEqual(); break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    throw new NotSupportedException();
            }
        }

        #endregion

        #region arithmetic_expression

        void OnArithmeticUnaryPlus() => generator_.OnArithmeticUnaryPlus();
        void OnArithmeticUnaryMinus() => generator_.OnArithmeticUnaryMinus();
        void OnArithmeticAddition() => generator_.OnArithmeticAddition();
        void OnArithmeticSubtraction() => generator_.OnArithmeticSubtraction();
        void OnArithmeticMultiplication() => generator_.OnArithmeticMultiplication();
        void OnArithmeticDivision() => generator_.OnArithmeticDivision();
        void OnArithmeticModulo() => generator_.OnArithmeticModulo();
        void OnArithmeticIntegerDivision() => generator_.OnArithmeticIntegerDivision();

        #endregion

        #region logical_expression
        void OnOrExpression() => generator_.OnOrExpression();

        void OnAndExpression() => generator_.OnAndExpression();

        void OnNotExpression() => generator_.OnNotExpression();

        #endregion

        void OnIdentifier(Token token) =>
            generator_.OnIdentifier((string)token.Value);

        void OnHashWildcardProjection() =>
            generator_.OnHashWildcardProjection();

        #region multi_select_hash

        void PushMultiSelectHash() => generator_.PushMultiSelectHash();
        void AddMultiSelectHashExpression() => generator_.AddMultiSelectHashExpression();
        void PopMultiSelectHash() => generator_.PopMultiSelectHash();

        #endregion

        #region multi_select_list

        void PushMultiSelectList() => generator_.PushMultiSelectList();
        void AddMultiSelectListExpression() => generator_.AddMultiSelectListExpression();
        void PopMultiSelectList() => generator_.PopMultiSelectList();

        #endregion

        void OnLiteralString(Token token) =>
            generator_.OnLiteralString((string)((LiteralStringToken)token).Value);

        void OnPipeExpression() => generator_.OnPipeExpression();

        #region function_expression

        void PushFunction() => generator_.PushFunction();

        void PopFunction(Token token)
        {
            System.Diagnostics.Debug.Assert(token.Type == TokenType.T_USTRING);

            var name = (string)token.Value;
            generator_.PopFunction(name);
        }

        void AddFunctionArg() => generator_.AddFunctionArg();

        void OnExpressionType() => generator_.OnExpressionType();

        #endregion

        void OnRawString(Token token)
        {
            var @string = (string)token.Value;
            generator_.OnRawString(@string);
        }

        void OnCurrentNode() => generator_.OnCurrentNode();
        void OnRootNode() => generator_.OnRootNode();

        #endregion // Expressions
    }
}
