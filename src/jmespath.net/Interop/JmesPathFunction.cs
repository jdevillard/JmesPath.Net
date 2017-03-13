using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Interop
{
    public abstract class JmesPathFunction
    {
        protected JmesPathFunction(string name, int count)
        {
            Name = name;
            ArgumentCount = count;
        }

        public string Name { get; private set; }

        public int ArgumentCount { get; private set; }

        public abstract bool Validate(params JToken[] args);
        public abstract JToken Execute(params JToken[] args);
    }
}