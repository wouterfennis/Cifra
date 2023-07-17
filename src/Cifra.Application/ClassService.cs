using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Cifra.Domain.Validation;
using Cifra.Application.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;

namespace Cifra.Application
{
    /// <summary>
    /// Application Service for the Class entity
    /// </summary>
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IValidator<UpdateClassCommand> _updateClassValidator;
        private readonly IValidator<CreateClassCommand> _classValidator;


        /// <summary>
        /// Ctor
        /// </summary>
        public ClassService(IClassRepository classRepository,
            IValidator<CreateClassCommand> classValidator,
            IValidator<UpdateClassCommand> updateClassValidator)
        {
            _classRepository = classRepository;
            _classValidator = classValidator;
            _updateClassValidator = updateClassValidator;
        }

        /// <summary>
        /// Creates a class
        /// </summary>
        public async Task<CreateClassResult> CreateClassAsync(CreateClassCommand model)
        {
            IEnumerable<ValidationMessage> validationMessages = _classValidator.ValidateRules(model);
            if (validationMessages.Any())
            {
                return new CreateClassResult(validationMessages);
            }

            var newClass = new Class(Name.CreateFromString(model.Name));
            int id = await _classRepository.CreateAsync(newClass);

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
        public async Task<GetClassResult> GetClassAsync(int id)
        {
            Class retrievedClass = await _classRepository.GetAsync(id);
            return new GetClassResult(retrievedClass);
        }

        /// <inheritdoc/>
        public async Task<UpdateClassResult> UpdateClassAsync(UpdateClassCommand model)
        {
            IEnumerable<ValidationMessage> validationMessages = _updateClassValidator.ValidateRules(model);
            if (validationMessages.Any())
            {
                return new UpdateClassResult(validationMessages);
            }

            int id = await _classRepository.UpdateAsync(model.UpdatedClass);

            return new UpdateClassResult(id);
        }
    }
}
