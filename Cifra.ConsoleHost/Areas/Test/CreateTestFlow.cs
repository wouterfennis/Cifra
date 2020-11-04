﻿using Cifra.Application;
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
    internal class CreateTestFlow : IFlow
    {
        private readonly TestController _testController;

        public CreateTestFlow(TestController classController)
        {
            _testController = classController;
        }

        public async Task StartAsync()
        {
            var testId = await CreateTestFlowAsync();
            Console.WriteLine("Adding questions to the test");
            await AddAssignmentsFlowAsync(testId);
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

        private async Task AddAssignmentsFlowAsync(Guid testId)
        {
            await AddAssignmentFlowAsync(testId);
            bool addAnotherAssignment = SharedConsoleFlows.AskForBool("Add another assignment?");

            if (addAnotherAssignment)
            {
                await AddAssignmentsFlowAsync(testId);
            }
        }

        private async Task AddAssignmentFlowAsync(Guid testId)
        {
            var addAssignmentRequest = new AddAssignmentRequest();
            var addAssignmentResult = await _testController.AddAssignmentAsync(addAssignmentRequest);

            if (addAssignmentResult.ValidationMessages.Count() > 0)
            {
                SharedConsoleFlows.PrintValidationMessages(addAssignmentResult.ValidationMessages);
                await AddAssignmentFlowAsync(testId);
            } else
            {
                await AddQuestionsFlowAsync(testId, addAssignmentResult.AssignmentId.Value);
            }
        }

        private async Task AddQuestionsFlowAsync(Guid testId, Guid assignmentId)
        {
            await AddQuestionFlowAsync(testId, assignmentId);
            bool addAnotherQuestion = SharedConsoleFlows.AskForBool("Add another question?");

            if (addAnotherQuestion)
            {
                await AddQuestionsFlowAsync(testId, assignmentId);
            }
        }

        private async Task AddQuestionFlowAsync(Guid testId, Guid assignmentId)
        {
            IEnumerable<string> names = CollectQuestionNames();
            var maximalScore = SharedConsoleFlows.AskForByte("What is the maximal score of the question?");
            var model = new AddQuestionRequest
            {
                TestId = testId,
                AssignmentId = assignmentId,
                Names = names,
                MaximalScore = maximalScore
            };
            AddQuestionResult addQuestionResponse = await _testController.AddQuestionAsync(model);

            if (addQuestionResponse.ValidationMessages.Count() > 0)
            {
                SharedConsoleFlows.PrintValidationMessages(addQuestionResponse.ValidationMessages);
                await AddQuestionFlowAsync(testId, assignmentId);
            }
        }

        private IEnumerable<string> CollectQuestionNames()
        {
            var names = new List<string>();
            var name = SharedConsoleFlows.AskForString("Type a name for the question");
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
