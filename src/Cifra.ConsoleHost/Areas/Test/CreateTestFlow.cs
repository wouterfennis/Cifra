using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using Cifra.ConsoleHost.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Test
{
    internal class CreateTestFlow : IFlow
    {
        private readonly ITestService _testController;

        public CreateTestFlow(ITestService classController)
        {
            _testController = classController;
        }

        public async Task StartAsync()
        {
            Console.Clear();
            int? testId = await CreateTestFlowAsync();
            Console.WriteLine("Adding assignments to the test");
            await AddAssignmentsFlowAsync(testId);
            await AddBonusAssignmentFlowAsync(testId);
        }

        private async Task<int?> CreateTestFlowAsync()
        {
            string testName = SharedConsoleFlows.AskForString("What is the name of the test?");
            int numberOfVersions = SharedConsoleFlows.AskForInteger("How many versions are there?");
            int minimumGrade = SharedConsoleFlows.AskForInteger("What is the minimum grade?");
            int standardizationFactor = SharedConsoleFlows.AskForInteger("What is the standardization factor?");

            var createTestRequest = new CreateTestCommand()
            {
                Name = testName,
                MinimumGrade = minimumGrade,
                StandardizationFactor = standardizationFactor,
                NumberOfVersions = numberOfVersions
            };
            CreateTestResult createTestResponse = await _testController.CreateTestAsync(createTestRequest);
            int? testId = createTestResponse.TestId;
            if (createTestResponse.ValidationMessages.Any())
            {
                SharedConsoleFlows.PrintValidationMessages(createTestResponse.ValidationMessages);
                testId = await CreateTestFlowAsync();
            }
            return testId;
        }

        private async Task AddAssignmentsFlowAsync(int? testId)
        {
            int numberOfAssignments = SharedConsoleFlows.AskForInteger("How many normal assignments are there?");

            for (int assignmentIndex = 0; assignmentIndex < numberOfAssignments; assignmentIndex++)
            {
                await AddAssignmentFlowAsync(testId, assignmentIndex);
            }
        }

        private async Task AddAssignmentFlowAsync(int? testId, int assignmentIndex)
        {
            int numberOfQuestions = SharedConsoleFlows.AskForInteger($"How many questions are there for assignment: {assignmentIndex + 1}?");

            var addAssignmentRequest = new AddAssignmentCommand
            {
                TestId = testId.Value,
                NumberOfQuestions = numberOfQuestions
            };

            AddAssignmentResult addAssignmentResult = await _testController.AddAssignmentAsync(addAssignmentRequest);

            if (addAssignmentResult.ValidationMessages.Any())
            {
                SharedConsoleFlows.PrintValidationMessages(addAssignmentResult.ValidationMessages);
                await AddAssignmentFlowAsync(testId, assignmentIndex);
            }
        }

        private async Task AddBonusAssignmentFlowAsync(int? testId)
        {
            bool isBonusAssignmentNeeded = SharedConsoleFlows.AskForBool($"Is there a bonus question?");

            if (isBonusAssignmentNeeded)
            {
                var addAssignmentRequest = new AddAssignmentCommand
                {
                    TestId = testId.Value,
                    NumberOfQuestions = 1
                };

                AddAssignmentResult addAssignmentResult = await _testController.AddAssignmentAsync(addAssignmentRequest);

                if (addAssignmentResult.ValidationMessages.Any())
                {
                    SharedConsoleFlows.PrintValidationMessages(addAssignmentResult.ValidationMessages);
                }
            }
        }
    }
}
