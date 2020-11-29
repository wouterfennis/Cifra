using OfficeOpenXml;
using OfficeOpenXml.Style;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.EPPlus.Extensions;
using System.Drawing;

namespace SpreadsheetWriter.EPPlus
{
    public class ExcelSpreadsheetWriter : ISpreadsheetWriter
    {
        private readonly Color DefaultBackgroundColor = Color.White;
        private readonly Color DefaultFontColor = Color.Black;
        private readonly int DefaultTextRotation = 0;
        private readonly float DefaultFontSize = 11;
        private readonly int DefaultXPosition = 1;
        private readonly int DefaultYPosition = 1;
        private readonly ExcelWorksheet _excelWorksheet;
        private Color _currentBackgroundColor;
        private Color _currentFontColor;
        private float _currentFontSize;
        private int _currentTextRotation;

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

        public ISpreadsheetWriter MoveDownTimes(int times)
        {
            for (int i = 0; i < times; i++)
            {
                MoveDown();
            }
            return this;
        }

        public ISpreadsheetWriter MoveUp()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
            return this;
        }

        public ISpreadsheetWriter MoveUpTimes(int times)
        {
            for (int i = 0; i < times; i++)
            {
                MoveUp();
            }
            return this;
        }

        public ISpreadsheetWriter MoveLeft()
        {
            CurrentPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
            return this;
        }

        public ISpreadsheetWriter MoveLeftTimes(int times)
        {
            for (int i = 0; i < times; i++)
            {
                MoveLeft();
            }
            return this;
        }

        public ISpreadsheetWriter MoveRight()
        {
            CurrentPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
            return this;
        }

        public ISpreadsheetWriter MoveRightTimes(int times)
        {
            for (int i = 0; i < times; i++)
            {
                MoveRight();
            }
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

        public ISpreadsheetWriter SetTextRotation(int rotation)
        {
            _currentTextRotation = rotation;
            return this;
        }

        public ISpreadsheetWriter Write(decimal value)
        {
            CurrentCell.Value = value;
            return this;
        }

        public ISpreadsheetWriter Write(string value)
        {
            ApplyCellStyling();
            CurrentCell.Value = value;
            return this;
        }

        public ISpreadsheetWriter NewLine()
        {
            CurrentPosition = new Point(DefaultXPosition, CurrentPosition.Y + 1);
            return this;
        }

        public ISpreadsheetWriter PlaceStandardFormula(Point startPosition, Point endPosition, FormulaType formulaType)
        {
            var startCell = _excelWorksheet.GetCell(startPosition);
            var endCell = _excelWorksheet.GetCell(endPosition);
            var resultCell = _excelWorksheet.GetCell(CurrentPosition);

            var formula = $"={formulaType}({startCell.Address}:{endCell.Address})";
            resultCell.Formula = formula;
            return this;
        }

        public ISpreadsheetWriter ResetStyling()
        {
            _currentBackgroundColor = DefaultBackgroundColor;
            _currentFontColor = DefaultFontColor;
            _currentTextRotation = DefaultTextRotation;
            _currentFontSize = DefaultFontSize;
            
            return this;
        }

        public ISpreadsheetWriter PlaceCustomFormula(IFormulaBuilder formulaBuilder)
        {
            var resultCell = _excelWorksheet.GetCell(CurrentPosition);

            resultCell.Formula = formulaBuilder.Build();
            return this;
        }

        private void ApplyCellStyling()
        {
            CurrentCell.SetBackgroundColor(_currentBackgroundColor);
            CurrentCell.SetFontColor(_currentFontColor);
            CurrentCell.Style.TextRotation = _currentTextRotation;
            CurrentCell.SetFontSize(_currentFontSize);
        }
    }
}
