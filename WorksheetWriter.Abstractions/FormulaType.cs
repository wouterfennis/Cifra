﻿namespace WorksheetWriter.Abstractions
{
    /// <summary>
    /// The types of formulas supported
    /// </summary>
    public enum FormulaType
    {
        /// <summary>
        /// Calculates the sum of the selected cells
        /// </summary>
        SUM,

        /// <summary>
        /// Calculates the average of the selected cells
        /// </summary>
        AVERAGE
    }
}