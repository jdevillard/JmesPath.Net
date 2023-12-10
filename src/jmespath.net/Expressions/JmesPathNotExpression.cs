using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathNotExpression : JmesPathSimpleExpression
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathNotExpression"/>
        /// that negates the result of evaluating its associated expression.
        /// </summary>
        /// <param name="expression"></param>
        public JmesPathNotExpression(JmesPathExpression expression)
            : base(expression)
        {
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            var token = base.Transform(json);
            return JmesPathArgument.IsFalse(token) 
                ? JmesPathArgument.True
                : JmesPathArgument.False
                ;
        }

        protected override async Task<JmesPathArgument> TransformAsync(JToken json)
        {
            var token = await base.TransformAsync(json);
            return JmesPathArgument.IsFalse(token) 
                    ? JmesPathArgument.True
                    : JmesPathArgument.False
                ;
        }
        
        public override string ToString()
            => $"!{Expression}";
    }
}