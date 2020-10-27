namespace DevLab.JmesPath
{
    public interface IJmesPathGenerator
    {
        bool IsProjection();

        void OnExpression();

        void OnIdentifier(string name);
        void OnLiteralString(string literal);
        void OnRawString(string value);

        void OnAndExpression();
        void OnNotExpression();
        void OnOrExpression();

        void OnComparisonEqual();
        void OnComparisonGreater();
        void OnComparisonGreaterOrEqual();
        void OnComparisonLesser();
        void OnComparisonLesserOrEqual();
        void OnComparisonNotEqual();

        void OnCurrentNode();

        void OnSubExpression();

        void OnFilterProjection();
        void OnFlattenProjection();
        void OnHashWildcardProjection();
        void OnListWildcardProjection();

        void OnIndex(int index);
        void OnIndexExpression();
        void OnSliceExpression(int? start, int? stop, int? step);

        void PushMultiSelectHash();
        void AddMultiSelectHashExpression();
        void PopMultiSelectHash();

        void PushMultiSelectList();
        void AddMultiSelectListExpression();
        void PopMultiSelectList();

        void PushFunction();
        void AddFunctionArg();
        void PopFunction(string name);
        void OnExpressionType();

        void OnPipeExpression();
        void OnBlockAssignment(string name);
        void OnBlock();
    }
}
