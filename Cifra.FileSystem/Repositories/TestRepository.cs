using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using Cifra.FileSystem.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.FileSystem.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly Application.Models.ValueTypes.Path _testRepositoryLocation;
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;

        public TestRepository(IFileLocationProvider fileLocationProvider,
            IFileInfoWrapperFactory fileInfoWrapperFactory)
        {
            _testRepositoryLocation = fileLocationProvider.GetTestRepositoryPath();
            _fileInfoWrapperFactory = fileInfoWrapperFactory;
        }

        public async Task CreateAsync(Test newTest)
        {
            FileEntity.Test testEntity = newTest.MapToFileEntity();
            List<FileEntity.Test> tests = await RetrieveOrCreateTestsAsync();
            tests.Add(testEntity);
            await SaveChangesAsync(tests);
        }

        public async Task<Test> GetAsync(Guid id)
        {
            List<FileEntity.Test> tests = await RetrieveOrCreateTestsAsync();
            FileEntity.Test testEntity = tests.SingleOrDefault(x => x.Id == id);
            return testEntity.MapToModel();
        }

        public async Task<ValidationMessage> UpdateAsync(Test test)
        {
            List<FileEntity.Test> tests = await RetrieveOrCreateTestsAsync();
            int index = tests.FindIndex(x => x.Id == test.Id);

            if (index == -1)
            {
                return new ValidationMessage(nameof(test), "The test was not found");
            }
            FileEntity.Test testEntity = test.MapToFileEntity();
            tests[index] = testEntity;
            await SaveChangesAsync(tests);

            return null;
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            List<FileEntity.Test> tests = await RetrieveOrCreateTestsAsync();
            return tests.Select(x => x.MapToModel());
        }

        private async Task<List<FileEntity.Test>> RetrieveOrCreateTestsAsync()
        {
            IFileInfoWrapper location = _fileInfoWrapperFactory.Create(_testRepositoryLocation);
            List<FileEntity.Test> tests = null;
            if (!location.Exists)
            {
                tests = new List<FileEntity.Test>();
            }
            else
            {
                using (var reader = new StreamReader(location.OpenRead()))
                {
                    tests = JsonConvert.DeserializeObject<List<FileEntity.Test>>(await reader.ReadToEndAsync());
                }
            }
            return tests;
        }

        private async Task SaveChangesAsync(IEnumerable<FileEntity.Test> tests)
        {
            IFileInfoWrapper location = _fileInfoWrapperFactory.Create(_testRepositoryLocation);
            using (var writer = new StreamWriter(location.OpenWrite()))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(tests));
            }
        }
    }
}
