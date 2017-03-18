namespace DevLab.JmesPath.Functions
{
    public abstract class MathArrayFunction : JmesPathFunction
    {
        protected MathArrayFunction(string name, int count) 
            : this(name, count, false)
        {
        }

        protected MathArrayFunction(string name, int minCount, bool variadic) 
            : base(name, minCount, variadic)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureArrayOfSame(args[0], "number", "string");
        }
    }
}