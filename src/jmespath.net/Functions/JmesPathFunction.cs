using System;
using System.Linq;
using System.Text;
using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public abstract class JmesPathFunction
    {
        protected readonly IScopeParticipant scopes_;
        private JToken context_ = null;

        protected JmesPathFunction(string name, int count)
            : this(name, count, null, false, null)
        { }
        protected JmesPathFunction(string name, int count, IScopeParticipant scopes)
            : this(name, count, null, false, scopes)
        { }

        protected JmesPathFunction(string name, int minCount, bool variadic)
            : this(name, minCount, null, variadic, null)
        { }

        protected JmesPathFunction(string name, int minCount, int maxCount)
            : this(name, minCount, maxCount, false, null)
        { }

        protected JmesPathFunction(string name, int minCount, int? maxCount, bool variadic, IScopeParticipant scopes)
        {
            Name = name;
            MinArgumentCount = minCount;
            MaxArgumentCount = maxCount;

            Variadic = variadic;

            System.Diagnostics.Debug.Assert(maxCount == null || !variadic);

            scopes_ = scopes;
        }

        public string Name { get; private set; }

        public int MinArgumentCount { get; }
        public int? MaxArgumentCount { get; }

        public bool Variadic { get; }

        public IScopeParticipant Scopes
            => scopes_;

        protected JToken Context
            => context_;

        /// <summary>
        /// Called when expression arguments have been evaluated prior to function evaluation.
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception"></exception>
        public virtual void Validate(params JmesPathFunctionArgument[] args)
        {
            if (args.Any(a => a.IsExpressionType))
                throw new Exception($"Error: invalid-type, unexpected expression type as a parameter to the function {Name}.");
        }

        public abstract JToken Execute(params JmesPathFunctionArgument[] args);

        protected static bool IsInteger(double sum)
        {
            return Math.Abs(sum % 1) <= (Double.Epsilon * 100);
        }

        protected string EnsureString(JmesPathFunctionArgument argument)
            => (EnsureOf(argument, "string") as JValue).Value<string>();

        protected JObject EnsureObject(JmesPathFunctionArgument argument)
        {
            return EnsureOf(argument, "object") as JObject;
        }

        protected JArray EnsureArray(JmesPathFunctionArgument argument)
        {
            return EnsureOf(argument, "array") as JArray;
        }

        protected JArray EnsureArrayOf(JmesPathFunctionArgument argument, string type)
        {
            return EnsureArrayOfAny(argument, new[] { type });
        }

        protected JArray EnsureArrayOfAny(JmesPathFunctionArgument argument, params string[] types)
        {
            var array = EnsureArray(argument);

            foreach (var item in array)
                if (!types.Contains(item.GetTokenType()))
                {
                    var valid = FormatAllowedDataTypes(types);
                    throw new Exception($"Error: invalid-type, function {Name} expects an array of {valid}.");
                }

            return array;
        }

        protected JArray EnsureArrayOfSame(JmesPathFunctionArgument argument, params string[] types)
        {
            var array = EnsureArray(argument);

            var dataTypes = array.Select(t => t.GetTokenType()).Distinct().ToArray();
            if (dataTypes.Length > 1)
                throw new Exception($"Error: invalid-type, all items in the array to the function {Name} must have the same type.");

            if (types.Length > 0 && dataTypes.Length > 0 && !types.Contains(dataTypes[0]))
            {
                var valid = FormatAllowedDataTypes(types);
                throw new Exception($"Error: invalid-type, function {Name} expects an array of {valid}.");
            }

            return array;
        }

        protected void EnsureArrayOrString(JmesPathFunctionArgument argument)
        {
            var type = argument.Token?.GetTokenType();

            if (type != "array" && type != "string")
                throw new Exception($"Error: invalid-type, function {Name} expects either an array or a string.");
        }

        protected void EnsureExpressionType(JmesPathFunctionArgument argument)
        {
            if (!argument.IsExpressionType)
                throw new Exception($"Error: invalid-type, function {Name} expects an expression-type.");
        }

        protected void EnsureNumbers(params JmesPathFunctionArgument[] args)
        {
            foreach (var argument in args)
            {
                var token = argument.Token;
                if (token.GetTokenType() != "number")
                    throw new Exception($"Error: invalid-type, function {Name} only accepts numbers.");
            }
        }

        private JToken EnsureOf(JmesPathFunctionArgument argument, string type)
        {
            var token = argument.Token;
            var tokenType = token.GetTokenType();
            if (tokenType != type)
                throw new Exception($"Error: invalid-type, function {Name} expects an {type}.");

            return token;
        }

        private static string FormatAllowedDataTypes(string[] types)
        {
            if (types.Length == 1)
                return types[0];

            var valid = new StringBuilder();
            valid.AppendFormat("{0} ", types[0]);
            for (var index = 1; index < types.Length - 1; index++)
                valid.AppendFormat(", {0} ", types[index]);
            valid.AppendFormat("or {0}", types[types.Length - 1]);

            return valid.ToString();
        }

        public void SetContext(JToken token)
        {
            context_ = token;
        }
    }
}