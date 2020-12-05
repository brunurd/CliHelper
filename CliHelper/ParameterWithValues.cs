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

        /// <summary>
        /// Get a value from a input parameter.
        /// </summary>
        /// <param name="parameter">A parameter, could be null.</param>
        /// <param name="index">The index of the value. Default is 0.</param>
        /// <returns></returns>
        public static object GetValue(ParameterWithValues parameter, int index = 0)
        {
            if (parameter == null) return null;
            return parameter.GetValue(index);
        }

        /// <summary>
        /// Get a parameter value, if don't exists return null.
        /// </summary>
        /// <param name="index">Optional index of the value. Default is 0.</param>
        public object GetValue(int index = 0)
        {
            if (Values == null) return null;
            return Values.Lenght >= index ? null : Values[index];
        }
    }
}