using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.EPPlus.Extensions;
using System.Drawing;

namespace SpreadsheetWriter.EPPlus
{
    public class ExcelSpreadsheetWriter : ISpreadsheetWriter
    {
        private readonly ExcelWorksheet _excelWorksheet;
        private Color _currentBackgroundColor;
        private Color _currentFontColor;
        private readonly int DefaultXPosition = 1;
        private readonly int DefaultYPosition = 1;

        public Point CurrentPosition { get; set; }

        public ExcelRange CurrentCell { get => _excelWorksheet.GetCell(CurrentPosition); }

        public ExcelSpreadsheetWriter(ExcelWorksheet excelWorksheet)
        {
            _excelWorksheet = excelWorksheet;
            CurrentPosition = new Point(DefaultXPosition, DefaultYPosition);
            _currentBackgroundColor = Color.White;
        }

        public ISpreadsheetWriter MoveDown()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
            return this;
        }

        public ISpreadsheetWriter MoveUp()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
            return this;
        }

        public ISpreadsheetWriter MoveLeft()
        {
            CurrentPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
            return this;
        }

        public ISpreadsheetWriter MoveRight()
        {
            CurrentPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
            return this;
        }

        public ISpreadsheetWriter SetBackgroundColor(Color color)
        {
            _currentBackgroundColor = color;
            return this;
        }

        public ISpreadsheetWriter SetFontColor(Color color)
        {
            _currentFontColor = color;
            return this;
        }

        public ISpreadsheetWriter Write(decimal value)
        {
            CurrentCell.ConvertToEuro();
            CurrentCell.SetBackgroundColor(_currentBackgroundColor);
            CurrentCell.SetFontColor(_currentFontColor);
            CurrentCell.Value = value;
            return this;
        }

        public ISpreadsheetWriter Write(string value)
        {
            CurrentCell.SetBackgroundColor(_currentBackgroundColor);
            CurrentCell.Value = value;
            return this;
        }

        public ISpreadsheetWriter NewLine()
        {
            CurrentPosition = new Point(DefaultXPosition, CurrentPosition.Y + 1);
            return this;
        }

        public ISpreadsheetWriter PlaceFormula(Point startPosition, Point endPosition, FormulaType formulaType)
        {
            var startCell = _excelWorksheet.GetCell(startPosition);
            var endCell = _excelWorksheet.GetCell(endPosition);
            var resultCell = _excelWorksheet.GetCell(CurrentPosition);

            Write(0);
            var formula = $"={formulaType}({startCell.Address}:{endCell.Address})";
            resultCell.Formula = formula;
            return this;
        }
    }
}
