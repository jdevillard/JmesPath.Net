using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

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

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var array = (JArray) args[0].Token;

            if (array.Count == 0)
                return JTokens.Null;

            var item = array[0];
            var dataTypes = array.Select(t => t.GetTokenType()).Distinct();

            return Execute(array, dataTypes);
        }

        protected abstract JToken Execute(JArray array, IEnumerable<string> dataTypes);
    }
}