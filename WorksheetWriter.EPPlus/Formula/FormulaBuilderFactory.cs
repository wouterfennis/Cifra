using SpreadsheetWriter.Abstractions.Formula;
using System.Text;

namespace SpreadsheetWriter.EPPlus.Formula
{
    public class FormulaBuilderFactory : IFormulaBuilderFactory
    {
        public IFormulaBuilder Create()
        {
            return new FormulaBuilder();
        }
    }
}
