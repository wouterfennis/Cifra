using System;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Test
{
    internal class TestMenuFlow : IFlow
    {
        private readonly CreateTestFlow _createTestFlow;

        public TestMenuFlow(CreateTestFlow createTestFlow)
        {
            _createTestFlow = createTestFlow;
        }

        public async Task StartAsync()
        {
            Console.Clear();
            Console.WriteLine("What would you like to do? Type the number");
            Console.WriteLine($"[{(int)TestMenuOption.CreateTest}] - Create a new test");
            Console.WriteLine($"[{(int)TestMenuOption.GoBack}] - Go back");
            var option = Console.ReadLine();
            await RedirectToTestOption(option);
        }

        private async Task RedirectToTestOption(string option)
        {
            if (!int.TryParse(option, out int result))
            {
                await RetryMenu();
                return;
            }
            switch (result)
            {
                case (int)TestMenuOption.CreateTest:
                    await _createTestFlow.StartAsync();
                    await RetryMenu();
                    break;
                case (int)TestMenuOption.GoBack:
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
