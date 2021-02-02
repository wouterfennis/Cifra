using SpreadsheetWriter.Abstractions.Formula;
using System;
using System.Drawing;

namespace SpreadsheetWriter.Abstractions
{
    public interface ISpreadsheetWriter
    {
        Point CurrentPosition { get; set; }

        IExcelRange GetExcelRange(Point position);

        ISpreadsheetWriter MoveUp();

        ISpreadsheetWriter MoveUpTimes(int times);

        ISpreadsheetWriter MoveDown();

        ISpreadsheetWriter MoveDownTimes(int times);

        ISpreadsheetWriter MoveLeft();

        ISpreadsheetWriter MoveLeftTimes(int times);

        ISpreadsheetWriter MoveRight();

        ISpreadsheetWriter MoveRightTimes(int times);

        ISpreadsheetWriter SetBackgroundColor(Color color);

        ISpreadsheetWriter SetFontColor(Color color);

        ISpreadsheetWriter SetFontSize(float size);

        ISpreadsheetWriter SetTextRotation(int rotation);

        ISpreadsheetWriter NewLine();

        ISpreadsheetWriter ResetStyling();

        ISpreadsheetWriter Write(decimal value);

        ISpreadsheetWriter Write(string value);

        ISpreadsheetWriter PlaceStandardFormula(Point startPosition, Point endPosition, FormulaType formulaType);

        ISpreadsheetWriter PlaceCustomFormula(IFormulaBuilder customFormula);
    }
}
