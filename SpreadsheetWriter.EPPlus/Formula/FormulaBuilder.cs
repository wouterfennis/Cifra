using System;
using System.Text;
using SpreadsheetWriter.Abstractions.Formula;

namespace SpreadsheetWriter.EPPlus.Formula
{
    /// <inheritdoc/>
    public class FormulaBuilder : IFormulaBuilder
    {
        private readonly StringBuilder _stringBuilder;

        public FormulaBuilder()
        {
            _stringBuilder = new StringBuilder();
        }

        /// <inheritdoc/>
        public IFormulaBuilder AddCellAddress(string cellAddress)
        {
            _ = cellAddress ?? throw new ArgumentNullException(nameof(cellAddress));

            _stringBuilder.Append(cellAddress);
            return this;
        }

        /// <inheritdoc/>
        public IFormulaBuilder AddDivisionSign()
        {
            _stringBuilder.Append("/");
            return this;
        }

        /// <inheritdoc/>
        public IFormulaBuilder AddMultiplicationSign()
        {
            _stringBuilder.Append("*");
            return this;
        }

        /// <inheritdoc/>
        public IFormulaBuilder AddSubtractionSign()
        {
            _stringBuilder.Append("-");
            return this;
        }

        /// <inheritdoc/>
        public IFormulaBuilder AddSummationSign()
        {
            _stringBuilder.Append("+");
            return this;
        }

        /// <inheritdoc/>
        public IFormulaBuilder AddOpenParenthesis()
        {
            _stringBuilder.Append("(");
            return this;
        }

        /// <inheritdoc/>
        public IFormulaBuilder AddClosingParenthesis()
        {
            _stringBuilder.Append(")");
            return this;
        }

        /// <inheritdoc/>
        public IFormulaBuilder AddEqualsSign()
        {
            _stringBuilder.Append("=");
            return this;
        }

        /// <inheritdoc/>
        public string Build()
        {
            return _stringBuilder.ToString();
        }
    }
}
