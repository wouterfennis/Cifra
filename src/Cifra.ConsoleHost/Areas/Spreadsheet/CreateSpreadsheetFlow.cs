using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Spreadsheet;
using Cifra.ConsoleHost.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Spreadsheet
{
    internal class CreateSpreadsheetFlow : IFlow
    {
        private readonly IClassService _classController;
        private readonly ITestService _testController;
        private readonly ITestResultsSpreadsheetBuilder _spreadsheetFactory;

        public CreateSpreadsheetFlow(IClassService classController,
            ITestService testController,
            ITestResultsSpreadsheetBuilder spreadsheetFactory)
        {
            _classController = classController;
            _testController = testController;
            _spreadsheetFactory = spreadsheetFactory;
        }

        /// <inheritdoc/>
        public async Task StartAsync()
        {
            Console.Clear();
            Cifra.Application.Models.Class.Class chosenClass = await AskForClassAsync();
            Cifra.Application.Models.Test.Test chosenTest = await AskForTestAsync();
            Console.Clear();
            string fileName = SharedConsoleFlows.AskForString("What should be the name of the spreadsheet?");

            SaveResult saveResult = await BuildSpreadsheetAsync(chosenClass, chosenTest, fileName);

            if (saveResult.IsSuccess)
            {
                Console.WriteLine("File successfully saved.");
                SharedConsoleFlows.AskForAnyKey("Press any key to go back");
            }
            else
            {
                Console.WriteLine("File not saved due to an error.");
                SharedConsoleFlows.AskForAnyKey("Press any key to go back");
            }
        }

        private async Task<Cifra.Application.Models.Class.Class> AskForClassAsync()
        {
            Console.Clear();
            Console.WriteLine("The following classes exist:");
            GetAllClassesResult result = await _classController.GetClassesAsync();
            int index = 1;
            foreach (var @class in result.Classes)
            {
                Console.WriteLine($"[{index}] {@class.Name.Value}");
                index++;
            }
            Console.WriteLine();
            var chosenIndex = SharedConsoleFlows.AskForByte("Select one of the following classes, type a number");
            var chosenClass = result.Classes.ElementAtOrDefault(chosenIndex - 1);
            if (chosenClass == null)
            {
                Console.WriteLine("Invalid choice!");
                return await AskForClassAsync();
            }
            return chosenClass;
        }

        private async Task<Cifra.Application.Models.Test.Test> AskForTestAsync()
        {
            Console.Clear();
            Console.WriteLine("The following tests exist:");
            var result = await _testController.GetTestsAsync();
            int index = 1;
            foreach (var @test in result.Tests)
            {
                Console.WriteLine($"[{index}] {test.Name.Value}");
                index++;
            }
            var chosenIndex = SharedConsoleFlows.AskForByte("Select one of the following tests, type a number");
            var chosenTest = result.Tests.ElementAtOrDefault(chosenIndex - 1);
            if (chosenTest == null)
            {
                Console.WriteLine("Invalid choice!");
                return await AskForTestAsync();
            }
            return chosenTest;
        }

        private async Task<SaveResult> BuildSpreadsheetAsync(Cifra.Application.Models.Class.Class chosenClass, Cifra.Application.Models.Test.Test chosenTest, string fileName)
        {
            var metadata = new Metadata
            {
                Title = fileName,
                Subject = fileName,
                Author = "Todo",
                Created = DateTime.Now,
                FileName = fileName
            };

            return await _spreadsheetFactory.CreateTestResultsSpreadsheetAsync(chosenClass, chosenTest, metadata);
        }
    }
}
