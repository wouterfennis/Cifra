using System.Drawing;
using SpreadsheetWriter.Abstractions.Formula;

namespace SpreadsheetWriter.Abstractions
{
    /// <inheritdoc/>
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
        
        /// <summary>
        /// The position of the current selected Cell
        /// </summary>
        public Point CurrentPosition { get; set; }

        public SpreadsheetWriterBase(int defaultXPosition, int defaultYPosition)
        {
            DefaultXPosition = defaultXPosition;
            DefaultYPosition = defaultYPosition;
            CurrentPosition = new Point(DefaultXPosition, DefaultYPosition);
            CurrentBackgroundColor = Color.White;
        }

        /// <inheritdoc/>
        public abstract ICellRange GetCellRange(Point position);

        /// <inheritdoc/>
        public ISpreadsheetWriter MoveDown()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter MoveDownTimes(int times)
        {
            for (int i = 0; i < times; i++)
            {
                MoveDown();
            }
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter MoveUp()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter MoveUpTimes(int times)
        {
            for (int i = 0; i < times; i++)
            {
                MoveUp();
            }
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter MoveLeft()
        {
            CurrentPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter MoveLeftTimes(int times)
        {
            for (int i = 0; i < times; i++)
            {
                MoveLeft();
            }
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter MoveRight()
        {
            CurrentPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter MoveRightTimes(int times)
        {
            for (int i = 0; i < times; i++)
            {
                MoveRight();
            }
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter SetBackgroundColor(Color color)
        {
            CurrentBackgroundColor = color;
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter SetFontColor(Color color)
        {
            CurrentFontColor = color;
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter SetTextRotation(int rotation)
        {
            CurrentTextRotation = rotation;
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter SetFontSize(float size)
        {
            CurrentFontSize = size;
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter NewLine()
        {
            CurrentPosition = new Point(DefaultXPosition, CurrentPosition.Y + 1);
            return this;
        }

        /// <inheritdoc/>
        public ISpreadsheetWriter ResetStyling()
        {
            CurrentBackgroundColor = DefaultBackgroundColor;
            CurrentFontColor = DefaultFontColor;
            CurrentTextRotation = DefaultTextRotation;
            CurrentFontSize = DefaultFontSize;

            return this;
        }

        /// <inheritdoc/>
        public abstract ISpreadsheetWriter Write(decimal value);

        /// <inheritdoc/>
        public abstract ISpreadsheetWriter Write(string value);

        /// <inheritdoc/>
        public abstract ISpreadsheetWriter PlaceStandardFormula(Point startPosition, Point endPosition, FormulaType formulaType);

        /// <inheritdoc/>
        public abstract ISpreadsheetWriter PlaceCustomFormula(IFormulaBuilder formulaBuilder);

    }
}
