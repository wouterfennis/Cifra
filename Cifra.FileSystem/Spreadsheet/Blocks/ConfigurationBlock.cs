using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.Abstractions;
using System;
using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the title.
    /// </summary>
    internal class ConfigurationBlock
    {
        private const int TitleSize = 16;

        public static void Write(ISpreadsheetWriter spreadsheetWriter, ConfigurationBlockInput input)
        {
            spreadsheetWriter.CurrentPosition = input.StartPoint;
        }

        public class ConfigurationBlockInput : BlockInputBase
        {
            public decimal MaximumPoints { get; }
            public StandardizationFactor StandardizationFactor { get; }
            public Grade MinimumGrade { get; }

            public ConfigurationBlockInput(Point startPoint, 
                decimal maximumPoints, 
                StandardizationFactor standardizationFactor,
                Grade minimumGrade) : base (startPoint)
            {
                MaximumPoints = maximumPoints;
                StandardizationFactor = standardizationFactor;
                MinimumGrade = minimumGrade;
            }
        }
    }
}
