using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.Abstractions;
using System;
using System.Drawing;
using static Cifra.FileSystem.Spreadsheet.Blocks.TitleBlock;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the title.
    /// </summary>
    internal class TitleBlock
    {
        private const int TitleSize = 16;
        private const string DateFormat = "dd-MM-yyyy";
        private readonly TitleBlockInput input;

        public TitleBlock(TitleBlockInput input)
        {
            this.input = input;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = input.StartPoint;
            spreadsheetWriter
                .SetFontSize(TitleSize)
                .Write(input.TestName.Value)
                .ResetStyling()
                .NewLine()
                .Write("Gemaakt op:")
                .MoveRight()
                .Write(input.CreatedOn.ToString(DateFormat));
        }

        public class TitleBlockInput
        {
            public Point StartPoint { get; }
            public Name TestName { get; }
            public DateTime CreatedOn { get; }

            public TitleBlockInput(Point startPoint, Name testName, DateTime createdOn)
            {
                StartPoint = startPoint;
                TestName = testName;
                CreatedOn = createdOn;
            }
        }
    }
}
