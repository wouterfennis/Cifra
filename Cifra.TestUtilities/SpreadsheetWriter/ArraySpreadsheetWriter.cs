using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;
using System.Drawing;

namespace SpreadsheetWriter.Test
{
    public class ArraySpreadsheetWriter : SpreadsheetWriterBase
    {
        public string[,] Spreadsheet { get; }
        private const int DefaultXPosition = 0;
        private const int DefaultYPosition = 0;

        public ArraySpreadsheetWriter(string[,] spreadsheet) : base(DefaultXPosition, DefaultYPosition)
        {
            Spreadsheet = spreadsheet;
            CurrentPosition = new Point(DefaultXPosition, DefaultYPosition);
        }

        public override ISpreadsheetWriter Write(decimal value)
        {
            Spreadsheet[CurrentPosition.X, CurrentPosition.Y] = value.ToString();
            return this;
        }

        public override ISpreadsheetWriter Write(string value)
        {
            Spreadsheet[CurrentPosition.X, CurrentPosition.Y] = value;
            return this;
        }

        public override ISpreadsheetWriter PlaceStandardFormula(Point startPosition, Point endPosition, FormulaType formulaType)
        {
            var currentStartValue = Spreadsheet[startPosition.X, startPosition.Y];
            var newStartValue = AppendFormula(currentStartValue, $"StartStandardFormula{formulaType}");
            Spreadsheet[startPosition.X, startPosition.Y] = newStartValue;

            var currentEndValue = Spreadsheet[endPosition.X, endPosition.Y];
            var newEndValue = AppendFormula(currentEndValue, $"EndStandardFormula{formulaType}");
            Spreadsheet[endPosition.X, endPosition.Y] = newEndValue;

            Spreadsheet[CurrentPosition.X, CurrentPosition.Y] = $"Result of StandardFormula{formulaType}";
            return this;
        }

        private string AppendFormula(string currentValue, string v)
        {
            string newValue = null;
            if(currentValue != null)
            {
                newValue = $"{currentValue} AND ";
            }
            return newValue + v;
        }

        public override ISpreadsheetWriter PlaceCustomFormula(IFormulaBuilder formulaBuilder)
        {
            Spreadsheet[CurrentPosition.X, CurrentPosition.Y] = formulaBuilder.Build();
            return this;
        }

        public override IExcelRange GetExcelRange(Point position)
        {
            return new TestExcelRange(position.ToString());
        }
    }
}
