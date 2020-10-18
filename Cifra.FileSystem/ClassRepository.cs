using Cifra.Application.Interfaces;
using Cifra.Application.Validation;
using Cifra.FileSystem.FileEntity;
using Cifra.FileSystem.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.FileSystem
{
    public class ClassRepository : IClassRepository
    {
        private IFileInfoWrapper _classRepositoryLocation;

        public ClassRepository(IFileLocationProvider fileLocationProvider)
        {
            _classRepositoryLocation = fileLocationProvider.GetClassRepositoryLocation();
        }

        public async Task CreateAsync(Application.Models.Class.Class newClass)
        {
            var classEntity = newClass.MapToFileEntity();
            List<Class> classes = await RetrieveOrCreateClassesAsync();
            classes.Add(classEntity);
            await SaveChangesAsync(classes);
        }

        private async Task<List<Class>> RetrieveOrCreateClassesAsync()
        {
            var location = _classRepositoryLocation.ToFileInfo();
            List<Class> classes = null;
            if (!location.Exists)
            {
                classes = new List<Class>();
            } else
            {
                using (var reader = new StreamReader(location.OpenRead()))
                {
                    classes = JsonConvert.DeserializeObject<List<Class>>(await reader.ReadToEndAsync());
                }
            }
            return classes;
        }

        public async Task<Application.Models.Class.Class> GetAsync(Guid id)
        {
            var classes = await RetrieveOrCreateClassesAsync();
            var classEntity = classes.SingleOrDefault(x => x.Id == id);
            return classEntity.MapToModel();
        }

        public async Task<ValidationMessage> UpdateAsync(Application.Models.Class.Class @class)
        {
            var classes = await RetrieveOrCreateClassesAsync();
            var index = classes.FindIndex(x => x.Id == @class.Id);

            if(index == -1)
            {
                return new ValidationMessage(nameof(@class), "The class was not found");
            }
            var classEntity = @class.MapToFileEntity();
            classes[index] = classEntity;
            await SaveChangesAsync(classes);
            
            return null;
        }

        private async Task SaveChangesAsync(IEnumerable<Class> classes)
        {
            var location = _classRepositoryLocation.ToFileInfo();
            using (var writer = new StreamWriter(location.OpenWrite()))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(classes));
            }
        }
    }
}
