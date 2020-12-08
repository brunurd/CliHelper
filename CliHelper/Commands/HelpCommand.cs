namespace CliHelper.Commands
{
    using System;

    /// <summary>
    /// The help command class.
    /// </summary>
    internal class HelpCommand
    {
        /// <summary>
        /// The help command key.
        /// </summary>
        public const string Key = "help";

        /// <summary>
        /// The help command description.
        /// </summary>
        public const string Description = "Show application commands list.";

        /// <summary>
        /// The shortcut char to help command.
        /// </summary>
        public const char Shortcut = 'h';

        private readonly CliApplication app;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpCommand"/> class.
        /// </summary>
        /// <param name="app">The application to add the command.</param>
        internal HelpCommand(CliApplication app)
        {
            this.app = app;
            app.RegisterRootFlag(Key, Description, Help, Shortcut);
        }

        private void Help()
        {
            var helpOutput = app.Help();
            Console.WriteLine(helpOutput);
        }
    }
}