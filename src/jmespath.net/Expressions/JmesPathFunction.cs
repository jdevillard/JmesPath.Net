using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Interop;


namespace DevLab.JmesPath.Expressions
{
    public class JmesPathFunction : JmesPathExpression
    {
        private readonly IFunctionRepository repository_;
        private readonly string name_;
        private readonly JmesPathExpression[] expressions_;

        public JmesPathFunction(string name, params JmesPathExpression[] expressions)
        :this(JmesPathFunctionFactory.Default, name, expressions)
        {
            
        }
        public JmesPathFunction(IFunctionRepository repository, string name, IList<JmesPathExpression> expressions)
            :this(repository,name, expressions.ToArray())
        {
            
        }
        public JmesPathFunction(IFunctionRepository repository,string name, params JmesPathExpression[] expressions)
        {
            if (!repository.Contains(name))
                throw new Exception("unknown-function");

            repository_ = repository;
            name_ = name;
            expressions_ = expressions;
        }

        public JToken Name => name_;

        protected override JmesPathArgument Transform(JToken json)
        {
            var arguments = expressions_.Select(expression => expression.Transform(json).AsJToken()).ToArray();
            var f = repository_[name_];
            return f.Execute(arguments);
        }
    }
}