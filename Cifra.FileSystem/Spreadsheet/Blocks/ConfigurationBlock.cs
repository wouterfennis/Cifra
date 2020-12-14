using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.Abstractions;
using System;
using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the configuration.
    /// </summary>
    internal class ConfigurationBlock
    {
        private readonly ConfigurationBlockInput input;
        public Point MaximumPointsPosition { get; private set; }
        public Point StandardizationfactorPosition { get; private set; }
        public Point MinimumGradePosition { get; private set; }

        public ConfigurationBlock(ConfigurationBlockInput input)
        {
            this.input = input;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = input.StartPoint;
            spreadsheetWriter
                .NewLine()
                .NewLine()
                .SetBackgroundColor(Color.LightGray)
                .Write("Configuratie")
                .Write("Maximale punten")
                .MoveRight()
                .Write(input.MaximumPoints);
            MaximumPointsPosition = spreadsheetWriter.CurrentPosition;

            spreadsheetWriter
                .NewLine()
                .Write("Normering")
                .MoveRight()
                .Write(input.StandardizationFactor.Value);
            StandardizationfactorPosition = spreadsheetWriter.CurrentPosition;

            spreadsheetWriter
                .NewLine()
                .Write("Minimale cijfer")
                .MoveRight()
                .Write(input.MinimumGrade.Value)
                .SetBackgroundColor(Color.White);
            MinimumGradePosition = spreadsheetWriter.CurrentPosition;
        }

        public class ConfigurationBlockInput : BlockInputBase
        {
            public decimal MaximumPoints { get; }
            public StandardizationFactor StandardizationFactor { get; }
            public Grade MinimumGrade { get; }

            public ConfigurationBlockInput(Point startPoint,
                decimal maximumPoints,
                StandardizationFactor standardizationFactor,
                Grade minimumGrade) : base(startPoint)
            {
                MaximumPoints = maximumPoints;
                StandardizationFactor = standardizationFactor;
                MinimumGrade = minimumGrade;
            }
        }
    }
}
