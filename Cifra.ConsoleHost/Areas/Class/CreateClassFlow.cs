using Cifra.Application;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.ConsoleHost.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Functionalities.Class
{
    internal class CreateClassFlow : IFlow
    {
        private readonly ClassController _classController;

        public CreateClassFlow(ClassController classController)
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
            Console.WriteLine("Add another student?");
            bool addAnotherStudent = SharedConsoleFlows.AskForBool();

            if (addAnotherStudent)
            {
                await AddStudentsFlowAsync(classId);
            }
        }

        private async Task AddStudentFlowAsync(Guid classId)
        {
            Console.WriteLine("Type the full name of the student");
            var fullName = Console.ReadLine();
            var model = new AddStudentRequest
            {
                ClassId = classId,
                FullName = fullName
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
