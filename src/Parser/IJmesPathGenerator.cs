namespace DevLab.JmesPath
{
    public interface IJmesPathGenerator
    {
        void OnExpression();
        bool IsProjection();
        void OnSubExpression();
        void OnIndex(int index);
        void OnFilterProjection();
        void OnFlattenProjection();
        void OnListWildcardProjection();
        void OnIndexExpression();
        void OnSliceExpression(int? start, int? stop, int? step);
        void OnComparisonEqual();
        void OnComparisonNotEqual();
        void OnComparisonGreaterOrEqual();
        void OnComparisonGreater();
        void OnComparisonLesserOrEqual();
        void OnComparisonLesser();
        void OnOrExpression();
        void OnAndExpression();
        void OnNotExpression();
        void OnIdentifier(string name);
        void OnHashWildcardProjection();
        void PushMultiSelectHash();
        void AddMultiSelectHashExpression();
        void PopMultiSelectHash();
        void PushMultiSelectList();
        void AddMultiSelectListExpression();
        void PopMultiSelectList();
        void OnLiteralString(string literal);
        void OnPipeExpression();
        void PushFunction();
        void PopFunction(string name);
        void AddFunctionArg();
        void OnExpressionType();
        void OnRawString(string value);
        void OnCurrentNode();
    }
}
