using SpreadsheetWriter.Abstractions.Formula;
using System.Text;

namespace SpreadsheetWriter.EPPlus.Formula
{
    public class FormulaBuilder : IFormulaBuilder
    {
        private readonly StringBuilder _stringBuilder;

        public FormulaBuilder()
        {
            _stringBuilder = new StringBuilder();
        }

        public IFormulaBuilder AddCellAddress(string cellAddress)
        {
            _stringBuilder.Append(cellAddress);
            return this;
        }

        public IFormulaBuilder AddDivisionSign()
        {
            _stringBuilder.Append("/");
            return this;
        }

        public IFormulaBuilder AddMultiplicationSign()
        {
            _stringBuilder.Append("*");
            return this;
        }

        public IFormulaBuilder AddSubtractionSign()
        {
            _stringBuilder.Append("-");
            return this;
        }

        public IFormulaBuilder AddSummationSign()
        {
            _stringBuilder.Append("+");
            return this;
        }

        public IFormulaBuilder AddOpenParenthesis()
        {
            _stringBuilder.Append("(");
            return this;
        }

        public IFormulaBuilder AddClosingParenthesis()
        {
            _stringBuilder.Append(")");
            return this;
        }

        public IFormulaBuilder AddEqualsSign()
        {
            _stringBuilder.Append("=");
            return this;
        }

        public string Build()
        {
            return _stringBuilder.ToString();
        }
    }
}
