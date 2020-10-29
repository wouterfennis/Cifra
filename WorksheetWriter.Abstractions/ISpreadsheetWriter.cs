using System;
using System.Drawing;

namespace SpreadsheetWriter.Abstractions
{
    public interface ISpreadsheetWriter
    {
        Point CurrentPosition { get; set; }

        ISpreadsheetWriter MoveUp();

        ISpreadsheetWriter MoveDown();

        ISpreadsheetWriter MoveLeft();

        ISpreadsheetWriter MoveRight();

        ISpreadsheetWriter NewLine();

        ISpreadsheetWriter SetBackgroundColor(Color color);

        ISpreadsheetWriter SetFontColor(Color color);

        ISpreadsheetWriter Write(decimal value);

        ISpreadsheetWriter Write(string value);

        ISpreadsheetWriter PlaceFormula(Point startPosition, Point endPosition, FormulaType formulaType);
    }
}
