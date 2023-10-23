namespace Cifra.Commands.Models
{
    public sealed class Metadata
    {
        /// <summary>
        /// The author of the spreadsheet
        /// </summary>
        public string Author { get; init; }

        /// <summary>
        /// The title of the spreadsheet
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// The subject of the spreadsheet
        /// </summary>
        public string Subject { get; init; }

        /// <summary>
        /// The created date of the spreadsheet
        /// </summary>
        public DateTime Created { get; init; }

        /// <summary>
        /// The filename of the spreadsheet
        /// </summary>
        public string FileName { get; init; }

        /// <summary>
        /// The version of the application.
        /// </summary>
        public string ApplicationVersion { get; init; }
    }
}
