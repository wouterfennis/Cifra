using Cifra.Application.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.ConsoleHost
{
    internal static class SharedConsoleFlows
    {
        public static bool AskForBool()
        {
            const string Yes = "Y";
            const string No = "N";
            Console.WriteLine($"Type {Yes} or {No}");
            string input = Console.ReadLine();
            if (input.ToUpperInvariant() == Yes)
            {
                return true;
            }
            else if (input.ToUpperInvariant() == No)
            {
                return false;
            }
            else
            {
                return AskForBool();
            }
        }

        public static void PrintValidationMessages(IEnumerable<ValidationMessage> validationMessages)
        {
            foreach (ValidationMessage validationMessage in validationMessages)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Some of the input was not valid:");
                Console.WriteLine($"{validationMessage.Message} on the following field: {validationMessage.Field} ");
                Console.ResetColor();
            }
        }
    }
}
