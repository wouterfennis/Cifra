using System;
using System.Drawing;
using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.Abstractions;

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
        public Name TestName { get; }
        public DateTime CreatedOn { get; }

        public TitleBlock(Point startPoint, Name testName, DateTime createdOn)
        {
            StartPoint = startPoint;
            TestName = testName;
            CreatedOn = createdOn;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPoint;
            spreadsheetWriter
                .SetFontSize(TitleSize)
                .Write(TestName.Value)
                .ResetStyling()
                .NewLine()
                .SetFontBold(true)
                .Write("Gemaakt op:")
                .SetFontBold(false)
                .MoveRight()
                .Write(CreatedOn.ToString(DateFormat));
        }
    }
}
