using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Interop
{
    public abstract class JFunction
    {
        protected JFunction(string name)
        {
            Name = name;
        }
        public string Name
        {
            get; private set;
        }

        public abstract bool Validate(params JToken[] args);
        public abstract JToken Execute(params JToken[] args);
    }
}