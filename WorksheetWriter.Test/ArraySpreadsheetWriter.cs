using SpreadsheetWriter.Abstractions;
using System;
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
            throw new NotImplementedException();
        }

        public override ISpreadsheetWriter PlaceCustomFormula(IFormulaBuilder formulaBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
