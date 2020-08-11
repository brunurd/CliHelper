namespace LavaLeak.CliHelper
{
    /// <summary>
    /// The flag info.
    /// </summary>
    public class Flag
    {
        public ArgumentKey Key { get; }
        public string Description { get;  }

        internal Flag(string name, string description, char shortcut)
        {
            Key = new ArgumentKey(name, shortcut);
            Description = description;
        }
    }
}