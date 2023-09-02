using Cifra.Domain.ValueTypes;
using SpreadsheetWriter.Abstractions;
using System;
using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the title.
    /// </summary>
    internal class TitleBlock
    {
        private const int TitleSize = 16;
        private const string DateFormat = "dd-MM-yyyy";
        public Point StartPoint { get; }
        public Name SpreadsheetName { get; }
        public DateTime CreatedOn { get; }
        public string ApplicationVersion { get; }

        public TitleBlock(Point startPoint, Name spreadsheetName, DateTime createdOn, string applicationVersion)
        {
            StartPoint = startPoint;
            SpreadsheetName = spreadsheetName;
            CreatedOn = createdOn;
            ApplicationVersion = applicationVersion;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPoint;
            spreadsheetWriter
                .SetFontSize(TitleSize)
                .SetBackgroundColor(Color.White)
                .Write(SpreadsheetName.Value)
                .ResetStyling()
                .NewLine()
                .SetFontBold(true)
                .Write("Gemaakt op:")
                .SetFontBold(false)
                .MoveRight()
                .Write(CreatedOn.ToString(DateFormat))
                .NewLine()
                .Write("Cifra:")
                .MoveRight()
                .Write(ApplicationVersion);
        }
    }
}
