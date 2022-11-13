using System;

namespace Cifra.Api.V1.Models.Spreadsheet.Requests
{
    public sealed class Metadata
    {
        /// <summary>
        /// The author of the spreadsheet
        /// </summary>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// The title of the spreadsheet
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The subject of the spreadsheet
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// The created date of the spreadsheet
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The filename of the spreadsheet
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// The version of the application.
        /// </summary>
        public string ApplicationVersion { get; set; } = string.Empty;
    }
}
