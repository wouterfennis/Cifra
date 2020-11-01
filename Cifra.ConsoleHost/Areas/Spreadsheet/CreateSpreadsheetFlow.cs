using Cifra.Application;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Spreadsheet;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Test
{
    internal class CreateSpreadsheetFlow : IFlow
    {
        private readonly ClassController _classController;
        private readonly TestController _testController;
        private readonly ITestResultsSpreadsheetFactory spreadsheetFactory;

        public CreateSpreadsheetFlow(ClassController classController,
            TestController testController,
            ITestResultsSpreadsheetFactory spreadsheetFactory)
        {
            _classController = classController;
            _testController = testController;
            this.spreadsheetFactory = spreadsheetFactory;
        }

        public async Task StartAsync()
        {
            Cifra.Application.Models.Class.Class chosenClass = await AskForClassAsync();
            Cifra.Application.Models.Test.Test chosenTest = await AskForTestAsync();
            string fileName = SharedConsoleFlows.AskForString("What should be the name of the spreadsheet?");

            await BuildSpreadsheetAsync(chosenClass, chosenTest, fileName);
        }

        private async Task<Cifra.Application.Models.Class.Class> AskForClassAsync()
        {
            Console.WriteLine("The following classes exist:");
            GetAllClassesResult result = await _classController.GetClassesAsync();
            int index = 1;
            foreach (var @class in result.Classes)
            {
                Console.WriteLine($"[{index}] {@class.Name.Value}");
                index++;
            }
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

        private async Task BuildSpreadsheetAsync(Cifra.Application.Models.Class.Class chosenClass, Cifra.Application.Models.Test.Test chosenTest, string fileName)
        {
            var metadata = new Metadata
            {
                Title = fileName,
                Subject = fileName,
                Author = "Todo",
                Created = DateTime.Now,
                FileName = fileName
            };

            await spreadsheetFactory.CreateTestResultsSpreadsheetAsync(chosenClass, chosenTest, metadata);
        }
    }
}
