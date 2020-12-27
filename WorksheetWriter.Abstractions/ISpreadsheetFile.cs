using SpreadsheetWriter.Abstractions;
using System;
using System.Threading.Tasks;

namespace SpreadsheetWriter.Abstractions
{
    /// <summary>
    /// Abstraction around SpreadsheetFile instance.
    /// </summary>
    public interface ISpreadsheetFile : IDisposable
    {
        /// <summary>
        /// Gets the writer from the spreadsheet file.
        /// </summary>
        ISpreadsheetWriter GetSpreadsheetWriter();

        /// <summary>
        /// Saves the SpreadsheetFile
        /// </summary>
        Task SaveAsync();
    }
}