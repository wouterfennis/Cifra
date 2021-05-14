using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem;
using Cifra.FileSystem.FileSystemInfo;
using System;
using System.Linq;
using System.Threading.Tasks;

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

            if (files.Any())
            {
                PrintAvailableFiles(files);
                await CreateClassFromFile(files);
            } else
            {
                Console.WriteLine("No magister files present. Add a file into the folder:");
                Console.WriteLine(_magisterDirectoryLocation.Value);
                SharedConsoleFlows.AskForAnyKey("Press any key to go back.");
            }
        }

        private void PrintAvailableFiles(IFileInfoWrapper[] files)
        {
            Console.WriteLine("Files currently available in magister directory:");
            Console.WriteLine(_magisterDirectoryLocation.Value);
            int index = 1;
            foreach (IFileInfoWrapper file in files)
            {
                Console.WriteLine($"[{index}] - {file.Name}");
            }
        }

        private async Task CreateClassFromFile(IFileInfoWrapper[] files)
        {
            byte chosenIndex = SharedConsoleFlows.AskForByte("What file should be used?");
            IFileInfoWrapper chosenFile = await GetFile(files, chosenIndex);
            var request = new CreateMagisterClassRequest
            {
                MagisterFileLocation = chosenFile.FullName
            };
            CreateMagisterClassResult createMagisterClassResponse = await _classController.CreateMagisterClassAsync(request);
            if (createMagisterClassResponse.ValidationMessages.Any())
            {
                SharedConsoleFlows.PrintValidationMessages(createMagisterClassResponse.ValidationMessages);
                await StartAsync();
            }
        }

        private async Task<IFileInfoWrapper> GetFile(IFileInfoWrapper[] files, byte chosenIndex)
        {
            var chosenFile = files.ElementAtOrDefault(chosenIndex - 1);
            if (chosenFile == null)
            {
                Console.WriteLine("Invalid choice!");
                await StartAsync();
            }
            return chosenFile;
        }
    }
}
