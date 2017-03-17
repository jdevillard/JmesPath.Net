using System;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public abstract class ByFunction : JmesPathFunction
    {
        protected ByFunction(string name)
            : base(name, 2)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            var array = args[0].Token;
            if (array.Type != JTokenType.Array)
                throw new Exception($"Error: invalid-type, function {Name} expects its first argument to be an array.");

            if (!args[1].IsExpressionType)
                throw new Exception($"Error: invalid-type, function {Name} expects its second argument to be an expression type.");
        }
    }
}