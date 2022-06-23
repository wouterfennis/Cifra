using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Cifra.Core.Models.Validation;
using Cifra.Application.Validation;
using Cifra.Database.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cifra.Core.Models.Class;
using AutoMapper;

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
        private readonly IMapper _mapper;


        /// <summary>
        /// Ctor
        /// </summary>
        public ClassService(IClassRepository classRepository,
            IValidator<CreateClassCommand> classValidator,
            IValidator<AddStudentCommand> studentValidator,
            IMapper mapper)
        {
            _classRepository = classRepository;
            _classValidator = classValidator;
            _studentValidator = studentValidator;
            _mapper = mapper;
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

            var newClass = new Database.Schema.Class { Name = model.Name };
            int id = await _classRepository.CreateAsync(newClass);

            return new CreateClassResult(id);
        }

        /// <summary>
        /// Retrieves classes currently available
        /// </summary>
        public async Task<GetAllClassesResult> GetClassesAsync()
        {
            List<Database.Schema.Class> classes = await _classRepository.GetAllAsync();
            var mappedClasses = _mapper.Map<List<Class>>(classes);
            return new GetAllClassesResult(mappedClasses);
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

            Database.Schema.Class existingClass = await _classRepository.GetAsync(model.ClassId);

            if (existingClass == null)
            {
                return new AddStudentResult(new ValidationMessage(nameof(model.ClassId), "No class was found"));
            }

            var student = new Database.Schema.Student
            {
                FirstName = model.FirstName,
                Infix = model.Infix,
                LastName = model.LastName
            };

            existingClass.Students.Add(student);
            ValidationMessage result = await _classRepository.UpdateAsync(existingClass);

            if (result != null)
            {
                return new AddStudentResult(result);
            }

            return new AddStudentResult();
        }
    }
}
