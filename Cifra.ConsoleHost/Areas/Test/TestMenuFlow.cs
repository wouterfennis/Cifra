using System;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Test
{
    internal class TestMenuFlow : IFlow
    {
        private readonly CreateTestFlow _createTestFlow;
        private readonly EditTestFlow _editTestFlow;
        private readonly DeleteTestFlow _deleteTestFlow;

        public TestMenuFlow(CreateTestFlow createTestFlow, EditTestFlow editTestFlow, DeleteTestFlow deleteTestFlow)
        {
            _createTestFlow = createTestFlow;
            _editTestFlow = editTestFlow;
            _deleteTestFlow = deleteTestFlow;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("What would you like to do? Type the number");
            Console.WriteLine($"[{(int)TestMenuOption.CreateTest}] - Create a new test");
            Console.WriteLine($"[{(int)TestMenuOption.EditTest}] - Edit a test");
            Console.WriteLine($"[{(int)TestMenuOption.DeleteTest}] - Delete a test");
            var option = Console.ReadLine();
            await RedirectToTestOption(option);
        }

        private async Task RedirectToTestOption(string option)
        {
            if (!byte.TryParse(option, out byte result))
            {
                await RetryMenu();
                return;
            }
            switch (result)
            {
                case (int)TestMenuOption.CreateTest:
                    await _createTestFlow.StartAsync();
                    break;
                case (int)TestMenuOption.EditTest:
                    await _editTestFlow.StartAsync();
                    break;
                case (int)TestMenuOption.DeleteTest:
                    await _deleteTestFlow.StartAsync();
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
