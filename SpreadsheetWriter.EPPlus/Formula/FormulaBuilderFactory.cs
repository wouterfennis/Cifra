using SpreadsheetWriter.Abstractions.Formula;

namespace SpreadsheetWriter.EPPlus.Formula
{
    /// <inheritdoc/>
    public class FormulaBuilderFactory : IFormulaBuilderFactory
    {
        /// <inheritdoc/>
        public IFormulaBuilder Create()
        {
            return new FormulaBuilder();
        }
    }
}
