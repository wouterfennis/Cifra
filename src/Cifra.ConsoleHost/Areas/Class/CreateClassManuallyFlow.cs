using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Cifra.ConsoleHost.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Class
{
    internal class CreateClassManuallyFlow : IFlow
    {
        private readonly IClassService _classController;

        public CreateClassManuallyFlow(IClassService classController)
        {
            _classController = classController;
        }

        public async Task StartAsync()
        {
            Console.Clear();
            int classId = await CreateClassFlowAsync();
            Console.WriteLine("Adding students to the class");
            await AddStudentsFlowAsync(classId);
        }

        private async Task<int> CreateClassFlowAsync()
        {
            Console.WriteLine("What is the name of the class?");
            string className = Console.ReadLine();
            var createClassRequest = new CreateClassCommand()
            {
                Name = className
            };
            CreateClassResult createClassResponse = await _classController.CreateClassAsync(createClassRequest);
            int classId = createClassResponse.ClassId;
            if (createClassResponse.ValidationMessages.Any())
            {
                SharedConsoleFlows.PrintValidationMessages(createClassResponse.ValidationMessages);
                classId = await CreateClassFlowAsync();
            }
            return classId;
        }

        private async Task AddStudentsFlowAsync(int classId)
        {
            await AddStudentFlowAsync(classId);
            bool addAnotherStudent = SharedConsoleFlows.AskForBool("Add another student?");

            if (addAnotherStudent)
            {
                await AddStudentsFlowAsync(classId);
            }
        }

        private async Task AddStudentFlowAsync(int classId)
        {
            string firstName = SharedConsoleFlows.AskForString("What is the first name of the student?");
            string infix = SharedConsoleFlows.AskForOptionalString("What is the infix of the student?");
            string lastName = SharedConsoleFlows.AskForString("What is the last name of the student?");
            var model = new AddStudentCommand
            {
                ClassId = classId,
                FirstName = firstName,
                Infix = infix,
                LastName = lastName
            };
            AddStudentResult addStudentResponse = await _classController.AddStudentAsync(model);

            if (addStudentResponse.ValidationMessages.Any())
            {
                SharedConsoleFlows.PrintValidationMessages(addStudentResponse.ValidationMessages);
                await AddStudentFlowAsync(classId);
            }
        }
    }
}
