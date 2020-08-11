namespace LavaLeak.CliHelper
{
    public static class ExtensionMethods
    {
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