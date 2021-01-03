using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;
using System.Drawing;

namespace SpreadsheetWriter.Test
{
    public class ArraySpreadsheetWriter : SpreadsheetWriterBase
    {
        public string[,] Worksheet { get; }
        private const int DefaultXPosition = 0;
        private const int DefaultYPosition = 0;

        public ArraySpreadsheetWriter(string[,] worksheet) : base(DefaultXPosition, DefaultYPosition)
        {
            Worksheet = worksheet;
            CurrentPosition = new Point(DefaultXPosition, DefaultYPosition);
        }

        public override ISpreadsheetWriter Write(decimal value)
        {
            Worksheet[CurrentPosition.X, CurrentPosition.Y] = value.ToString();
            return this;
        }

        public override ISpreadsheetWriter Write(string value)
        {
            Worksheet[CurrentPosition.X, CurrentPosition.Y] = value;
            return this;
        }

        public override ISpreadsheetWriter PlaceStandardFormula(Point startPosition, Point endPosition, FormulaType formulaType)
        {
            var currentStartValue = Worksheet[startPosition.X, startPosition.Y];
            var newStartValue = AppendFormula(currentStartValue, $"StartStandardFormula{formulaType}");
            Worksheet[startPosition.X, startPosition.Y] = newStartValue;

            var currentEndValue = Worksheet[endPosition.X, endPosition.Y];
            var newEndValue = AppendFormula(currentEndValue, $"EndStandardFormula{formulaType}");
            Worksheet[endPosition.X, endPosition.Y] = newEndValue;

            Worksheet[CurrentPosition.X, CurrentPosition.Y] = $"Result of StandardFormula{formulaType}";
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
            Worksheet[CurrentPosition.X, CurrentPosition.Y] = formulaBuilder.Build();
            return this;
        }

        public override IExcelRange GetExcelRange(Point position)
        {
            return new TestExcelRange(position.ToString());
        }
    }
}
