using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LavaLeak.CliHelper
{
    public class CliApplication
    {
        private const string REGEX_NAME = @"( |^)--(\w+)";
        private const string REGEX_SHORTCUT = @"( |^)-(\w+)";

        private Dictionary<string, Command> _commands;
        private Dictionary<ArgumentKey, ApplicationRootFlag> _flags;

        public static CliApplication Current { get; private set; }
        public string Name { get; }
        public CliOptions Options { get; }

        public CliApplication(string name, CliOptions options = null)
        {
            Name = name;
            Options = options ?? new CliOptions();
            _commands = new Dictionary<string, Command>();
            _flags = new Dictionary<ArgumentKey, ApplicationRootFlag>();

            if (Options.addHelp)
            {
                RegisterRootFlag("help", "Show application commands list.", () => Help(), 'h');
            }

            Current = this;
        }

        /// <summary>
        /// Register of a command of the application.
        /// </summary>
        /// <param name="name">The name to be used to call the command.</param>
        /// <param name="description">Add a description to the command.</param>
        /// <param name="callback">This is the execution of the command with all parameters and flags.</param>
        /// <returns></returns>
        public Command RegisterCommand(string name, string description, Action<ParameterWithValues[], Flag[]> callback)
        {
            var command = new Command(name, description, callback);
            _commands.Add(name, command);
            return command;
        }

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
        public void Help(string commandName = null)
        {
            if (commandName == null)
            {
                foreach (var command in _commands)
                {
                    // TODO: Briefly commands help.
                    Console.WriteLine(command);
                }
            }
            else
            {
                // TODO: Command specific help.
            }
        }

        /// <summary>
        /// Execute the application passing the console args.
        /// </summary>
        /// <param name="args"></param>
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