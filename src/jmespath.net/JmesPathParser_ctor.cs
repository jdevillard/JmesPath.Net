using System.Collections.Generic;
using DevLab.JmesPath.Expressions;
using StarodubOleg.GPPG.Runtime;

namespace DevLab.JmesPath
{
    public partial class JmesPathParser
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
            expression_ = expressions_.Pop();
        }

        private void OnSubExpression()
        {
            var mainExpression = expression_;
            var nestedExpression = expressions_.Pop();

            var subExpression = new JmesPathSubExpression(mainExpression, nestedExpression);

            expressions_.Push(subExpression);
            expression_ = null;
        }

        private void OnIdentifier(Token token)
        {
            var @string = (string)token.Value;
            var expression = new JmesPathIdentifier(@string);
            expressions_.Push(expression);
        }

        private void OnRawString(Token token)
        {
            var @string = (string)token.Value;
            var expression = new JmesPathRawString(@string);
            expressions_.Push(expression);
        }

        #region index_expression

        private void OnIndex(Token token)
        {
            var number = token as NumberToken;

            System.Diagnostics.Debug.Assert(token.Type == TokenType.T_NUMBER);
            System.Diagnostics.Debug.Assert(number != null);

            var index = (int)number.Value;

            var expression = new JmesPathIndex(index);

            expressions_.Push(expression);
        }

        private void OnIndexExpression()
        {
            var expression = expression_;
            var specifier = expressions_.Pop();

            var indexExpression = new JmesPathIndexExpression(expression, specifier);

            expressions_.Push(indexExpression);
        }

        private void OnSliceExpression(Token start, Token stop, Token step)
        {
            System.Diagnostics.Debug.Assert(start == null || start is NumberToken);
            System.Diagnostics.Debug.Assert(stop == null || stop is NumberToken);
            System.Diagnostics.Debug.Assert(step == null || step is NumberToken);

            var startIndex = (int?)start?.Value;
            var stopIndex = (int?)stop?.Value;
            var stepIndex = (int?)step?.Value;

            var sliceExpression = new JmesPathSliceExpression(startIndex, stopIndex, stepIndex);

            expressions_.Push(sliceExpression);
        }

        #endregion

        #region multi_select_hash

        private void PushMultiSelectHash()
        {
            selectHashes_.Push(new Dictionary<string, JmesPathExpression>());
        }

        private void AddMultiSelectHashExpression()
        {
            System.Diagnostics.Debug.Assert(expression_ != null);
            System.Diagnostics.Debug.Assert(expressions_.Count > 0);
            System.Diagnostics.Debug.Assert(selectHashes_.Count > 0);

            var identifier = expressions_.Pop() as JmesPathIdentifier;
            System.Diagnostics.Debug.Assert(identifier != null);
            var name = identifier.Name;
            System.Diagnostics.Debug.Assert(name != null);

            var items = selectHashes_.Peek();
            items.Add(name, expression_);
            expression_ = null;
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
            System.Diagnostics.Debug.Assert(expression_ != null);
            System.Diagnostics.Debug.Assert(selectLists_.Count > 0);
            var items = selectLists_.Peek();
            items.Add(expression_);
            expression_ = null;
        }

        private void PopMultiSelectList()
        {
            System.Diagnostics.Debug.Assert(selectLists_.Count > 0);
            var items = selectLists_.Pop();
            var expression = new JmesPathMultiSelectList(items);

            expressions_.Push(expression);
        }

        #endregion
    }
}