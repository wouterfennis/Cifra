using Cifra.Application;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.ConsoleHost.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Class
{
    internal class CreateClassManuallyFlow : IFlow
    {
        private readonly ClassController _classController;

        public CreateClassManuallyFlow(ClassController classController)
        {
            _classController = classController;
        }

        public async Task StartAsync()
        {
            var classId = await CreateClassFlowAsync();
            Console.WriteLine("Adding students to the class");
            await AddStudentsFlowAsync(classId);
        }

        private async Task<Guid> CreateClassFlowAsync()
        {
            Console.WriteLine("What is the name of the class?");
            var className = Console.ReadLine();
            var createClassRequest = new CreateClassRequest()
            {
                Name = className
            };
            var createClassResponse = await _classController.CreateClassAsync(createClassRequest);
            var classId = createClassResponse.ClassId;
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
            var firstName = SharedConsoleFlows.AskForString("What is the first name of the student?");
            var infix = SharedConsoleFlows.AskForString("What is the infix of the student?");
            var lastName = SharedConsoleFlows.AskForString("What is the last name of the student?");
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
