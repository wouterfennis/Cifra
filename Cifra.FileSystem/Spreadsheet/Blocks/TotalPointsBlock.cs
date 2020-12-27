using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.Abstractions;
using System;
using System.Drawing;
using static Cifra.FileSystem.Spreadsheet.Blocks.TitleBlock;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write total points.
    /// </summary>
    internal class TotalPointsBlock
    {
        private readonly TotalPointsBlockInput input;

        public TotalPointsBlock(TotalPointsBlockInput input)
        {
            this.input = input;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = input.StartPoint;
            spreadsheetWriter
                .Write("Totaal")
                .MoveRight();
            for (int i = 0; i < input.NumberOfStudents; i++)
            {
                var startPosition = new Point(spreadsheetWriter.CurrentPosition.X, input.FirstScorePoint.Y);
                var endPosition = new Point(spreadsheetWriter.CurrentPosition.X, spreadsheetWriter.CurrentPosition.Y - 1);
                spreadsheetWriter
                    .PlaceStandardFormula(startPosition, endPosition, FormulaType.SUM)
                    .MoveRight();
            }
        }

        public class TotalPointsBlockInput
        {
            public Point StartPoint { get; }
            public Point FirstScorePoint { get; }

            public int NumberOfStudents { get; }

            public TotalPointsBlockInput(Point startPoint, Point firstScorePoint)
            {
                StartPoint = startPoint;
                FirstScorePoint = firstScorePoint;
            }
        }
    }
}
