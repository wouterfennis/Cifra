using System;
using SpreadsheetWriter.Abstractions;

namespace Cifra.FileSystem.Mapping
{
    internal static class SpreadsheetMapping
    {
        /// <summary>
        /// Maps to library metadata model.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Metadata MapToLibraryModel(this Application.Models.Spreadsheet.Metadata input)
        {
            ValidateNullInput(input);

            return new Metadata
            {
                Title = input.Title,
                Subject = input.Subject,
                Author = input.Author,
                Created = input.Created,
                FileName = input.FileName
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
