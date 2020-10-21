using Cifra.ConsoleHost.Areas;
using Cifra.ConsoleHost.Areas.Class;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Functionalities.Class
{
    internal class ClassMenuFlow : IFlow
    {
        private readonly CreateClassFlow _createClassFlow;
        private readonly EditClassFlow _editClassFlow;
        private readonly DeleteClassFlow _deleteClassFlow;

        public ClassMenuFlow(CreateClassFlow createClassFlow, EditClassFlow editClassFlow, DeleteClassFlow deleteClassFlow)
        {
            _createClassFlow = createClassFlow;
            _editClassFlow = editClassFlow;
            _deleteClassFlow = deleteClassFlow;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("What would you like to do? Type the number");
            Console.WriteLine($"[{(int)ClassMenuOption.CreateClass}] - Create a new class");
            Console.WriteLine($"[{(int)ClassMenuOption.EditClass}] - Edit a class");
            Console.WriteLine($"[{(int)ClassMenuOption.DeleteClass}] - Delete a class");
            var option = Console.ReadLine();
            await RedirectToClassOption(option);
        }

        private async Task RedirectToClassOption(string option)
        {
            if (!byte.TryParse(option, out byte result))
            {
                await RetryMenu();
                return;
            }
            switch (result)
            {
                case (int)ClassMenuOption.CreateClass:
                    await _createClassFlow.StartAsync();
                    break;
                case (int)ClassMenuOption.EditClass:
                    await _editClassFlow.StartAsync();
                    break;
                case (int)ClassMenuOption.DeleteClass:
                    await _deleteClassFlow.StartAsync();
                    break;
                default:
                    await RetryMenu();
                    break;
            }
        }

        private async Task RetryMenu()
        {
            Console.WriteLine("Invalid choice!");
            await StartAsync();
        }
    }
}
