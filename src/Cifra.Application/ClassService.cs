using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Cifra.Domain.Validation;
using Cifra.Application.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IValidator<CreateClassCommand> _classValidator;
        private readonly IValidator<AddStudentCommand> _studentValidator;


        /// <summary>
        /// Ctor
        /// </summary>
        public ClassService(IClassRepository classRepository,
            IValidator<CreateClassCommand> classValidator,
            IValidator<AddStudentCommand> studentValidator)
        {
            _classRepository = classRepository;
            _classValidator = classValidator;
            _studentValidator = studentValidator;
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

        /// <summary>
        /// Adds a students to class
        /// </summary>
        public async Task<AddStudentResult> AddStudentAsync(AddStudentCommand model)
        {
            IEnumerable<ValidationMessage> validationMessages = _studentValidator.ValidateRules(model);
            if (validationMessages.Any())
            {
                return new AddStudentResult(validationMessages);
            }

            Class existingClass = await _classRepository.GetAsync(model.ClassId);

            if (existingClass == null)
            {
                return new AddStudentResult(new ValidationMessage(nameof(model.ClassId), "No class was found"));
            }

            var student = new Student(Name.CreateFromString(model.FirstName), model.Infix, Name.CreateFromString(model.LastName));

            existingClass.Students.Add(student);
            await _classRepository.UpdateAsync(existingClass);

            return new AddStudentResult();
        }
    }
}
