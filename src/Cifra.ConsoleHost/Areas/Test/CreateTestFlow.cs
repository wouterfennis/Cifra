using System;
using System.Linq;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test.Results;
using Cifra.ConsoleHost.Utilities;

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
            Guid testId = await CreateTestFlowAsync();
            Console.WriteLine("Adding assignments to the test");
            await AddAssignmentsFlowAsync(testId);
            await AddBonusAssignmentFlowAsync(testId);
        }

        private async Task<Guid> CreateTestFlowAsync()
        {
            string testName = SharedConsoleFlows.AskForString("What is the name of the test?");
            byte numberOfVersions = SharedConsoleFlows.AskForByte("How many versions are there?");
            byte minimumGrade = SharedConsoleFlows.AskForByte("What is the minimum grade?");
            byte standardizationFactor = SharedConsoleFlows.AskForByte("What is the standardization factor?");

            var createTestRequest = new CreateTestRequest()
            {
                Name = testName,
                MinimumGrade = minimumGrade,
                StandardizationFactor = standardizationFactor,
                NumberOfVersions = numberOfVersions
            };
            CreateTestResult createTestResponse = await _testController.CreateTestAsync(createTestRequest);
            Guid testId = createTestResponse.TestId;
            if (createTestResponse.ValidationMessages.Any())
            {
                SharedConsoleFlows.PrintValidationMessages(createTestResponse.ValidationMessages);
                testId = await CreateTestFlowAsync();
            }
            return testId;
        }

        private async Task AddAssignmentsFlowAsync(Guid testId)
        {
            byte numberOfAssignments = SharedConsoleFlows.AskForByte("How many normal assignments are there?");

            for (int assignmentIndex = 0; assignmentIndex < numberOfAssignments; assignmentIndex++)
            {
                await AddAssignmentFlowAsync(testId, assignmentIndex);
            }
        }

        private async Task AddAssignmentFlowAsync(Guid testId, int assignmentIndex)
        {
            byte numberOfQuestions = SharedConsoleFlows.AskForByte($"How many questions are there for assignment: {assignmentIndex + 1}?");

            var addAssignmentRequest = new AddAssignmentRequest
            {
                TestId = testId,
                NumberOfQuestions = numberOfQuestions
            };

            AddAssignmentResult addAssignmentResult = await _testController.AddAssignmentAsync(addAssignmentRequest);

            if (addAssignmentResult.ValidationMessages.Any())
            {
                SharedConsoleFlows.PrintValidationMessages(addAssignmentResult.ValidationMessages);
                await AddAssignmentFlowAsync(testId, assignmentIndex);
            }
        }

        private async Task AddBonusAssignmentFlowAsync(Guid testId)
        {
            bool isBonusAssignmentNeeded = SharedConsoleFlows.AskForBool($"Is there a bonus question?");

            if (isBonusAssignmentNeeded)
            {
                var addAssignmentRequest = new AddAssignmentRequest
                {
                    TestId = testId,
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
