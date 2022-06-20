using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    internal class BlockInputBase
    {
        public Point StartPoint { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public BlockInputBase(Point startPoint)
        {
            StartPoint = startPoint;
        }
    }
}