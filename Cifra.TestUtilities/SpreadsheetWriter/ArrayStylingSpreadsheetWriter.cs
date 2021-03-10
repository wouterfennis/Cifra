﻿using System.Drawing;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;

namespace SpreadsheetWriter.Test
{
    /// <summary>
    /// Test implementation of the spreadsheet writer in order to assert certain contents in the spreadsheet.
    /// </summary>
    public class ArrayStylingSpreadsheetWriter : SpreadsheetWriterBase
    {
        public string[,] Spreadsheet { get; }
        private const int DefaultXPosition = 0;
        private const int DefaultYPosition = 0;

        public ArrayStylingSpreadsheetWriter(string[,] spreadsheet) : base(DefaultXPosition, DefaultYPosition)
        {
            Spreadsheet = spreadsheet;
            CurrentPosition = new Point(DefaultXPosition, DefaultYPosition);
        }

        public override ISpreadsheetWriter Write(decimal value)
        {
            string stylingString = GenerateStylingString();
            Spreadsheet[CurrentPosition.X, CurrentPosition.Y] = stylingString;
            return this;
        }

        public override ISpreadsheetWriter Write(string value)
        {
            string stylingString = GenerateStylingString();
            Spreadsheet[CurrentPosition.X, CurrentPosition.Y] = stylingString;
            return this;
        }

        private string GenerateStylingString()
        {
            return $"{CurrentBorderDirection} + {CurrentBorderStyle}";
        }

        public override ICellRange GetCellRange(Point position)
        {
            return new TestExcelRange(position.ToString(), string.Empty);
        }

        public override ISpreadsheetWriter PlaceStandardFormula(Point startPosition, Point endPosition, FormulaType formulaType)
        {
            throw new System.NotImplementedException();
        }

        public override ISpreadsheetWriter PlaceCustomFormula(IFormulaBuilder formulaBuilder)
        {
            throw new System.NotImplementedException();
        }
    }
}
