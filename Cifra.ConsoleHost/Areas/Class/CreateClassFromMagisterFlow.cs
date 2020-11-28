using Cifra.Application;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.ConsoleHost.Areas;
using Cifra.FileSystem;
using Cifra.FileSystem.FileReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Class
{
    internal class CreateClassFromMagisterFlow : IFlow
    {
        private readonly ClassController _classController;
        private readonly IDirectoryInfoWrapperFactory _directoryInfoWrapperFactory;
        private readonly Path _magisterDirectoryLocation;

        public CreateClassFromMagisterFlow(ClassController classController,
            IFileLocationProvider fileLocationProvider,
            IDirectoryInfoWrapperFactory directoryInfoWrapperFactory)
        {
            _classController = classController;
            _directoryInfoWrapperFactory = directoryInfoWrapperFactory;
            _magisterDirectoryLocation = fileLocationProvider.GetMagisterDirectoryPath();
        }

        public async Task StartAsync()
        {
            IDirectoryInfoWrapper directory = _directoryInfoWrapperFactory.Create(_magisterDirectoryLocation);
            IFileInfoWrapper[] files = directory.GetFiles();
            Console.WriteLine("Files currently available in magister directory:");
            Console.WriteLine(_magisterDirectoryLocation);
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
            await _classController.CreateMagisterClassAsync(request);
        }
    }
}
