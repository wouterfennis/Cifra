using System;

namespace Cifra.Application.Models.Spreadsheet
{
    /// <summary>
    /// Metadata of an spreadsheet
    /// </summary>
    public sealed class Metadata
    {
        /// <summary>
        /// The author of the spreadsheet
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The title of the spreadsheet
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The subject of the spreadsheet
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The created date of the spreadsheet
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The filename of the spreadsheet
        /// </summary>
        public string FileName { get; set; }
    }
}