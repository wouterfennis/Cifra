using Cifra.ConsoleHost.Areas.Class;
using Cifra.ConsoleHost.Areas.Spreadsheet;
using Cifra.ConsoleHost.Areas.Test;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost
{
    /// <summary>
    /// The start point of the application.
    /// </summary>
    internal class Application
    {
        private readonly ClassMenuFlow _classMenuFlow;
        private readonly TestMenuFlow _testMenuFlow;
        private readonly SpreadsheetMenuFlow _spreadsheetMenuFlow;

        public Application(ClassMenuFlow classMenuFlow,
            TestMenuFlow testMenuFlow,
            SpreadsheetMenuFlow spreadsheetMenuFlow)
        {
            _classMenuFlow = classMenuFlow;
            _testMenuFlow = testMenuFlow;
            _spreadsheetMenuFlow = spreadsheetMenuFlow;
        }

        public async Task StartAsync()
        {
            Console.Clear();
            PrintTitle();
            Console.WriteLine("What would you like to do? Type the number");
            Console.WriteLine($"[{(int)AreaMenuOption.Class}] - Open the Class menu");
            Console.WriteLine($"[{(int)AreaMenuOption.Test}] - Open the Test menu");
            Console.WriteLine($"[{(int)AreaMenuOption.Spreadsheet}] - Open the spreadsheet menu");
            Console.WriteLine($"[{(int)AreaMenuOption.Quit}] - Quit application");
            var option = Console.ReadLine();
            await RedirectToArea(option);
        }

        private static void PrintTitle()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            string title = $@"
         _______         ________   
        /  _____\   ___ /\   ____\ _______  
       /\  \    /  /\__\\ \  \___ /\   ___\  ______ 
      /  \  \__/__ \/\  \\ \   __\\ \  \__/ /  __  \
      \   \________\\ \  \\ \  \_/ \ \  \  /\  \L\  \
       \  /        / \ \__\\ \__\   \ \ _\ \ \__/.\__\
        \/________/   \/__/ \/__/    \/__/  \/__/\/__/ v{version}
-------------------------------------------------------------
Created by: Wouter Fennis";
            Console.WriteLine(title);
            Console.WriteLine();
        }

        private async Task RedirectToArea(string option)
        {
            if (!int.TryParse(option, out int result))
            {
                await RetryMenuAsync();
                return;
            }
            switch (result)
            {
                case (int)AreaMenuOption.Class:
                    await _classMenuFlow.StartAsync();
                    await StartAsync();
                    break;
                case (int)AreaMenuOption.Test:
                    await _testMenuFlow.StartAsync();
                    await StartAsync();
                    break;
                case (int)AreaMenuOption.Spreadsheet:
                    await _spreadsheetMenuFlow.StartAsync();
                    await StartAsync();
                    break;
                case (int)AreaMenuOption.Quit:
                    break;
                default:
                    await RetryMenuAsync();
                    break;
            }
        }

        private async Task RetryMenuAsync()
        {
            Console.Clear();
            Console.WriteLine("Invalid choice!");
            await StartAsync();
        }
    }
}