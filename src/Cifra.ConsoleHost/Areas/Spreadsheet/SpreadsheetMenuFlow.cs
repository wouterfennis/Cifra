using System;
using System.Threading.Tasks;
using Cifra.ConsoleHost.Areas.Class;

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
            Console.Clear();
            Console.WriteLine("What would you like to do? Type the number");
            Console.WriteLine($"[{(int)SpreadsheetMenuOption.CreateSpreadsheet}] - Create a spreadsheet");
            Console.WriteLine($"[{(int)SpreadsheetMenuOption.GoBack}] - Go back");
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
                    await RetryMenu();
                    break;
                case (int)SpreadsheetMenuOption.GoBack:
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
