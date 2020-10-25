using System;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class VariableFunction : JmesPathFunction
    {
        public VariableFunction()
            : base("variable", 1)
        {

        }
        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureArrayOrString(args[0]);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 1);
            System.Diagnostics.Debug.Assert(args[0].IsToken);

            var varName = args[0].Token.Value<string>();

            if (this.Context.Variable.ContainsKey(varName))
                return this.Context.Variable[varName];
            return JTokens.Null;

        }
    }
}