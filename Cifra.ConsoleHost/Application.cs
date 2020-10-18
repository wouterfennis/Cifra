using Cifra.Application;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost
{
    internal class Application
    {
        private readonly ClassController _classController;
        private readonly TestController _testController;

        public Application(ClassController classController, TestController testController)
        {
            _classController = classController;
            _testController = testController;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Welcome to Cifra");
            await MenuFlowAsync();
        }

        private async Task MenuFlowAsync()
        {
            Functionality functionality = AskForFunctionality();

            if (functionality == Functionality.CreateClass)
            {
                var classId = await CreateClassFlowAsync();
                Console.WriteLine("Adding students to the class");
                await AddStudentsFlowAsync(classId);
            }
            else if (functionality == Functionality.CreateTest)
            {
                CreateTestFlow();
            }
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
                PrintValidationMessages(createClassResponse.ValidationMessages);
                classId = await CreateClassFlowAsync();
            }
            return classId;
        }

        private async Task AddStudentsFlowAsync(Guid classId)
        {
            await AddStudentFlowAsync(classId);
            Console.WriteLine("Add another student?");
            bool addAnotherStudent = AskForBool();

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
                PrintValidationMessages(addStudentResponse.ValidationMessages);
                await AddStudentFlowAsync(classId);
            }
        }

        private bool AskForBool()
        {
            const string Yes = "Y";
            const string No = "N";
            Console.WriteLine($"Type {Yes} or {No}");
            string input = Console.ReadLine();
            if (input.ToUpperInvariant() == Yes)
            {
                return true;
            }
            else if (input.ToUpperInvariant() == No)
            {
                return false;
            }
            else
            {
                return AskForBool();
            }
        }

        private void PrintValidationMessages(IEnumerable<ValidationMessage> validationMessages)
        {
            foreach (ValidationMessage validationMessage in validationMessages)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Some of the input was not valid:");
                Console.WriteLine($"{validationMessage.Message} on the following field: {validationMessage.Field} ");
                Console.ResetColor();
            }
        }

        private void CreateTestFlow()
        {
            throw new NotImplementedException();
        }

        private Functionality AskForFunctionality()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("Create a new class, type C");
            Console.WriteLine("Create a new test, type T");
            Console.Write("Choice: ");
            string functionality = Console.ReadLine();
            bool isValidOption = ValidateOption(functionality);
            if (!isValidOption)
            {
                Console.WriteLine("Invalid choice!");
                AskForFunctionality();
            }
            return MapToFunctionality(functionality);
        }

        private bool ValidateOption(string functionality)
        {
            if (functionality == "C" || functionality == "T")
            {
                return true;
            }
            return false;
        }

        private Functionality MapToFunctionality(string functionality)
        {
            switch (functionality)
            {
                case "C": return Functionality.CreateClass;
                case "T": return Functionality.CreateClass;
                default:
                    throw new NotSupportedException($"Functionality: {functionality} is not supported");
            }
        }
    }
}