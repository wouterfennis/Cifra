using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
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
        private IFileInfoWrapper _testRepositoryLocation;

        public TestRepository(IFileLocationProvider fileLocationProvider)
        {
            _testRepositoryLocation = fileLocationProvider.GetTestRepositoryLocation();
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
            var tests = await RetrieveOrCreateTestsAsync();
            var testEntity = tests.SingleOrDefault(x => x.Id == id);
            return testEntity.MapToModel();
        }

        public async Task<ValidationMessage> UpdateAsync(Test test)
        {
            var tests = await RetrieveOrCreateTestsAsync();
            var index = tests.FindIndex(x => x.Id == test.Id);

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
            var tests = await RetrieveOrCreateTestsAsync();
            return tests.Select(x => x.MapToModel());
        }

        private async Task<List<FileEntity.Test>> RetrieveOrCreateTestsAsync()
        {
            var location = _testRepositoryLocation.ToFileInfo();
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
            var location = _testRepositoryLocation.ToFileInfo();
            using (var writer = new StreamWriter(location.OpenWrite()))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(tests));
            }
        }
    }
}
