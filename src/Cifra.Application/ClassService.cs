using System.Collections.Generic;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Commands;
using Cifra.Application.Models.Results;
using Cifra.Domain;

namespace Cifra.Application
{
    /// <summary>
    /// Application Service for the Class entity
    /// </summary>
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;


        /// <summary>
        /// Ctor
        /// </summary>
        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        /// <summary>
        /// Creates a class
        /// </summary>
        public async Task<CreateClassResult> CreateClassAsync(CreateClassCommand model)
        {
            var newClassResult = Class.TryCreate(model.Name);

            if (newClassResult.IsSuccess!)
            {
                return new CreateClassResult(newClassResult.ValidationMessage);
            }

            uint id = await _classRepository.CreateAsync(newClassResult.Value);

            return new CreateClassResult(id);
        }

        /// <summary>
        /// Retrieves classes currently available
        /// </summary>
        public async Task<GetAllClassesResult> GetClassesAsync()
        {
            List<Class> classes = await _classRepository.GetAllAsync();
            return new GetAllClassesResult(classes);
        }

        /// <summary>
        /// Retrieve a specific class.
        /// </summary>
        public async Task<GetClassResult> GetClassAsync(uint id)
        {
            Class retrievedClass = await _classRepository.GetAsync(id);
            return new GetClassResult(retrievedClass);
        }

        /// <inheritdoc/>
        public async Task<UpdateClassResult> UpdateClassAsync(UpdateClassCommand model)
        {
            var updatedClassResult = Class.TryCreate(model.Class.Name);

            if (updatedClassResult.IsSuccess!)
            {
                return new UpdateClassResult(updatedClassResult.ValidationMessage);
            }

            uint id = await _classRepository.UpdateAsync(updatedClassResult.Value);

            return new UpdateClassResult(id);
        }
    }
}
