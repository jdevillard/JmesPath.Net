using System;
using System.Collections.Generic;
using DevLab.JmesPath.Interop;

#if NETSTANDARD1_3
using System.Reflection;
#endif

namespace DevLab.JmesPath.Functions
{
    public class JmesPathFunctionFactory : IRegisterFunctions, IFunctionRepository
    {
        private readonly Dictionary<string, JmesPathFunction> functions_
            = new Dictionary<string, JmesPathFunction>()
            ;

        private JmesPathFunctionFactory()
        {
            this
                .Register<AbsFunction>()
                .Register<AvgFunction>()
                .Register<CeilFunction>()
                .Register<ContainsFunction>()
                .Register<EndsWithFunction>()
                .Register<FindFirstFunction>()
                .Register<FindLastFunction>()
                .Register<FloorFunction>()
                .Register<FromItemsFunction>()
                .Register<GroupByFunction>()
                .Register<ItemsFunction>()
                .Register<JoinFunction>()
                .Register<KeysFunction>()
                .Register<LengthFunction>()
                .Register<LowerFunction>()
                .Register<MapFunction>()
                .Register<MaxByFunction>()
                .Register<MaxFunction>()
                .Register<MergeFunction>()
                .Register<MinByFunction>()
                .Register<MinFunction>()
                .Register<NotNullFunction>()
                .Register<PadLeftFunction>()
                .Register<PadRightFunction>()
                .Register<ReplaceFunction>()
                .Register<ReverseFunction>()
                .Register<SortByFunction>()
                .Register<SortFunction>()
                .Register<SplitFunction>()
                .Register<StartsWithFunction>()
                .Register<SumFunction>()
                .Register<ToArrayFunction>()
                .Register<ToNumberFunction>()
                .Register<ToStringFunction>()
                .Register<TrimFunction>()
                .Register<TrimLeftFunction>()
                .Register<TrimRightFunction>()
                .Register<TypeFunction>()
                .Register<UpperFunction>()
                .Register<ValuesFunction>()
                .Register<ZipFunction>()
                ;
        }

        public static JmesPathFunctionFactory Create()
            => new JmesPathFunctionFactory();

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

            Register(instance.Name, instance);

            return this;
        }

        public IEnumerable<string> Names => functions_.Keys;

        public JmesPathFunction this[string name] => functions_[name];

        public bool Contains(string name)
        {
            return functions_.ContainsKey(name);
        }
    }
}