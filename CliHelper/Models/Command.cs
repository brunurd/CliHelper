namespace CliHelper.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using CliHelper.Models.Flags;
    using CliHelper.Models.Parameters;

    /// <summary>
    /// A cli application command representation.
    /// </summary>
    public class Command
    {
        private const string REGEX_NAME = @"( |^)--(\w+)";
        private const string REGEX_SHORTCUT = @"( |^)-(\w+)";

        private readonly Action<ParameterWithValues[], Flag[]> _callback;
        private Dictionary<ArgumentKey, Parameter> _parameters;
        private Dictionary<ArgumentKey, Flag> _flags;

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the command description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="name">The command name.</param>
        /// <param name="description">The command description.</param>
        /// <param name="callback">The command callback.</param>
        internal Command(string name, string description, Action<ParameterWithValues[], Flag[]> callback)
        {
            Name = name;
            Description = description;
            _callback = callback;
            _parameters = new Dictionary<ArgumentKey, Parameter>();
            _flags = new Dictionary<ArgumentKey, Flag>();
        }

        /// <summary>
        /// Register of a command parameter, something like: command --param value1 value2 
        /// </summary>
        /// <param name="name">A unique parameter name.
        /// warning: don't put hyphen at the begin.</param>
        /// <param name="description">The command description.</param>
        /// <param name="shortcut">A unique shortcut char.
        /// warning: don't put hyphen at the begin.</param>
        /// <param name="parametersLength">The length of values to put after the parameter.</param>
        /// <param name="type">The values type.</param>
        /// <exception cref="Exception">Throws a exception the name or shortcut is already registered.</exception>
        public void RegisterParameter(string name, string description, char shortcut = default, int parametersLength = 1, ParameterType type = ParameterType.String)
        {
            var key = new ArgumentKey(name, shortcut);

            foreach (var parameter in _parameters)
            {
                if (parameter.Key.Equals(key))
                {
                    throw new Exception(
                        $"Error: An parameter with the name {name} or shortcut {shortcut} already added.");
                }
            }

            _parameters.Add(key, new Parameter(name, description, shortcut, parametersLength, type));
        }

        /// <summary>
        /// Register of a command flag to be on or off, something like: command --flag 
        /// </summary>
        /// <param name="name">A unique flag name.
        /// warning: don't put hyphen at the begin.</param>
        /// <param name="description">The flag description.</param>
        /// <param name="shortcut">A unique shortcut char.
        /// warning: don't put hyphen at the begin.</param>
        /// <exception cref="Exception">Throws a exception the name or shortcut is already registered.</exception>
        public void RegisterFlag(string name, string description, char shortcut = default)
        {
            var key = new ArgumentKey(name, shortcut);

            foreach (var parameter in _parameters)
            {
                if (parameter.Key.Equals(key))
                {
                    throw new Exception(
                        $"Error: An flag with the name {name} or shortcut {shortcut} already added.");
                }
            }

            _flags.Add(key, new Flag(name, description, shortcut));
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var parameter in _parameters)
            {
                builder.AppendLine(parameter.ToString());
            }

            foreach (var flag in _flags)
            {
                builder.AppendLine(flag.ToString());
            }

            return builder.ToString();
        }

        /// <summary>
        /// Execute it command if it is called.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        internal void Run(string[] args)
        {
            var parameters = HandleParameters(args);
            var flags = HandleFlags(args);

            try
            {
                _callback?.Invoke(parameters, flags);
            }
            catch (Exception error)
            {
                ErrorHandler.Log(error);
            }
        }

        private ParameterWithValues[] HandleParameters(string[] args)
        {
            var parameters = new List<ParameterWithValues>();

            for (var index = 0; index < args.Length; index++)
            {
                var arg = args[index];
                var nameMatch = new Regex(REGEX_NAME).Match(arg);
                var shortcutMatch = new Regex(REGEX_SHORTCUT).Match(arg);
                string name = nameMatch.Success ? nameMatch.Groups[2].Value : null;
                char[] shortcuts = shortcutMatch.Success ? shortcutMatch.Groups[2].Value.ToCharArray() : new char[0];
                var nameKey = new ArgumentKey(name);

                foreach (var parameter in _parameters)
                {
                    if (parameter.Key.Equals(nameKey))
                    {
                        var length = index + parameter.Value.ParametersLength + 1;
                        var parametersArguments = new List<object>();

                        for (int i = index + 1; i < length; i++)
                        {
                            var value = parameter.Value.Type == ParameterType.String
                                ? args[i]
                                : Convert.ChangeType(args[i], typeof(double));
                            parametersArguments.Add(value);
                        }

                        parameters.Add(new ParameterWithValues(parameter.Value, parametersArguments.ToArray()));
                        continue;
                    }

                    foreach (var shortcut in shortcuts)
                    {
                        var shortcutKey = new ArgumentKey(null, shortcut);
                        if (parameter.Key.Equals(shortcutKey))
                        {
                            var length = index + parameter.Value.ParametersLength + 1;
                            var parametersArguments = new List<object>();

                            for (int i = index + 1; i < length; i++)
                            {
                                var value = parameter.Value.Type == ParameterType.String
                                    ? args[i]
                                    : Convert.ChangeType(args[i], typeof(double));
                                parametersArguments.Add(value);
                            }

                            parameters.Add(new ParameterWithValues(parameter.Value, parametersArguments.ToArray()));
                        }
                    }
                }
            }

            return parameters.ToArray();
        }

        private Flag[] HandleFlags(string[] args)
        {
            var flags = new List<Flag>();

            foreach (var arg in args)
            {
                var nameMatch = new Regex(REGEX_NAME).Match(arg);
                var shortcutMatch = new Regex(REGEX_SHORTCUT).Match(arg);
                string name = nameMatch.Success ? nameMatch.Groups[2].Value : null;
                char[] shortcuts = shortcutMatch.Success ? shortcutMatch.Groups[2].Value.ToCharArray() : new char[0];
                var nameKey = new ArgumentKey(name);

                foreach (var flag in _flags)
                {
                    if (flag.Key.Equals(nameKey))
                    {
                        flags.Add(flag.Value);
                        continue;
                    }

                    foreach (var shortcut in shortcuts)
                    {
                        var shortcutKey = new ArgumentKey(null, shortcut);
                        if (flag.Key.Equals(shortcutKey))
                        {
                            flags.Add(flag.Value);
                        }
                    }
                }
            }

            return flags.ToArray();
        }
    }
}