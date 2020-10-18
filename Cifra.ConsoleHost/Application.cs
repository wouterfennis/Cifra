﻿using Cifra.Application;
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
                AddStudentsFlow(classId);
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
            if (createClassResponse.ValidationMessages.Count() > 0)
            {
                PrintValidationMessages(createClassResponse.ValidationMessages);
                CreateClassFlowAsync();
            }
            return createClassResponse.ClassId;
        }

        private void AddStudentsFlow(Guid classId)
        {
            Console.WriteLine("Adding students to the class");

            AddStudentFlowAsync(classId);
            Console.Write("Add another student?");
            bool addAnotherStudent = AskForBool();

            if (addAnotherStudent)
            {
                AddStudentFlowAsync(classId);
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
                AddStudentFlowAsync(classId);
            }
        }

        private bool AskForBool()
        {
            const char Yes = 'Y';
            const char No = 'N';
            Console.WriteLine($"Type {Yes} or {No}");
            ConsoleKeyInfo choice = Console.ReadKey();
            if (char.ToUpperInvariant(choice.KeyChar) == 'Y')
            {
                return true;
            } else if (char.ToUpperInvariant(choice.KeyChar) == 'N')
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
                Console.WriteLine("{validationMessage.Field} {validationMessage.Message}");
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