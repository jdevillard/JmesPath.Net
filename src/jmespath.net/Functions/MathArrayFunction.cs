using System;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public abstract class MathArrayFunction : JmesPathFunction
    {
        protected MathArrayFunction(string name, int count) 
            : base(name, count)
        {
        }

        protected MathArrayFunction(string name, int minCount, bool variadic) 
            : base(name, minCount, variadic)
        {
        }

        public override bool Validate(params JmesPathArgument[] args)
        {
            if (args[0].Token.Type != JTokenType.Array)
                throw new Exception("invalid-type");

            foreach (var item in (JArray)args[0].Token)
                if (item.Type != JTokenType.Integer && item.Type != JTokenType.Float)
                    throw new Exception("invalid-type");

            return true;
        }
    }
}