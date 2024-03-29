﻿using Cifra.Commands.Models;

namespace Cifra.Commands
{
    /// <summary>
    /// The command to update an Class
    /// </summary>
    public sealed record UpdateClassCommand
    {
        /// <summary>
        /// The updated class
        /// </summary>
        public required Class Class { get; init; }
    }
}
