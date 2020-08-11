using System;

namespace LavaLeak.CliHelper
{
    public class ApplicationRootFlag
    {
        private readonly Action _callback;

        public ArgumentKey Key { get; }
        public string Description { get; }

        internal ApplicationRootFlag(string name, string description, Action callback, char shortcut)
        {
            Key = new ArgumentKey(name, shortcut);
            Description = description;
            _callback = callback;
        }

        internal void Run()
        {
            _callback?.Invoke();
        }
    }
}