using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Tokens;
using StarodubOleg.GPPG.Runtime;

namespace DevLab.JmesPath
{
    internal partial class JmesPathParser
    {
        private readonly Stack<IDictionary<string, JmesPathExpression>> selectHashes_
            = new Stack<IDictionary<string, JmesPathExpression>>()
            ;

        private readonly Stack<IList<JmesPathExpression>> selectLists_
            = new Stack<IList<JmesPathExpression>>()
            ;

        private readonly Stack<JmesPathExpression> expressions_
            = new Stack<JmesPathExpression>()
            ;

        private JmesPathExpression expression_;

        public JmesPathExpression Expression => expression_;

        public JmesPathParser(AbstractScanner<ValueType, LexLocation> scanner)
            : base(scanner)
        {
        }

        private void OnExpression()
        {
            if (expression_ == null)
                expression_ = expressions_.Pop();
        }

        #region Implementation

        private void Prolog()
        {
            if (expression_ != null)
                expressions_.Push(expression_);
            expression_ = null;
        }

        #endregion

        private void OnIdentifier(Token token)
        {
            Prolog();

            var @string = (string)token.Value;
            var expression = new JmesPathIdentifier(@string);
            expressions_.Push(expression);
        }

        private void OnLiteralString(Token token)
        {
            Prolog();

            var @string = (JToken)token.Value;
            var expression = new JmesPathLiteral(@string);
            expressions_.Push(expression);
        }

        private void OnRawString(Token token)
        {
            Prolog();

            var @string = (string)token.Value;
            var expression = new JmesPathRawString(@string);
            expressions_.Push(expression);
        }

        private void OnSubExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);

            var right = expressions_.Pop();
            var left = expressions_.Pop();

            var expression = new JmesPathSubExpression(left, right);

            expressions_.Push(expression);
        }

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

        private void OnPipeExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);

            var right = expressions_.Pop();
            var left = expressions_.Pop();

            var expression = new JmesPathPipeExpression(left, right);

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

            if (stepIndex.HasValue && stepIndex.Value == 0)
                throw new ArgumentException("Error: invalid-value. A slice expression step number cannot be 0.");

            var sliceExpression = new JmesPathSliceProjection(startIndex, stopIndex, stepIndex);

            expressions_.Push(sliceExpression);
        }

        private void OnFilterExpression()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(expressions_.Count >= 1);

            var comparison = expressions_.Pop();
            var expression = new JmesPathFilterProjection(comparison);

            expressions_.Push(expression);
        }

        #endregion

        #region function
        private readonly Stack<IList<JmesPathExpression>> functions_
            = new Stack<IList<JmesPathExpression>>();

        private void PushFunction()
        {
            functions_.Push(new List<JmesPathExpression>());
        }
        private void PopFunction(Token token)
        {
            System.Diagnostics.Debug.Assert(token.Type == TokenType.T_USTRING);
            System.Diagnostics.Debug.Assert(functions_.Count > 0);

            var args = functions_.Pop();
            var expression = new JmesPathFunction((string) token.Value, args.ToArray());
            expressions_.Push(expression);
        }

        private void AddFunctionArg()
        {
            Prolog();

            System.Diagnostics.Debug.Assert(functions_.Count > 0);

            var expression = expressions_.Pop();
            functions_.Peek().Add(expression);
        }

        #endregion 

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

        #region projections

        private void OnFlattenProjection()
        {
            Prolog();

            expressions_.Push(new JmesPathFlattenProjection());
        }

        private void OnHashWildcardProjection()
        {
            Prolog();

            expressions_.Push(new JmesPathHashWildcardProjection());
        }

        private void OnListWildcardProjection()
        {
            Prolog();

            expressions_.Push(new JmesPathListWildcardProjection());
        }

        #endregion
    }
}