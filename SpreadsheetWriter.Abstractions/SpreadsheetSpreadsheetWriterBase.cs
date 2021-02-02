using SpreadsheetWriter.Abstractions.Formula;
using System.Drawing;

namespace SpreadsheetWriter.Abstractions
{
    public abstract class SpreadsheetWriterBase : ISpreadsheetWriter
    {
        private readonly Color DefaultBackgroundColor = Color.White;
        private readonly Color DefaultFontColor = Color.Black;
        private readonly int DefaultTextRotation = 0;
        private readonly float DefaultFontSize = 11;
        private readonly int DefaultXPosition;
        private readonly int DefaultYPosition;
        protected Color CurrentBackgroundColor;
        protected Color CurrentFontColor;
        protected float CurrentFontSize;
        protected int CurrentTextRotation;

        public Point CurrentPosition { get; set; }

        public SpreadsheetWriterBase(int defaultXPosition, int defaultYPosition)
        {
            DefaultXPosition = defaultXPosition;
            DefaultYPosition = defaultYPosition;
            CurrentPosition = new Point(DefaultXPosition, DefaultYPosition);
            CurrentBackgroundColor = Color.White;
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
            CurrentBackgroundColor = color;
            return this;
        }

        public ISpreadsheetWriter SetFontColor(Color color)
        {
            CurrentFontColor = color;
            return this;
        }

        public ISpreadsheetWriter SetTextRotation(int rotation)
        {
            CurrentTextRotation = rotation;
            return this;
        }

        public ISpreadsheetWriter SetFontSize(float size)
        {
            CurrentFontSize = size;
            return this;
        }

        public ISpreadsheetWriter NewLine()
        {
            CurrentPosition = new Point(DefaultXPosition, CurrentPosition.Y + 1);
            return this;
        }

        public ISpreadsheetWriter ResetStyling()
        {
            CurrentBackgroundColor = DefaultBackgroundColor;
            CurrentFontColor = DefaultFontColor;
            CurrentTextRotation = DefaultTextRotation;
            CurrentFontSize = DefaultFontSize;

            return this;
        }

        public abstract ISpreadsheetWriter Write(decimal value);

        public abstract ISpreadsheetWriter Write(string value);

        public abstract ISpreadsheetWriter PlaceStandardFormula(Point startPosition, Point endPosition, FormulaType formulaType);

        public abstract ISpreadsheetWriter PlaceCustomFormula(IFormulaBuilder formulaBuilder);

        public abstract IExcelRange GetExcelRange(Point position);
    }
}
