using SpreadsheetWriter.Abstractions;
using System.Text;

namespace SpreadsheetWriter.EPPlus
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

        public IFormulaBuilder AddDivideSign()
        {
            _stringBuilder.Append("/");
            return this;
        }

        public IFormulaBuilder AddMultiplySign()
        {
            _stringBuilder.Append("*");
            return this;
        }

        public IFormulaBuilder AddSubtractSign()
        {
            _stringBuilder.Append("-");
            return this;
        }

        public IFormulaBuilder AddSumSign()
        {
            _stringBuilder.Append("+");
            return this;
        }

        public IFormulaBuilder AddOpenParenthesis()
        {
            _stringBuilder.Append("(");
            return this;
        }

        public IFormulaBuilder AddClosedParenthesis()
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
