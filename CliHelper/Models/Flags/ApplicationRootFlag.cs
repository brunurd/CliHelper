namespace CliHelper.Models.Flags
{
    using System;

    /// <summary>
    /// A direct flag without a command.
    /// </summary>
    public class ApplicationRootFlag
    {
        private readonly Action _callback;

        /// <summary>
        /// Gets the flag key name and shortcut.
        /// </summary>
        public ArgumentKey Key { get; }

        /// <summary>
        /// Gets the flag description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRootFlag"/> class.
        /// </summary>
        /// <param name="name">The flag name.</param>
        /// <param name="description">The flag description.</param>
        /// <param name="callback">The flag callback.</param>
        /// <param name="shortcut">The flag name shortcut.</param>
        internal ApplicationRootFlag(string name, string description, Action callback, char shortcut)
        {
            Key = new ArgumentKey(name, shortcut);
            Description = description;
            _callback = callback;
        }

        /// <summary>
        /// Execute the callback.
        /// If the callback is null it will pass through.
        /// </summary>
        internal void Run()
        {
            _callback?.Invoke();
        }
    }
}