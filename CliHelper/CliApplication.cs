namespace CliHelper
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using CliHelper.Commands;
    using CliHelper.Models;
    using CliHelper.Models.Flags;
    using CliHelper.Models.Parameters;

    /// <summary>
    /// The command line application.
    /// </summary>
    public class CliApplication
    {
        private const string REGEX_NAME = @"( |^)--(\w+)";
        private const string REGEX_SHORTCUT = @"( |^)-(\w+)";

        private readonly Dictionary<string, Command> _commands;
        private readonly Dictionary<ArgumentKey, ApplicationRootFlag> _flags;

        /// <summary>
        /// Gets the current application instance.
        /// </summary>
        public static CliApplication Current { get; private set; }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the application options <seealso cref="Options"/>.
        /// </summary>
        public CliOptions Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CliApplication"/> class.
        /// </summary>
        /// <param name="name">The name of the application.</param>
        /// <param name="options">Optional settings for the application.</param>
        public CliApplication(string name, CliOptions options = null)
        {
            Name = name;
            Options = options ?? new CliOptions(name);
            _commands = new Dictionary<string, Command>();
            _flags = new Dictionary<ArgumentKey, ApplicationRootFlag>();

            if (Options.AddHelp)
            {
                new HelpCommand(this);
            }

            Current = this;
        }

        /// <summary>
        /// Register of a command of the application.
        /// </summary>
        /// <param name="name">The name to be used to call the command.</param>
        /// <param name="description">Add a description to the command.</param>
        /// <param name="callback">This is the execution of the command with all parameters and flags.</param>
        /// <returns>The command instance.</returns>
        public Command RegisterCommand(string name, string description, Action<ParameterWithValues[], Flag[]> callback)
        {
            var command = new Command(name, description, callback);
            _commands.Add(name, command);
            return command;
        }

        /// <summary>
        /// Register of a root flag (a flag without command).
        /// </summary>
        /// <param name="name">The name of the root flag.</param>
        /// <param name="description">The root flag description.</param>
        /// <param name="callback">The root flag callback.</param>
        /// <param name="shortcut">The root flag character shortcut.</param>
        public void RegisterRootFlag(string name, string description, Action callback, char shortcut = default)
        {
            var key = new ArgumentKey(name, shortcut);

            foreach (var parameter in _flags)
            {
                if (parameter.Key.Equals(key))
                {
                    throw new Exception(
                        $"Error: An parameter with the name {name} or shortcut {shortcut} already added.");
                }
            }

            _flags.Add(key, new ApplicationRootFlag(name, description, callback, shortcut));
        }

        /// <summary>
        /// Show all commands usage instructions.
        /// </summary>
        /// <param name="commandName">A command to show it current help.</param>
        /// <returns>Return the output text.</returns>
        public string Help(string commandName = null)
        {
            StringBuilder helpOutput = new StringBuilder();

            helpOutput.AppendLine($"{Name}\n");
            var keySize = 0;

            if (_commands.ContainsKey(commandName))
            {
                helpOutput.AppendLine($"Usage: {Options.AssemblyName} {commandName} [options]\n");
                helpOutput.AppendLine("Parameters:");

                var cmd = _commands[commandName];
                helpOutput.AppendLine(cmd.Help());

                return helpOutput.ToString();
            }

            helpOutput.AppendLine($"Usage: {Options.AssemblyName} <command> [options]\n");
            helpOutput.AppendLine("Commands:");

            // TODO: Get the max key length.
            foreach (var key in _commands.Keys)
            {
                if (key.Length > keySize)
                {
                    keySize = key.Length;
                }
            }

            keySize++;

            // TODO: Add root flags.

            // TODO: Use a method for this.
            foreach (var command in _commands)
            {
                var key = new char[keySize];

                for (int i = 0; i < command.Key.Length; i++)
                {
                    key[i] = command.Key[i];
                }

                var keyStr = new string(key).Replace('\0', ' ');

                helpOutput.AppendLine($"{keyStr}| {command.Value.Description}");
            }

            return helpOutput.ToString();
        }

        /// <summary>
        /// Execute the application passing the console args.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <exception cref="Exception">Throws a exception if don't have any argument.</exception>
        public void Run(string[] args)
        {
            if (args == null)
            {
                Help();
                ErrorHandler.Log("Error: No arguments provided.");
                return;
            }

            if (args.Length <= 0)
            {
                Help();
                ErrorHandler.Log("Error: No arguments provided.");
                return;
            }

            var commandName = args[0];
            var nameMatch = new Regex(REGEX_NAME).Match(commandName);
            var shortcutMatch = new Regex(REGEX_SHORTCUT).Match(commandName);
            string name = nameMatch.Success ? nameMatch.Groups[2].Value : null;
            char[] shortcuts = shortcutMatch.Success ? shortcutMatch.Groups[2].Value.ToCharArray() : new char[0];
            var nameKey = new ArgumentKey(name);

            if (_commands.ContainsKey(commandName))
            {
                var command = _commands[commandName];
                var argsSlice = new string[args.Length - 1];
                Array.Copy(args, 1, argsSlice, 0, argsSlice.Length);
                command.Run(argsSlice);
                return;
            }

            foreach (var flag in _flags)
            {
                if (flag.Key.Equals(nameKey))
                {
                    flag.Value.Run();
                    return;
                }

                foreach (var shortcut in shortcuts)
                {
                    var shortcutKey = new ArgumentKey(null, shortcut);
                    if (flag.Key.Equals(shortcutKey))
                    {
                        flag.Value.Run();
                        return;
                    }
                }
            }

            ErrorHandler.Log($"Error: The command \"{commandName}\" is not recognized.");
        }
    }
}