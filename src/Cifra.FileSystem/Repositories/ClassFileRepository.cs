using Cifra.Application.Interfaces;
using Cifra.Application.Models.Validation;
using Cifra.Application.Validation;
using Cifra.FileSystem.FileEntity;
using Cifra.FileSystem.FileSystemInfo;
using Cifra.FileSystem.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.FileSystem.Repositories
{
    /// <inheritdoc/>
    public class ClassFileRepository : IClassRepository
    {
        private readonly IFileLocationProvider _fileLocationProvider;
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;

        public ClassFileRepository(IFileLocationProvider fileLocationProvider,
            IFileInfoWrapperFactory fileInfoWrapperFactory)
        {
            _fileLocationProvider = fileLocationProvider;
            _fileInfoWrapperFactory = fileInfoWrapperFactory;
        }

        /// <inheritdoc/>
        public async Task CreateAsync(Application.Models.Class.Class newClass)
        {
            Class classEntity = newClass.MapToFileEntity();
            List<Class> classes = await RetrieveOrCreateClassesAsync();
            classes.Add(classEntity);
            await SaveChangesAsync(classes);
        }

        /// <inheritdoc/>
        public async Task<Application.Models.Class.Class> GetAsync(Guid id)
        {
            List<Class> classes = await RetrieveOrCreateClassesAsync();
            Class classEntity = classes.SingleOrDefault(x => x.Id == id);
            return classEntity.MapToModel();
        }

        /// <inheritdoc/>
        public async Task<ValidationMessage> UpdateAsync(Application.Models.Class.Class @class)
        {
            List<Class> classes = await RetrieveOrCreateClassesAsync();
            int index = classes.FindIndex(x => x.Id == @class.Id);

            if (index == -1)
            {
                return new ValidationMessage(nameof(@class), "The class was not found");
            }
            Class classEntity = @class.MapToFileEntity();
            classes[index] = classEntity;
            await SaveChangesAsync(classes);

            return null;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Application.Models.Class.Class>> GetAllAsync()
        {
            List<Class> classes = await RetrieveOrCreateClassesAsync();
            return classes.Select(x => x.MapToModel());
        }

        private async Task<List<Class>> RetrieveOrCreateClassesAsync()
        {
            var repositoryPath = _fileLocationProvider.GetClassRepositoryPath();
            IFileInfoWrapper location = _fileInfoWrapperFactory.Create(repositoryPath);
            List<Class> classes = null;
            if (location.Exists)
            {
                using (StreamReader reader = new StreamReader(location.OpenRead()))
                {
                    classes = JsonConvert.DeserializeObject<List<Class>>(await reader.ReadToEndAsync());
                }
            }
            else
            {
                classes = new List<Class>();
            }
            return classes;
        }

        private async Task SaveChangesAsync(IEnumerable<Class> classes)
        {
            IFileInfoWrapper location = _fileInfoWrapperFactory.Create(_fileLocationProvider.GetClassRepositoryPath());
            using (var writer = new StreamWriter(location.OpenWrite()))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(classes));
            }
        }
    }
}
