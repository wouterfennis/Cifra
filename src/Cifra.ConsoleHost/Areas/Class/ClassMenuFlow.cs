﻿using System;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Class
{
    internal class ClassMenuFlow : IFlow
    {
        private readonly CreateClassManuallyFlow _createClassManuallyFlow;
        private readonly CreateClassFromMagisterFlow _createClassFromMagisterFlow;

        public ClassMenuFlow(CreateClassManuallyFlow createClassManuallyFlow,
            CreateClassFromMagisterFlow createClassFromMagisterFlow)
        {
            _createClassManuallyFlow = createClassManuallyFlow;
            _createClassFromMagisterFlow = createClassFromMagisterFlow;
        }

        public async Task StartAsync()
        {
            Console.Clear();
            Console.WriteLine("What would you like to do? Type the number");
            Console.WriteLine($"[{(int)ClassMenuOption.CreateClassManually}] - Create a new class manually");
            Console.WriteLine($"[{(int)ClassMenuOption.CreateClassFromMagister}] - Create a new class from magister");
            Console.WriteLine($"[{(int)ClassMenuOption.GoBack}] - Go back");
            var option = Console.ReadLine();
            await RedirectToClassOption(option);
        }

        private async Task RedirectToClassOption(string option)
        {
            if (!int.TryParse(option, out int result))
            {
                await RetryMenu();
                return;
            }
            switch (result)
            {
                case (int)ClassMenuOption.CreateClassManually:
                    await _createClassManuallyFlow.StartAsync();
                    await RetryMenu();
                    break;
                case (int)ClassMenuOption.CreateClassFromMagister:
                    await _createClassFromMagisterFlow.StartAsync();
                    await RetryMenu();
                    break;
                case (int)ClassMenuOption.GoBack:
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
