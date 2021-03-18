using System;
using System.Linq;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test.Requests;

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
            var testId = await CreateTestFlowAsync();
            Console.WriteLine("Adding assignments to the test");
            await AddAssignmentsFlowAsync(testId);
            await AddBonusAssignmentFlowAsync(testId);
        }

        private async Task<Guid> CreateTestFlowAsync()
        {
            var testName = SharedConsoleFlows.AskForString("What is the name of the test?");
            var numberOfVersions = SharedConsoleFlows.AskForByte("How many versions are there?");
            var minimumGrade = SharedConsoleFlows.AskForByte("What is the minimum grade?");
            var standardizationFactor = SharedConsoleFlows.AskForByte("What is the standardization factor?");

            var createTestRequest = new CreateTestRequest()
            {
                Name = testName,
                MinimumGrade = minimumGrade,
                StandardizationFactor = standardizationFactor,
                NumberOfVersions = numberOfVersions
            };
            var createTestResponse = await _testController.CreateTestAsync(createTestRequest);
            var testId = createTestResponse.TestId;
            if (createTestResponse.ValidationMessages.Count() > 0)
            {
                SharedConsoleFlows.PrintValidationMessages(createTestResponse.ValidationMessages);
                testId = await CreateTestFlowAsync();
            }
            return testId;
        }

        private async Task AddAssignmentsFlowAsync(Guid testId)
        {
            byte numberOfAssignments = SharedConsoleFlows.AskForByte("How many assignments are there?");

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

            var addAssignmentResult = await _testController.AddAssignmentAsync(addAssignmentRequest);

            if (addAssignmentResult.ValidationMessages.Count() > 0)
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
                var addBonusAssignment = new AddBonusAssignmentRequest
                {
                    TestId = testId
                };

                var addBonusAssignmentResult = await _testController.AddBonusAssignmentAsync(addBonusAssignment);

                if (addBonusAssignmentResult.ValidationMessages.Count() > 0)
                {
                    SharedConsoleFlows.PrintValidationMessages(addBonusAssignmentResult.ValidationMessages);
                }
            }
        }
    }
}
