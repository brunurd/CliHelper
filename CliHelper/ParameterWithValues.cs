namespace CliHelper
{
    public class ParameterWithValues
    {
        /// <summary>
        /// The parameter info.
        /// </summary>
        public Parameter Parameter { get; }
        
        /// <summary>
        /// The input values to the parameter.
        /// </summary>
        public object[] Values { get; }

        internal ParameterWithValues(Parameter parameter, object[] values)
        {
            Parameter = parameter;
            Values = values;
        }
    }
}