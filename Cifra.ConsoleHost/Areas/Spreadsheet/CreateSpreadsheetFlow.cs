using Cifra.Application;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.ConsoleHost.Areas;
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

        public CreateSpreadsheetFlow(ClassController classController, TestController testController)
        {
            _classController = classController;
            _testController = testController;
        }

        public async Task StartAsync()
        {
            var chosenClass = await AskForClassAsync();
            var chosenTest = await AskForTestAsync();

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

        private async Task<Guid> CreateTestFlowAsync()
        {
            var testName = SharedConsoleFlows.AskForString("What is the name of the test?");
            var minimumGrade = SharedConsoleFlows.AskForByte("What is the minimum grade?");
            var standardizationFactor = SharedConsoleFlows.AskForByte("What is the standardization factor?");

            var createTestRequest = new CreateTestRequest()
            {
                Name = testName,
                MinimumGrade = minimumGrade,
                StandardizationFactor = standardizationFactor
            };
            var createTestResponse = await _testController.CreateTestAsync(createTestRequest);
            var classId = createTestResponse.TestId;
            if (createTestResponse.ValidationMessages.Count() > 0)
            {
                SharedConsoleFlows.PrintValidationMessages(createTestResponse.ValidationMessages);
                classId = await CreateTestFlowAsync();
            }
            return classId;
        }

        private async Task AddQuestionsFlowAsync(Guid classId)
        {
            await AddQuestionFlowAsync(classId);
            bool addAnotherQuestion = SharedConsoleFlows.AskForBool("Add another question?");

            if (addAnotherQuestion)
            {
                await AddQuestionsFlowAsync(classId);
            }
        }

        private async Task AddQuestionFlowAsync(Guid testId)
        {
            IEnumerable<string> names = CollectQuestionNames();
            var maximalScore = SharedConsoleFlows.AskForByte("What is the maximal score of the question?");
            var model = new AddQuestionRequest
            {
                TestId = testId,
                Names = names,
                MaximalScore = maximalScore
            };
            AddQuestionResult addQuestionResponse = await _testController.AddQuestionAsync(model);

            if (addQuestionResponse.ValidationMessages.Count() > 0)
            {
                SharedConsoleFlows.PrintValidationMessages(addQuestionResponse.ValidationMessages);
                await AddQuestionFlowAsync(testId);
            }
        }

        private IEnumerable<string> CollectQuestionNames()
        {
            var names = new List<string>();
            var name = SharedConsoleFlows.AskForString("Type an name for the question");
            names.Add(name);
            var addAnotherName = SharedConsoleFlows.AskForBool("Add an additional name to the question?");
            if (addAnotherName)
            {
                names.AddRange(CollectQuestionNames());
            }
            return names;
        }
    }
}
