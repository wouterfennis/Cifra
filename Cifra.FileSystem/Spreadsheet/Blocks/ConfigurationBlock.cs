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
        public Point StartPoint { get; set; }
        public StandardizationFactor StandardizationFactor { get; }
        public Grade MinimumGrade { get; }
        public Point StandardizationfactorPosition { get; private set; }
        public Point MinimumGradePosition { get; private set; }

        public ConfigurationBlock(Point startPoint,
                StandardizationFactor standardizationFactor,
                Grade minimumGrade)
        {
            StartPoint = startPoint;
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPoint;
            spreadsheetWriter
                .SetBackgroundColor(Color.LightGray)
                .SetFontBold(true)
                .Write("Configuratie");

            spreadsheetWriter
                .NewLine()
                .Write("Normering")
                .SetFontBold(false)
                .MoveRight()
                .Write(StandardizationFactor.Value);
            StandardizationfactorPosition = spreadsheetWriter.CurrentPosition;

            spreadsheetWriter
                .NewLine()
                .SetFontBold(true)
                .Write("Minimale cijfer")
                .SetFontBold(false)
                .MoveRight()
                .Write(MinimumGrade.Value)
                .SetBackgroundColor(Color.White);
            MinimumGradePosition = spreadsheetWriter.CurrentPosition;
        }
    }
}
