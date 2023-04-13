using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Interop;

namespace DevLab.JmesPath
{
    sealed class JmesPathGenerator : IJmesPathGenerator
    {
        /// <summary>
        /// holds the functions available to the parser
        /// </summary>
        readonly IFunctionRepository repository_;

        readonly Stack<IDictionary<string, JmesPathExpression>> selectHashes_
            = new Stack<IDictionary<string, JmesPathExpression>>()
            ;

        readonly Stack<IList<JmesPathExpression>> selectLists_
            = new Stack<IList<JmesPathExpression>>()
            ;

        readonly Stack<IList<JmesPathExpression>> functions_
            = new Stack<IList<JmesPathExpression>>();

        readonly Stack<IList<JmesPathBinding>> bindings_
            = new Stack<IList<JmesPathBinding>>()
            ;

        readonly Stack<JmesPathExpression> expressions_
            = new Stack<JmesPathExpression>()
            ;

        IList<JmesPathBinding> currentBindings_ = new List<JmesPathBinding>();

        public JmesPathGenerator(IFunctionRepository repository)
        {
            repository_ = repository;
        }

        public JmesPathExpression Expression =>
            expressions_.Peek();

        public void OnExpression() { }

        public bool IsProjection()
        {
            if (expressions_.Count == 0)
                return false;

            return expressions_.Peek() is JmesPathProjection;
        }

        #region Expressions

        public void OnSubExpression()
            => PopPush((left, right) => new JmesPathSubExpression(left, right));

        #region index_expression

        public void OnIndex(int index)
            => Push(new JmesPathIndex(index));

        public void OnFilterProjection()
            => PopPush(e => new JmesPathFilterProjection(e));

        static readonly JmesPathFlattenProjection JmesPathFlattenProjection = new JmesPathFlattenProjection();
        public void OnFlattenProjection()
            => Push(JmesPathFlattenProjection);


        static readonly JmesPathListWildcardProjection JmesPathListWildcardProjection = new JmesPathListWildcardProjection();
        public void OnListWildcardProjection()
            => Push(JmesPathListWildcardProjection);


        public void OnIndexExpression()
            => PopPush((left, right) => new JmesPathIndexExpression(left, right));

        public void OnSliceExpression(int? start, int? stop, int? step)
            => Push(new JmesPathSliceProjection(start, stop, step));

        #endregion

        #region comparator_expression

        public void OnComparisonEqual() =>
            PopPush((left, right) => new JmesPathEqualOperator(left, right));

        public void OnComparisonNotEqual() =>
            PopPush((left, right) => new JmesPathNotEqualOperator(left, right));

        public void OnComparisonGreaterOrEqual() =>
            PopPush((left, right) => new JmesPathGreaterThanOrEqualOperator(left, right));

        public void OnComparisonGreater() =>
            PopPush((left, right) => new JmesPathGreaterThanOperator(left, right));

        public void OnComparisonLesserOrEqual() =>
            PopPush((left, right) => new JmesPathLessThanOrEqualOperator(left, right));

        public void OnComparisonLesser() =>
            PopPush((left, right) => new JmesPathLessThanOperator(left, right));

        #endregion

        #region arithmetic_expression

        public void OnArithmeticUnaryPlus()
            => PopPush(e => new JmesPathUnaryPlusExpression(e));

        public void OnArithmeticUnaryMinus()
            => PopPush(e => new JmesPathUnaryMinusExpression(e));

        public void OnArithmeticAddition()
            => PopPush((left, right) => new JmesPathAdditionExpression(left, right));
        public void OnArithmeticSubtraction()
            => PopPush((left, right) => new JmesPathSubtractionExpression(left, right));
        public void OnArithmeticMultiplication()
            => PopPush((left, right) => new JmesPathMultiplicationExpression(left, right));
        public void OnArithmeticDivision()
            => PopPush((left, right) => new JmesPathDivisionExpression(left, right));
        public void OnArithmeticModulo()
            => PopPush((left, right) => new JmesPathModuloExpression(left, right));
        public void OnArithmeticIntegerDivision()
            => PopPush((left, right) => new JmesPathIntegerDivisionExpression(left, right));

        #endregion

        #region logical_expression

        public void OnOrExpression() =>
            PopPush((left, right) => new JmesPathOrExpression(left, right));

        public void OnAndExpression() =>
            PopPush((left, right) => new JmesPathAndExpression(left, right));

        public void OnNotExpression() =>
            PopPush(e => new JmesPathNotExpression(e));

        #endregion

        #region let_expression

