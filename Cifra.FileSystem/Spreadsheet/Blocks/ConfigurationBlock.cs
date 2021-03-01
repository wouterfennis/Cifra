using System.Drawing;
using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.Abstractions;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the configuration.
    /// </summary>
    internal class ConfigurationBlock
    {
        private readonly ConfigurationBlockInput input;
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
                .SetBackgroundColor(Color.LightGray)
                .Write("Configuratie");

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
            public StandardizationFactor StandardizationFactor { get; }
            public Grade MinimumGrade { get; }

            public ConfigurationBlockInput(Point startPoint,
                StandardizationFactor standardizationFactor,
                Grade minimumGrade) : base(startPoint)
            {
                StandardizationFactor = standardizationFactor;
                MinimumGrade = minimumGrade;
            }
        }
    }
}
