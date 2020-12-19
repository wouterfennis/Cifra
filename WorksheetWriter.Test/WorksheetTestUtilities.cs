using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadsheetWriter.Test
{
    /// <summary>
    /// Helper class to debug worksheets.
    /// </summary>
    public static class WorksheetTestUtilities
    {
        /// <summary>
        /// Print a worksheet in the console.
        /// </summary>
        public static void PrintArrayWorksheet(string[,] worksheet)
        {
            int rowLength = worksheet.GetLength(0);
            int collumnLength = worksheet.GetLength(1);
            for (int y = 0; y < collumnLength; y++)
            {
                for (int x = 0; x < rowLength; x++)
                {
                    Console.Write($"| {worksheet[x, y]} ");
                    if (x == rowLength)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
