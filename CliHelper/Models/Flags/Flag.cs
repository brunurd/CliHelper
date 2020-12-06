namespace CliHelper.Models.Flags
{
    /// <summary>
    /// The flag info.
    /// </summary>
    public class Flag
    {
        /// <summary>
        /// Gets the flag key, that contains it name and char shortcut.
        /// </summary>
        public ArgumentKey Key { get; }

        /// <summary>
        /// Gets the flag description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Flag"/> class.
        /// </summary>
        /// <param name="name">The flag name.</param>
        /// <param name="description">The flag descriptiion.</param>
        /// <param name="shortcut">The flag character shortcut.</param>
        internal Flag(string name, string description, char shortcut)
        {
            Key = new ArgumentKey(name, shortcut);
            Description = description;
        }
    }
}