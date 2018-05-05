using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.command
{
    /// <summary>
    ///     Class that store console commands
    /// </summary>
    public class CommandContainer
    {
        private static Dictionary<String, Command> commands = new Dictionary<String, Command>();

        public void AddCommand(string name, Command command)
        {
            commands.TryAdd(name, command);
        }

        public bool ContainsCommand(string name)
        {
            return commands.ContainsKey(name);
        }

        public Command GetCommand(string name)
        {
            return commands.TryGetValue(name, out Command command) ? command : null;
        }
    }
}
