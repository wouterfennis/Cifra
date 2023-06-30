using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.File;
using System;

namespace Cifra.FileSystem.Mapping
{
    internal static class SpreadsheetMapping
    {
        /// <summary>
        /// Maps to library metadata model.
        /// </summary>
        public static Metadata MapToLibraryModel(this Domain.Spreadsheet.Metadata input)
        {
            ValidateNullInput(input);

            return new Metadata
            {
                Title = input.Title,
                Subject = input.Subject,
                Author = input.Author,
                Created = input.Created,
                FileName = input.FileName,
                ApplicationVersion = input.ApplicationVersion
            };
        }

        private static void ValidateNullInput(object input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
        }
    }
}
