using Cifra.ConsoleHost.Areas;
using Cifra.ConsoleHost.Areas.Class;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Class
{
    internal class ClassMenuFlow : IFlow
    {
        private readonly CreateClassManuallyFlow _createClassManuallyFlow;
        private readonly CreateClassFromMagisterFlow _createClassFromMagisterFlow;
        private readonly EditClassFlow _editClassFlow;
        private readonly DeleteClassFlow _deleteClassFlow;

        public ClassMenuFlow(CreateClassManuallyFlow createClassManuallyFlow, CreateClassFromMagisterFlow createClassFromMagisterFlow, EditClassFlow editClassFlow, DeleteClassFlow deleteClassFlow)
        {
            _createClassManuallyFlow = createClassManuallyFlow;
            _createClassFromMagisterFlow = createClassFromMagisterFlow;
            _editClassFlow = editClassFlow;
            _deleteClassFlow = deleteClassFlow;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("What would you like to do? Type the number");
            Console.WriteLine($"[{(int)ClassMenuOption.CreateClassManually}] - Create a new class manually");
            Console.WriteLine($"[{(int)ClassMenuOption.CreateClassFromMagister}] - Create a new class from magister");
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
                case (int)ClassMenuOption.CreateClassManually:
                    await _createClassManuallyFlow.StartAsync();
                    break;
                case (int)ClassMenuOption.CreateClassFromMagister:
                    await _createClassFromMagisterFlow.StartAsync();
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
