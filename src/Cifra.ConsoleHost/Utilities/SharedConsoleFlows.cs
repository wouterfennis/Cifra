using Cifra.Application.Models.Validation;
using Cifra.Application.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.ConsoleHost.Utilities
{
    /// <summary>
    /// Console logic that is recurring in the application.
    /// </summary>
    internal static class SharedConsoleFlows
    {
        private const string InvalidInputMessage = "Invalid Input";
        private const ConsoleColor WarningColor = ConsoleColor.Yellow;

        /// <summary>
        /// Asks for a binary choice (boolean).
        /// </summary>
        /// <returns>The parsed boolean</returns>
        public static bool AskForBool(string question)
        {
            Console.WriteLine(question);
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
                Console.WriteLine(InvalidInputMessage);
                return AskForBool(question);
            }
        }

        /// <summary>
        /// Asks for a small number.
        /// </summary>
        /// <returns>The parsed number</returns>
        public static byte AskForByte(string question)
        {
            if (question != null)
            {
                Console.WriteLine(question);
            }
            string input = Console.ReadLine();

            if (!byte.TryParse(input, out byte result))
            {
                Console.WriteLine(InvalidInputMessage);
                return AskForByte(question);
            }
            return result;
        }

        /// <summary>
        /// Asks for a string.
        /// </summary>
        /// <returns>The string</returns>
        public static string AskForString(string question)
        {
            var input = AskForOptionalString(question);

            if (string.IsNullOrWhiteSpace(input) || input == string.Empty)
            {
                Console.WriteLine(InvalidInputMessage);
                return AskForString(question);
            }
            return input;
        }

        /// <summary>
        /// Asks for any key.
        /// </summary>
        /// <returns>The string</returns>
        public static void AskForAnyKey(string question)
        {
            if (question != null)
            {
                Console.WriteLine(question);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Asks for an optional string.
        /// </summary>
        /// <returns>The string</returns>
        public static string AskForOptionalString(string question)
        {
            if (question != null)
            {
                Console.WriteLine(question);
            }
            string input = Console.ReadLine();

            return input;
        }

        public static void PrintValidationMessages(IEnumerable<ValidationMessage> validationMessages)
        {
            foreach (ValidationMessage validationMessage in validationMessages)
            {
                Console.ForegroundColor = WarningColor;
                Console.WriteLine("Some of the input was not valid:");
                Console.WriteLine($"{validationMessage.Message} on the following field: {validationMessage.Field} ");
                Console.ResetColor();
            }
        }
    }
}
