using System.Drawing;
using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.EPPlus.Extensions;

namespace SpreadsheetWriter.EPPlus
{
    /// <summary>
    /// Spreadsheet writer for the OfficeOpenXml library.
    /// </summary>
    public class ExcelSpreadsheetWriter : SpreadsheetWriterBase
    {
        private const int DefaultXPosition = 1;
        private const int DefaultYPosition = 1;
        private readonly ExcelWorksheet _excelWorksheet;

        private ExcelRange CurrentCell { get => _excelWorksheet.GetCell(CurrentPosition); }

        public ExcelSpreadsheetWriter(ExcelWorksheet excelWorksheet) : base(DefaultXPosition, DefaultYPosition)
        {
            _excelWorksheet = excelWorksheet;
            CurrentPosition = new Point(DefaultXPosition, DefaultYPosition);
        }

        /// <inheritdoc/>
        public override ICellRange GetCellRange(Point position)
        {
            return new ExcelRangeWrapper(_excelWorksheet.GetCell(position));
        }

        /// <inheritdoc/>
        public override ISpreadsheetWriter Write(decimal value)
        {
            ApplyCellStyling();
            CurrentCell.Value = value;
            return this;
        }

        /// <inheritdoc/>
        public override ISpreadsheetWriter Write(string value)
        {
            ApplyCellStyling();
            CurrentCell.Value = value;
            return this;
        }

        /// <inheritdoc/>
        public override ISpreadsheetWriter PlaceStandardFormula(Point startPosition, Point endPosition, FormulaType formulaType)
        {
            var startCell = _excelWorksheet.GetCell(startPosition);
            var endCell = _excelWorksheet.GetCell(endPosition);
            var resultCell = _excelWorksheet.GetCell(CurrentPosition);

            var formula = $"={formulaType}({startCell.Address}:{endCell.Address})";
            resultCell.Formula = formula;

            return this;
        }

        /// <inheritdoc/>
        public override ISpreadsheetWriter PlaceCustomFormula(IFormulaBuilder formulaBuilder)
        {
            var resultCell = _excelWorksheet.GetCell(CurrentPosition);

            resultCell.Formula = formulaBuilder.Build();
            return this;
        }

        /// <inheritdoc/>
        private void ApplyCellStyling()
        {
            CurrentCell.SetBackgroundColor(CurrentBackgroundColor);
            CurrentCell.SetFontColor(CurrentFontColor);
            CurrentCell.Style.TextRotation = CurrentTextRotation;
            CurrentCell.SetFontSize(CurrentFontSize);
        }
    }
}
