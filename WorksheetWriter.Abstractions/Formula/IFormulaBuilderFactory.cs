
namespace SpreadsheetWriter.Abstractions.Formula
{
    /// <summary>
    /// Factory for creating IFormulaBuilder
    /// </summary>
    public interface IFormulaBuilderFactory
    {
        /// <summary>
        /// Creates a IFormulaBuilder
        /// </summary>
        IFormulaBuilder Create();
    }
}
