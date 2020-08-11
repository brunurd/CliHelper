namespace LavaLeak.CliHelper
{
    public class ArgumentKey
    {
        public string Name { get; }
        public char Shortcut { get; }

        internal ArgumentKey(string name, char shortcut = default)
        {
            Name = name;
            Shortcut = shortcut;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is ArgumentKey)) return false;

            var key = (ArgumentKey) obj;

            if (string.IsNullOrEmpty(Name)) return key.Shortcut == Shortcut;
            if (Shortcut == default) return key.Name == Name;

            return key.Name == Name || key.Shortcut == Shortcut;
        }
    }
}