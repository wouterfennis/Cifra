using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadsheetWriter.Test
{
    /// <summary>
    /// Helper class to debug spreadsheets.
    /// </summary>
    public static class SpreadsheetTestUtilities
    {
        /// <summary>
        /// Print a array spreadsheet in the console.
        /// </summary>
        public static void PrintArraySpreadsheet(string[,] worksheet)
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
