namespace CliHelper
{
    using CliHelper.Models.Flags;
    using CliHelper.Models.Parameters;

    /// <summary>
    /// Helper static class with extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get a parameter by it name in a parameters array.
        /// </summary>
        /// <param name="parameters">A parameters array.</param>
        /// <param name="name">The name of a parameter.</param>
        /// <returns>The parameter if it exists, else returns null.</returns>
        public static ParameterWithValues GetParameterByName(this ParameterWithValues[] parameters, string name)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.Parameter.Key.Name == name)
                {
                    return parameter;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if a flags array contains a flag by it name.
        /// </summary>
        /// <param name="flags">The flags array.</param>
        /// <param name="name">The name to find.</param>
        /// <returns>Returns if it has or not.</returns>
        public static bool HasFlag(this Flag[] flags, string name)
        {
            foreach (var flag in flags)
            {
                if (flag.Key.Name == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}