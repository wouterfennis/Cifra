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

        /// <summary>
        /// Maps to save result model.
        /// </summary>
        public static Application.Models.Spreadsheet.SaveResult MapToModel(this  SaveResult input)
        {
            ValidateNullInput(input);

            return new Application.Models.Spreadsheet.SaveResult
            {
                IsSuccess = input.IsSuccess,
                Exception = input.Exception,
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
