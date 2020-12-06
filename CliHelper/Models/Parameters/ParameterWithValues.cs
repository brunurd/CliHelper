namespace CliHelper.Models.Parameters
{
    /// <summary>
    /// A parameter representation with the current values stored.
    /// </summary>
    public class ParameterWithValues
    {
        /// <summary>
        /// Gets the parameter info.
        /// </summary>
        public Parameter Parameter { get; }

        /// <summary>
        /// Gets the input values of the parameter.
        /// </summary>
        public object[] Values { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterWithValues"/> class.
        /// </summary>
        /// <param name="parameter">The base parameter.</param>
        /// <param name="values">The current values.</param>
        internal ParameterWithValues(Parameter parameter, object[] values)
        {
            Parameter = parameter;
            Values = values;
        }

        /// <summary>
        /// Get a value from a input parameter.
        /// </summary>
        /// <param name="parameter">A parameter, could be null.</param>
        /// <param name="index">The index of the value. Default is 0.</param>
        /// <returns>The value or null if the index don't exists.</returns>
        public static object GetValue(ParameterWithValues parameter, int index = 0)
        {
            if (parameter == null) {
                return null;
            }

            return parameter.GetValue(index);
        }

        /// <summary>
        /// Get a parameter value, if don't exists return null.
        /// </summary>
        /// <param name="index">Optional index of the value. Default is 0.</param>
        /// <returns>The value or null if the index don't exists.</returns>
        public object GetValue(int index = 0)
        {
            if (Values == null) {
                return null;
            }

            return Values.Length >= index ? null : Values[index];
        }
    }
}