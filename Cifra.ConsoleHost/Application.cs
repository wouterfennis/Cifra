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
            Area area = AskForArea();

            switch (area)
            {
                case Area.Class:
                    var classId = await CreateClassFlowAsync();
                    Console.WriteLine("Adding students to the class");
                    await AddStudentsFlowAsync(classId);
                    break;
                case Area.Test:
                    CreateTestFlow();
                    break;
                case Area.Spreadsheet:
                    break;
                case Area.Quit:
                    break;
                case Area.Unknown:
                default:
                    throw new ArgumentException($"Area: {area} is unknown");
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

        private Area AskForArea()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("Create/update classes, type C");
            Console.WriteLine("Create/update tests, type T");
            Console.WriteLine("Create new spreadsheet, type S");
            Console.WriteLine("To Quit, type Q");
            string input = Console.ReadLine();
            Area area = MapToArea(input);
            if (area == Area.Unknown)
            {
                Console.WriteLine("Invalid choice!");
                AskForArea();
            }
            return area;
        }

        private Area MapToArea(string functionality)
        {
            switch (functionality.ToUpperInvariant())
            {
                case "C":
                    return Area.Class;
                case "T":
                    return Area.Test;
                case "S":
                    return Area.Spreadsheet;
                case "Q":
                    return Area.Quit;
                default:
                    return Area.Unknown;
            }
        }
    }
}