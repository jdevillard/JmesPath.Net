using System;
using System.Collections.Generic;
using DevLab.JmesPath.Interop;

namespace DevLab.JmesPath.Functions
{
    public  class JmesPathFunctionFactory : IRegisterFunctions, IFunctionRepository
    {
        private readonly Dictionary<string, JmesPathFunction> functions_ = new Dictionary<string, JmesPathFunction>();
        private static IFunctionRepository repository_;

        public static IFunctionRepository Default {
            get
            {
                if (repository_ != null)
                    return repository_;
                var repo = new JmesPathFunctionFactory();
                repo
                    .Register<AbsFunction>()
                    .Register<AvgFunction>()
                    .Register<CeilFunction>()
                    .Register<ContainsFunction>()
                    .Register<EndsWithFunction>()
                    .Register<FloorFunction>()
                    .Register<JoinFunction>()
                    .Register<LengthFunction>()
                    .Register<MaxFunction>()
                    .Register<MaxByFunction>()
                    .Register<MinFunction>()
                    .Register<MinByFunction>()
                    .Register<ReverseFunction>()
                    .Register<StartsWithFunction>()
                    .Register<SortFunction>()
                    .Register<SortByFunction>()
                    .Register<SumFunction>()
                    .Register<ToArrayFunction>()
                    .Register<ToNumberFunction>()
                    .Register<ToStringFunction>()
                    .Register<TypeFunction>()
                    ;

                repository_ = repo;
                return repo;
            }
        } 

        public IRegisterFunctions Register(string name, JmesPathFunction function)
        {
            if (!functions_.ContainsKey(name))
                functions_.Add(name, function);
            else
                functions_[name] = function;

            return this;
        }

        public IRegisterFunctions Register<T>() where T : JmesPathFunction
        {
            var instance = Activator.CreateInstance<T>();
            Register(instance.Name,instance);

            return this;
        }

        public IEnumerable<string> Names => functions_.Keys;
        
        public JmesPathFunction this[string name] => functions_[name];

        public  bool Contains(string name)
        {
            return functions_.ContainsKey(name);
        }
    }
}