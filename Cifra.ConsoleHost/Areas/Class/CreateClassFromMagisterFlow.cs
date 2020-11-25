using Cifra.Application;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.ConsoleHost.Areas;
using Cifra.FileSystem;
using Cifra.FileSystem.FileReaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Class
{
    internal class CreateClassFromMagisterFlow : IFlow
    {
        private readonly ClassController _classController;
        private readonly IDirectoryInfoWrapper _magisterDirectoryLocation;

        public CreateClassFromMagisterFlow(ClassController classController,
            IFileLocationProvider fileLocationProvider)
        {
            _classController = classController;
            _magisterDirectoryLocation = fileLocationProvider.GetMagisterDirectoryPath();
        }

        public async Task StartAsync()
        {
            IFileInfoWrapper[] files = _magisterDirectoryLocation.GetFiles();
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
            _classController.CreateMagisterClassAsync(request);
        }
    }
}
