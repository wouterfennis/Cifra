namespace SpreadsheetWriter.Abstractions.Formula
{
    public interface IFormulaBuilder
    {
        IFormulaBuilder AddCellAddress(string cellAddress);
        IFormulaBuilder AddClosedParenthesis();
        IFormulaBuilder AddDivideSign();
        IFormulaBuilder AddMultiplySign();
        IFormulaBuilder AddOpenParenthesis();
        IFormulaBuilder AddSubtractSign();
        IFormulaBuilder AddSumSign();
        IFormulaBuilder AddEqualsSign();
        string Build();
    }
}