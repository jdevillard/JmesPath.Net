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

        private void OnIndexExpression()
        {
            var specifier = expressions_.Pop();
            var expression = expression_;

            System.Diagnostics.Debug.Assert(specifier is JmesPathSliceExpression);

            var indexExpression = new JmesPathIndexExpression(expression, specifier);

            expressions_.Push(indexExpression);
        }

        private void OnSubExpression()
        {
            var mainExpression = expression_;
            var nestedExpression = expressions_.Pop();

            var subExpression = new JmesPathSubExpression(mainExpression, nestedExpression);

            expressions_.Push(subExpression);
            expression_ = null;
        }

        private void OnBracketSpecifier(Token token)
        {
            System.Diagnostics.Debug.Assert(token.Type == TokenType.T_NUMBER);
            var number = (int) token.Value;

            var index = new JmesPathNumber(number);
            var expression = new JmesPathSliceExpression(index);
            expressions_.Push(expression);
        }

        private void OnIdentifier(Token token)
        {
            var @string = (string) token.Value;
            var expression = new JmesPathIdentifier(@string);
            expressions_.Push(expression);
        }
        private void OnRawString(Token token)
        {
            var @string = (string)token.Value;
            var expression = new JmesPathRawString(@string);
            expressions_.Push(expression);
        }

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
    }
}