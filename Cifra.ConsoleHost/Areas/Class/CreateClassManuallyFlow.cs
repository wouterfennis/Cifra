using System;
using System.Linq;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;

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
            Guid classId = await CreateClassFlowAsync();
            Console.WriteLine("Adding students to the class");
            await AddStudentsFlowAsync(classId);
        }

        private async Task<Guid> CreateClassFlowAsync()
        {
            Console.WriteLine("What is the name of the class?");
            string className = Console.ReadLine();
            var createClassRequest = new CreateClassRequest()
            {
                Name = className
            };
            CreateClassResult createClassResponse = await _classController.CreateClassAsync(createClassRequest);
            Guid classId = createClassResponse.ClassId;
            if (createClassResponse.ValidationMessages.Count() > 0)
            {
                SharedConsoleFlows.PrintValidationMessages(createClassResponse.ValidationMessages);
                classId = await CreateClassFlowAsync();
            }
            return classId;
        }

        private async Task AddStudentsFlowAsync(Guid classId)
        {
            await AddStudentFlowAsync(classId);
            bool addAnotherStudent = SharedConsoleFlows.AskForBool("Add another student?");

            if (addAnotherStudent)
            {
                await AddStudentsFlowAsync(classId);
            }
        }

        private async Task AddStudentFlowAsync(Guid classId)
        {
            string firstName = SharedConsoleFlows.AskForString("What is the first name of the student?");
            string infix = SharedConsoleFlows.AskForOptionalString("What is the infix of the student?");
            string lastName = SharedConsoleFlows.AskForString("What is the last name of the student?");
            var model = new AddStudentRequest
            {
                ClassId = classId,
                FirstName = firstName,
                Infix = infix,
                LastName = lastName
            };
            AddStudentResult addStudentResponse = await _classController.AddStudentAsync(model);

            if (addStudentResponse.ValidationMessages.Count() > 0)
            {
                SharedConsoleFlows.PrintValidationMessages(addStudentResponse.ValidationMessages);
                await AddStudentFlowAsync(classId);
            }
        }
    }
}
