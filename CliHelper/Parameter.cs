namespace LavaLeak.CliHelper
{
    public class Parameter
    {
        public ArgumentKey Key { get; }
        public string Description { get; }
        public int ParametersLength { get; }
        public ParameterType Type { get; }

        internal Parameter(string name, string description, char shortcut, int parametersLength, ParameterType type)
        {
            Key = new ArgumentKey(name, shortcut);
            Description = description;
            ParametersLength = parametersLength;
            Type = type;
        }
    }
}