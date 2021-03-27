using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using SpreadsheetWriter.Abstractions.Cell;
using SpreadsheetWriter.Abstractions.Formula;

namespace Cifra.TestUtilities.SpreadsheetWriter
{
    public static class FormulaBuilderTestUtilities
    {
        public static void SetupFormulaBuilder(Mock<IFormulaBuilder> formulaBuilder, string expectedFormula)
        {
            formulaBuilder.Setup(x => x.AddCellAddress(It.IsAny<ICellAddress>()))
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddConstantSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddCellColumnLetter(It.IsAny<ColumnLetter>()))
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddRowNumber(It.IsAny<RowNumber>()))
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddClosingParenthesis())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddDivisionSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddEqualsSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddMultiplicationSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddOpenParenthesis())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddSubtractionSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddSummationSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddSummationSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.Build())
                .Returns(expectedFormula);
        }
    }
}
