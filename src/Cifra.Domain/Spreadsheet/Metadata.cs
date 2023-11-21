using Cifra.Domain.Validation;
using Cifra.Domain.ValueTypes;
using System;

namespace Cifra.Domain.Spreadsheet
{
    /// <summary>
    /// Metadata of an spreadsheet
    /// </summary>
    public sealed class Metadata
    {
        /// <summary>
        /// The author of the spreadsheet
        /// </summary>
        public Name Author { get; }

        /// <summary>
        /// The title of the spreadsheet
        /// </summary>
        public Name Title { get; }

        /// <summary>
        /// The subject of the spreadsheet
        /// </summary>
        public Name Subject { get; }

        /// <summary>
        /// The created date of the spreadsheet
        /// </summary>
        public DateTime Created { get; }

        /// <summary>
        /// The filename of the spreadsheet
        /// </summary>
        public Name FileName { get; }

        /// <summary>
        /// The version of the application.
        /// </summary>
        public Name ApplicationVersion { get; }

        private Metadata(Name author, Name title, Name subject, DateTime created, Name fileName, Name applicationVersion)
        {
            Author = author;
            Title = title;
            Subject = subject;
            Created = created;
            FileName = fileName;
            ApplicationVersion = applicationVersion;
        }

        public static Result<Metadata> TryCreate(string author, string title, string subject, DateTime created, string fileName, string applicationVersion)
        {
            Result<Name> authorResult = Name.CreateFromString(author);
            Result<Name> titleResult = Name.CreateFromString(title);
            Result<Name> subjectResult = Name.CreateFromString(subject);
            Result<Name> fileNameResult = Name.CreateFromString(fileName);
            Result<Name> applicationVersionResult = Name.CreateFromString(applicationVersion);

            if (!authorResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(author), "Author is not valid");
                return Result<Metadata>.Fail<Metadata>(validationMessage);
            }

            if (!titleResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(title), "Title is not valid");
                return Result<Metadata>.Fail<Metadata>(validationMessage);
            }

            if (!subjectResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(subject), "Subject is not valid");
                return Result<Metadata>.Fail<Metadata>(validationMessage);
            }

            if (!fileNameResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(fileName), "File name is not valid");
                return Result<Metadata>.Fail<Metadata>(validationMessage);
            }

            if (!applicationVersionResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(applicationVersion), "Application version is not valid");
                return Result<Metadata>.Fail<Metadata>(validationMessage);
            }

            return Result<Metadata>.Ok<Metadata>(new Metadata(authorResult.Value!, titleResult.Value!, subjectResult.Value!, created, fileNameResult.Value!, applicationVersionResult.Value!));
        }
    }
}