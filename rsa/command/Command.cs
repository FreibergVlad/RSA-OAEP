using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.command
{
    /// <summary>
    ///     Interface that represents console command
    /// </summary>
    public interface Command
    {
        Command Initialize(String[] args);

        void Execute();
    }
}
