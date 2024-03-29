﻿namespace Cifra.Commands.Models
{
    /// <summary>
    /// The metadata of the spreadsheet.
    /// </summary>
    public sealed record Metadata
    {
        /// <summary>
        /// The author of the spreadsheet
        /// </summary>
        public required string Author { get; init; }

        /// <summary>
        /// The title of the spreadsheet
        /// </summary>
        public required string Title { get; init; }

        /// <summary>
        /// The subject of the spreadsheet
        /// </summary>
        public required string Subject { get; init; }

        /// <summary>
        /// The created date of the spreadsheet
        /// </summary>
        public required DateTime Created { get; init; }

        /// <summary>
        /// The filename of the spreadsheet
        /// </summary>
        public required string FileName { get; init; }

        /// <summary>
        /// The version of the application.
        /// </summary>
        public required string ApplicationVersion { get; init; }
    }
}
