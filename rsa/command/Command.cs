using System;
using System.Collections.Generic;
using System.Text;
using RSA.errors;

namespace RSA.command
{
    /// <summary>
    ///     Interface that represents console command
    /// </summary>
    public interface Command
    {
        /// <summary>
        ///     Method that should be called before command execution
        /// </summary>
        /// <param name="args">Array that stores command line arguments</param>
        /// <returns>Initialized console command. Instance if <see cref="Command"/> interface</returns>
        Command Initialize(String[] args);

        /// <summary>
        ///     Method that execute console command. If command object
        ///     is not initialized, method should throw <see cref="CommandNotInitializedException"/>
        /// </summary>
        void Execute();

        /// <summary>
        ///     Method that check if command is initialized
        /// </summary>
        /// <returns></returns>
        bool isInitialized();
    }
}
