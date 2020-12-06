namespace CliHelper.Models
{
    /// <summary>
    /// Store the name and shortcut of a flag or parameter.
    /// </summary>
    public class ArgumentKey
    {
        /// <summary>
        /// Gets the full name of a flag or parameter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the shortcut character of a flag or parameter.
        /// </summary>
        public char Shortcut { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentKey"/> class.
        /// </summary>
        /// <param name="name">The name of the flag or parameter.</param>
        /// <param name="shortcut">The character shortcut of the flag or parameter.</param>
        internal ArgumentKey(string name, char shortcut = default)
        {
            Name = name;
            Shortcut = shortcut;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }

            if (!(obj is ArgumentKey)) {
                return false;
            }

            var key = (ArgumentKey)obj;

            if (string.IsNullOrEmpty(Name)) {
                return key.Shortcut == Shortcut;
            }

            if (Shortcut == default) {
                return key.Name == Name;
            }

            return key.Name == Name || key.Shortcut == Shortcut;
        }
    }
}