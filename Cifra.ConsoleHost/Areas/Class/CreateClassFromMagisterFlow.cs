using System;
using System.Linq;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem;
using Cifra.FileSystem.FileSystemInfo;

namespace Cifra.ConsoleHost.Areas.Class
{
    internal class CreateClassFromMagisterFlow : IFlow
    {
        private readonly IClassService _classController;
        private readonly IDirectoryInfoWrapperFactory _directoryInfoWrapperFactory;
        private readonly Path _magisterDirectoryLocation;

        public CreateClassFromMagisterFlow(IClassService classController,
            IFileLocationProvider fileLocationProvider,
            IDirectoryInfoWrapperFactory directoryInfoWrapperFactory)
        {
            _classController = classController;
            _directoryInfoWrapperFactory = directoryInfoWrapperFactory;
            _magisterDirectoryLocation = fileLocationProvider.GetMagisterDirectoryPath();
        }

        public async Task StartAsync()
        {
            Console.Clear();
            IDirectoryInfoWrapper directory = _directoryInfoWrapperFactory.Create(_magisterDirectoryLocation);
            IFileInfoWrapper[] files = directory.GetFiles();
            Console.WriteLine("Files currently available in magister directory:");
            Console.WriteLine(_magisterDirectoryLocation.Value);
            int index = 1;
            foreach (IFileInfoWrapper file in files)
            {
                Console.WriteLine($"[{index}] - {file.Name}");
            }
            var chosenIndex = SharedConsoleFlows.AskForByte("What file should be used?");
            var chosenFile = (IFileInfoWrapper)files.GetValue(chosenIndex - 1);

            if (chosenFile == null)
            {
                Console.WriteLine("Invalid choice!");
                await StartAsync();
            }
            var request = new CreateMagisterClassRequest
            {
                MagisterFileLocation = chosenFile.FullName
            };
            CreateMagisterClassResult createMagisterClassResponse = await _classController.CreateMagisterClassAsync(request);
            if (createMagisterClassResponse.ValidationMessages.Count() > 0)
            {
                SharedConsoleFlows.PrintValidationMessages(createMagisterClassResponse.ValidationMessages);
                await StartAsync();
            }
        }
    }
}
