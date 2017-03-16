using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Tokens;
using StarodubOleg.GPPG.Runtime;
using JmesPathFunction = DevLab.JmesPath.Expressions.JmesPathFunction;

namespace DevLab.JmesPath
{
    internal partial class JmesPathParser
    {
        /// <summary>
        /// holds the functions available to the parser
        /// </summary>
        private readonly IFunctionRepository repository_;

        private readonly Stack<IDictionary<string, JmesPathExpression>> selectHashes_
            = new Stack<IDictionary<string, JmesPathExpression>>()
            ;

        private readonly Stack<IList<JmesPathExpression>> selectLists_
            = new Stack<IList<JmesPathExpression>>()
            ;

        private readonly Stack<IList<JmesPathExpression>> functions_
            = new Stack<IList<JmesPathExpression>>();

        private readonly Stack<JmesPathExpression> expressions_
            = new Stack<JmesPathExpression>()
            ;

        private JmesPathExpression expression_;

        public JmesPathExpression Expression => expression_;

        public JmesPathParser(AbstractScanner<ValueType, LexLocation> scanner, IFunctionRepository repository)
            : base(scanner)
        {
            repository_ = repository;
        }

        #region Implementation

        private void OnExpression()
        {
            if (expression_ == null)
                expression_ = expressions_.Pop();
        }

        private bool Prolog()
        {
            if (expression_ != null)
            {
                expressions_.Push(expression_);
                expression_ = null;
                return true;
            }

            return false;
        }

        private void ResolveParsingState()
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

            var pop = Prolog();

            try
            {
                if (expressions_.Count == 0)
                    return;

                // only interested in cases where previous construct is a projection

                var expression = expressions_.Peek();
                if (!(expression is JmesPathProjection))
                    return;

                var scanner = this.Scanner as JmesPathScanner;
                System.Diagnostics.Debug.Assert(scanner != null);

                var next = scanner.EnqueueAndReturnInitialToken(NextToken);
                NextToken = 0;

                if (next == (int)TokenType.T_LBRACKET && IsParsingMultiSelectList())
                    ResolveSubExpressionToMultiSelectList();

                scanner.AddPushbackBufferToQueue();
            }
            finally
            {
                if (pop)
                    OnExpression();
            }
        }

        private bool IsParsingMultiSelectList()
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

        private void ResolveSubExpressionToMultiSelectList()
        {
            // next construct is a multi_select_list
            // check that the previous construct is a projection
            // and prepend a "." to parse as a sub_expression

            NextToken = (int)TokenType.T_DOT;
        }

        #endregion

        #region Expressions

