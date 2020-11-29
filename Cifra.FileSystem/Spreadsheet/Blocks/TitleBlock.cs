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

        public static void Write(ISpreadsheetWriter spreadsheetWriter, TitleBlockInput input)
        {
            spreadsheetWriter.CurrentPosition = input.StartPoint;
            spreadsheetWriter
                .SetFontSize(TitleSize)
                .Write(input.TestName.Value)
                .ResetStyling()
                .NewLine()
                .Write("Gemaakt op:")
                .MoveRight()
                .Write(input.CreatedOn.ToString());
        }

        public class TitleBlockInput
        {
            public Point StartPoint { get; }
            public Name TestName { get; }
            public DateTime CreatedOn { get; }

            public TitleBlockInput(Point startPoint, Name testName, DateTime createdOn)
            {
                StartPoint = startPoint;
                StartPoint = startPoint;
                TestName = testName;
                CreatedOn = createdOn;
            }
        }
    }
}
