using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Magister;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Validation;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Application
{
    /// <summary>
    /// Application Service for the Class entity
    /// </summary>
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IMagisterFileReader _magisterFileReader;
        private readonly IValidator<CreateClassCommand> _classValidator;
        private readonly IValidator<CreateMagisterClassCommand> _magisterClassValidator;
        private readonly IValidator<AddStudentCommand> _studentValidator;


        /// <summary>
        /// Ctor
        /// </summary>
        public ClassService(IClassRepository classRepository,
            IMagisterFileReader magisterFileReader,
            IValidator<CreateClassCommand> classValidator,
            IValidator<CreateMagisterClassCommand> magisterClassValidator,
            IValidator<AddStudentCommand> studentValidator)
        {
            _classRepository = classRepository;
            _magisterFileReader = magisterFileReader;
            _classValidator = classValidator;
            _magisterClassValidator = magisterClassValidator;
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

            var @class = new Class(Name.CreateFromString(model.Name));
            int id = await _classRepository.CreateAsync(@class);

            return new CreateClassResult(id);
        }

        /// <summary>
        /// Creates a magister class
        /// </summary>
        public async Task<CreateMagisterClassResult> CreateMagisterClassAsync(CreateMagisterClassCommand model)
        {
            IEnumerable<ValidationMessage> validationMessages = _magisterClassValidator.ValidateRules(model);
            if (validationMessages.Any())
            {
                return new CreateMagisterClassResult(validationMessages);
            }
            MagisterClass magisterClass = _magisterFileReader.ReadClass(Path.CreateFromString(model.MagisterFileLocation));
            var newClass = new Class(Name.CreateFromString(magisterClass.Name));
            foreach (var magisterStudent in magisterClass.Students)
            {
                var student = new Student(Name.CreateFromString(magisterStudent.FirstName),
                    magisterStudent.Infix,
                    Name.CreateFromString(magisterStudent.LastName));
                newClass.AddStudent(student);
            }
            int id = await _classRepository.CreateAsync(newClass);

            return new CreateMagisterClassResult(id);
        }

        /// <summary>
        /// Retrieves classes currently available
        /// </summary>
        public async Task<GetAllClassesResult> GetClassesAsync()
        {
            var classes = await _classRepository.GetAllAsync();
            return new GetAllClassesResult(classes);
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

            var existingClass = await _classRepository.GetAsync(model.ClassId);
            if (existingClass == null)
            {
                return new AddStudentResult(new ValidationMessage(nameof(model.ClassId), "No class was found"));
            }

            var student = new Student(Name.CreateFromString(model.FirstName),
                model.Infix,
                Name.CreateFromString(model.LastName));

            existingClass.AddStudent(student);
            ValidationMessage result = await _classRepository.UpdateAsync(existingClass);

            if (result != null)
            {
                return new AddStudentResult(result);
            }

            return new AddStudentResult();
        }
    }
}
