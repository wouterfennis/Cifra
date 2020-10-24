using Cifra.ConsoleHost.Areas.Class;
using Cifra.ConsoleHost.Areas.Test;
using System;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Spreadsheet
{
    internal class SpreadsheetMenuFlow : IFlow
    {
        private readonly CreateSpreadsheetFlow _createSpreadsheetFlow;

        public SpreadsheetMenuFlow(CreateSpreadsheetFlow createSpreadsheetFlow)
        {
            _createSpreadsheetFlow = createSpreadsheetFlow;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("What would you like to do? Type the number");
            Console.WriteLine($"[{(int)SpreadsheetMenuOption.CreateSpreadsheet}] - Create a spreadsheet");
            var option = Console.ReadLine();
            await RedirectToSpreadsheetOption(option);
        }

        private async Task RedirectToSpreadsheetOption(string option)
        {
            if (!byte.TryParse(option, out byte result))
            {
                await RetryMenu();
                return;
            }
            switch (result)
            {
                case (int)SpreadsheetMenuOption.CreateSpreadsheet:
                    await _createSpreadsheetFlow.StartAsync();
                    break;
                default:
                    await RetryMenu();
                    break;
            }
        }

        private async Task RetryMenu()
        {
            Console.WriteLine("Invalid choice!");
            await StartAsync();
        }
    }
}