        private void OnSubExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);

            var right = expressions_.Pop();
            var left = expressions_.Pop();

            var expression = new JmesPathSubExpression(left, right);

            expressions_.Push(expression);
        }
        
        #region index_expression

        private void OnIndex(Token token)
        {
            Prolog();

            var number = token as NumberToken;

            System.Diagnostics.Debug.Assert(token.Type == TokenType.T_NUMBER);
            System.Diagnostics.Debug.Assert(number != null);

            var index = (int)number.Value;

            var expression = new JmesPathIndex(index);

            expressions_.Push(expression);
        }

        private void OnFilterProjection()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 1);

            var comparison = expressions_.Pop();
            var expression = new JmesPathFilterProjection(comparison);

            expressions_.Push(expression);
        }

        private void OnFlattenProjection()
        {
            Prolog();

            expressions_.Push(new JmesPathFlattenProjection());
        }

        private void OnListWildcardProjection()
        {
            Prolog();

            expressions_.Push(new JmesPathListWildcardProjection());
        }

        private void OnIndexExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);

            var right = expressions_.Pop();
            var left = expressions_.Pop();

            var expression = new JmesPathIndexExpression(left, right);

            expressions_.Push(expression);
        }

        private void OnSliceExpression(Token start, Token stop, Token step)
        {
            Prolog();

            System.Diagnostics.Debug.Assert(start == null || start is NumberToken);
            System.Diagnostics.Debug.Assert(stop == null || stop is NumberToken);
            System.Diagnostics.Debug.Assert(step == null || step is NumberToken);

            var startIndex = (int?)start?.Value;
            var stopIndex = (int?)stop?.Value;
            var stepIndex = (int?)step?.Value;

            var sliceExpression = new JmesPathSliceProjection(startIndex, stopIndex, stepIndex);

            expressions_.Push(sliceExpression);
        }

        #endregion

        #region comparator_expression

        private void OnComparisonExpression(Token token)
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);

            var right = expressions_.Pop();
            var left = expressions_.Pop();

            JmesPathExpression expression;

            switch (token.Type)
            {
                case TokenType.T_EQ:
                    expression = new JmesPathEqualOperator(left, right);
                    break;
                case TokenType.T_GE:
                    expression = new JmesPathGreaterThanOrEqualOperator(left, right);
                    break;
                case TokenType.T_GT:
                    expression = new JmesPathGreaterThanOperator(left, right);
                    break;
                case TokenType.T_LE:
                    expression = new JmesPathLessThanOrEqualOperator(left, right);
                    break;
                case TokenType.T_LT:
                    expression = new JmesPathLessThanOperator(left, right);
                    break;
                case TokenType.T_NE:
                    expression = new JmesPathNotEqualOperator(left, right);
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    throw new NotSupportedException();
            }

            expressions_.Push(expression);
        }

        #endregion

        private void OnOrExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);

            var right = expressions_.Pop();
            var left = expressions_.Pop();

            var expression = new JmesPathOrExpression(left, right);

            expressions_.Push(expression);
        }

        private void OnAndExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);

            var right = expressions_.Pop();
            var left = expressions_.Pop();

            var expression = new JmesPathAndExpression(left, right);

            expressions_.Push(expression);
        }

        private void OnNotExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 1);

            var expression = expressions_.Pop();
            var negated = new JmesPathNotExpression(expression);

            expressions_.Push(negated);
        }

        private void OnIdentifier(Token token)
        {
            Prolog();

            var @string = (string)token.Value;
            var expression = new JmesPathIdentifier(@string);
            expressions_.Push(expression);
        }

        private void OnHashWildcardProjection()
        {
            Prolog();

            expressions_.Push(new JmesPathHashWildcardProjection());
        }

        #region multi_select_hash

        private void PushMultiSelectHash()
        {
            selectHashes_.Push(new Dictionary<string, JmesPathExpression>());
        }

        private void AddMultiSelectHashExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);
            System.Diagnostics.Debug.Assert(selectHashes_.Count > 0);

            var expression = expressions_.Pop();

            var identifier = expressions_.Pop() as JmesPathIdentifier;
            System.Diagnostics.Debug.Assert(identifier != null);
            var name = identifier.Name;
            System.Diagnostics.Debug.Assert(name != null);

            var items = selectHashes_.Peek();
            items.Add(name, expression);
        }

        private void PopMultiSelectHash()
        {
            System.Diagnostics.Debug.Assert(selectHashes_.Count > 0);
            var items = selectHashes_.Pop();
            var expression = new JmesPathMultiSelectHash(items);
            expressions_.Push(expression);
        }

        #endregion

        #region multi_select_list

        private void PushMultiSelectList()
        {
            selectLists_.Push(new List<JmesPathExpression>());
        }

        private void AddMultiSelectListExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(selectLists_.Count > 0);
            var expression = expressions_.Pop();
            var items = selectLists_.Peek();
            items.Add(expression);
        }

        private void PopMultiSelectList()
        {
            System.Diagnostics.Debug.Assert(selectLists_.Count > 0);
            var items = selectLists_.Pop();
            var expression = new JmesPathMultiSelectList(items);

            expressions_.Push(expression);
        }

        #endregion

        private void OnLiteralString(Token token)
        {
            Prolog();

            var @string = (JToken)token.Value;
            var expression = new JmesPathLiteral(@string);
            expressions_.Push(expression);
        }

        private void OnPipeExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);

            var right = expressions_.Pop();
            var left = expressions_.Pop();

            var expression = new JmesPathPipeExpression(left, right);

            expressions_.Push(expression);
        }

        #region function_expression

        private void PushFunction()
        {
            functions_.Push(new List<JmesPathExpression>());
        }
        private void PopFunction(Token token)
        {
            System.Diagnostics.Debug.Assert(token.Type == TokenType.T_USTRING);
            System.Diagnostics.Debug.Assert(functions_.Count > 0);

            var args = functions_.Pop();
            var name = (string)token.Value;
            var expressions = args.ToArray();

            var expression = new JmesPathFunction(repository_, name, expressions);

            expressions_.Push(expression);
        }

        private void AddFunctionArg()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(functions_.Count > 0);

            var expression = expressions_.Pop();
            functions_.Peek().Add(expression);
        }
        private void OnExpressionType()
        {
            Prolog();
            var expression = expressions_.Pop();
            var expression_type = new JmesPathExpressionType(expression);
            expressions_.Push(expression_type);
        }
        #endregion 

        private void OnRawString(Token token)
        {
            Prolog();

            var @string = (string)token.Value;
            var expression = new JmesPathRawString(@string);
            expressions_.Push(expression);
        }

        private void OnCurrentNode()
        {
            Prolog();

            expressions_.Push(new JmesPathCurrentNodeExpression());
        }

        #endregion // Expressions
    }
}