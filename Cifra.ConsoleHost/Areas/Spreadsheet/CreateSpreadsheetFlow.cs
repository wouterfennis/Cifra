using Cifra.Application;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.ConsoleHost.Areas;
using Cifra.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Test
{
    internal class CreateSpreadsheetFlow : IFlow
    {
        private readonly ClassController _classController;
        private readonly TestController _testController;

        public CreateSpreadsheetFlow(ClassController classController,
            TestController testController)
        {
            _classController = classController;
            _testController = testController;
        }

        public async Task StartAsync()
        {
            Cifra.Application.Models.Class.Class chosenClass = await AskForClassAsync();
            Cifra.Application.Models.Test.Test chosenTest = await AskForTestAsync();
            string fileName = SharedConsoleFlows.AskForString("What should be the name of the spreadsheet?");

            BuildSpreadsheet(chosenClass, chosenTest);

            Console.WriteLine("No implementation yet! Try again in next version");
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

        private void BuildSpreadsheet(Cifra.Application.Models.Class.Class chosenClass, Cifra.Application.Models.Test.Test chosenTest)
        {
            throw new NotImplementedException();
        }
    }
}
