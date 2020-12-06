namespace CliHelper.Models.Parameters
{
    /// <summary>
    /// A parameter representation.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Gets the parameter key, with it name and char shortcut.
        /// </summary>
        public ArgumentKey Key { get; }

        /// <summary>
        /// Gets the parameter description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the parameters length (quantity of values).
        /// </summary>
        public int ParametersLength { get; }

        /// <summary>
        /// Gets the type of the parameter value.
        /// </summary>
        public ParameterType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="description">The parameteer description.</param>
        /// <param name="shortcut">The parameter char shortcut.</param>
        /// <param name="parametersLength">The parameter values length.</param>
        /// <param name="type">The parameter value type.</param>
        internal Parameter(string name, string description, char shortcut, int parametersLength, ParameterType type)
        {
            Key = new ArgumentKey(name, shortcut);
            Description = description;
            ParametersLength = parametersLength;
            Type = type;
        }
    }
}