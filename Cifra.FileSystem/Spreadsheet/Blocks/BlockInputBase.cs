using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    public class BlockInputBase
    {
        public Point StartPoint { get; }

        public BlockInputBase(Point startPoint)
        {
            StartPoint = startPoint;
        }
    }
}