        public void OnLetExpression()
            => Push(() =>
            {
                System.Diagnostics.Debug.Assert(bindings_.Count >= 1);
                System.Diagnostics.Debug.Assert(expressions_.Count >= 1);

                var bindings = bindings_.Pop();
                var expression = expressions_.Pop();
                return new JmesPathLetExpression(bindings, expression);
            });

        public void OnLetBindings()
        {
            var bindings = new List<JmesPathBinding>(currentBindings_);
            currentBindings_.Clear();
            bindings_.Push(bindings);
        }

        public void OnLetBinding(string name)
        {
            System.Diagnostics.Debug.Assert(expressions_.Count >= 1);
            var expression = expressions_.Pop();
            var binding = new JmesPathBinding(name, expression);

            currentBindings_.Add(binding);
        }

        #endregion

        #region variable_ref

        public void OnVariableReference(string name)
            => Push(new JmesPathVariableReference(name));

        #endregion

        public void OnIdentifier(string name)
            => Push(new JmesPathIdentifier(name));

        static readonly JmesPathHashWildcardProjection JmesPathHashWildcardProjection = new JmesPathHashWildcardProjection();

        public void OnHashWildcardProjection()
            => Push(JmesPathHashWildcardProjection);

        #region multi_select_hash

        public void PushMultiSelectHash()
            => selectHashes_.Push(new Dictionary<string, JmesPathExpression>());

        public void AddMultiSelectHashExpression()
        {
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

        public void PopMultiSelectHash()
            => Push(() => {
                System.Diagnostics.Debug.Assert(selectHashes_.Count > 0);
                var items = selectHashes_.Pop();
                return new JmesPathMultiSelectHash(items);
            });

        #endregion

        #region multi_select_list

        public void PushMultiSelectList()
            => selectLists_.Push(new List<JmesPathExpression>());

        public void AddMultiSelectListExpression()
        {
            System.Diagnostics.Debug.Assert(selectLists_.Count > 0);
            var expression = expressions_.Pop();
            var items = selectLists_.Peek();
            items.Add(expression);
        }

        public void PopMultiSelectList()
            => Push(() => {
                System.Diagnostics.Debug.Assert(selectLists_.Count > 0);
                var items = selectLists_.Pop();
                return new JmesPathMultiSelectList(items);
            });

        #endregion

        public void OnLiteralString(string literal)
        {
            var token = JToken.Parse(literal);
            var expression = new JmesPathLiteral(token);
            expressions_.Push(expression);
        }

        public void OnPipeExpression()
            => PopPush((left, right) => new JmesPathPipeExpression(left, right));

        #region function_expression

        public void PushFunction()
            => functions_.Push(new List<JmesPathExpression>());

        public void PopFunction(string name)
            => Push(() =>
            {
                System.Diagnostics.Debug.Assert(functions_.Count > 0);

                var args = functions_.Pop();
                var expressions = args.ToArray();

                return new JmesPathFunctionExpression(repository_, name, expressions);
            });

        public void AddFunctionArg()
        {
            System.Diagnostics.Debug.Assert(functions_.Count > 0);

            var expression = expressions_.Pop();
            functions_.Peek().Add(expression);
        }

        public void OnExpressionType()
        {
            var expression = expressions_.Pop();
            JmesPathExpression.MakeExpressionType(expression);
            expressions_.Push(expression);
        }

        #endregion

        public void OnRawString(string value)
            => Push(new JmesPathRawString(value));

        public void OnCurrentNode()
            => Push<JmesPathCurrentNodeExpression>();

        public void OnRootNode()
            => Push<JmesPathRootNodeExpression>();

        #endregion // Expressions

        void Push<T>()
            where T : JmesPathExpression
            => Push(() => (T) Activator.CreateInstance(typeof(T)));

        void Push<T>(Func<T> factory)
            where T : JmesPathExpression
            => Push(factory());

        void Push(JmesPathExpression expression)
            => expressions_.Push(expression);

        void PopPush<T>(Func<JmesPathExpression, T> factory)
            where T : JmesPathExpression
        {
            System.Diagnostics.Debug.Assert(expressions_.Count >= 1);

            var arg = expressions_.Pop();
            expressions_.Push(factory(arg));
        }

        void PopPush<T>(Func<JmesPathExpression, JmesPathExpression, T> factory)
            where T : JmesPathExpression
        {
            System.Diagnostics.Debug.Assert(expressions_.Count >= 2);

            var right = expressions_.Pop();
            var left = expressions_.Pop();

            expressions_.Push(factory(left, right));
        }
    }
}
