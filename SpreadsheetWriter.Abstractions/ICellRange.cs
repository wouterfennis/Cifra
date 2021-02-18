
namespace SpreadsheetWriter.Abstractions
{
    /// <summary>
    /// Abstraction for an range of cells.
    /// </summary>
    public interface ICellRange
    {
        /// <summary>
        /// The address of the cell range.
        /// </summary>
        string Address { get; }
    }
}