using Cifra.Application.Interfaces;
using Cifra.Application.Models;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Class.Magister;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Application
{
    public class ClassController
    {
        private readonly IClassRepository _classRepository;
        private readonly IMagisterFileReader _magisterFileReader;
        private readonly IValidator<CreateClassRequest> _classValidator;
        private readonly IValidator<CreateMagisterClassRequest> _magisterClassValidator;
        private readonly IValidator<AddStudentRequest> _studentValidator;

        public ClassController(IClassRepository classRepository,
            IMagisterFileReader magisterFileReader,
            IValidator<CreateClassRequest> classValidator,
            IValidator<CreateMagisterClassRequest> magisterClassValidator,
            IValidator<AddStudentRequest> studentValidator)
        {
            _classRepository = classRepository;
            _magisterFileReader = magisterFileReader;
            _classValidator = classValidator;
            _magisterClassValidator = magisterClassValidator;
            _studentValidator = studentValidator;
        }

        public async Task<CreateClassResult> CreateClassAsync(CreateClassRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _classValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new CreateClassResult(validationMessages);
            }

            var @class = new Class(Name.CreateFromString(model.Name));
            await _classRepository.CreateAsync(@class);

            return new CreateClassResult(@class.Id);
        }

        public async Task<CreateMagisterClassResult> CreateMagisterClassAsync(CreateMagisterClassRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _magisterClassValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new CreateMagisterClassResult(validationMessages);
            }
            MagisterClass magisterClass = _magisterFileReader.ReadClass(Path.CreateFromString(model.MagisterFileLocation));
            var @class = new Class(Name.CreateFromString(magisterClass.Name));
            foreach (var magisterStudent in magisterClass.Students)
            {
                var student = new Student(Name.CreateFromString(magisterStudent.FirstName),
                    Infix.CreateFromString(magisterStudent.Infix),
                    Name.CreateFromString(magisterStudent.LastName));
                @class.AddStudent(student);
            }
            await _classRepository.CreateAsync(@class);

            return new CreateMagisterClassResult(@class.Id);
        }

        public async Task<GetAllClassesResult> GetClassesAsync()
        {
            var classes = await _classRepository.GetAllAsync();
            return new GetAllClassesResult(classes);
        }

        public async Task<AddStudentResult> AddStudentAsync(AddStudentRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _studentValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new AddStudentResult(validationMessages);
            }

            var @class = await _classRepository.GetAsync(model.ClassId);
            if (@class == null)
            {
                return new AddStudentResult(new ValidationMessage(nameof(model.ClassId), "No class was found"));
            }

            var student = new Student(Name.CreateFromString(model.FirstName),
                Infix.CreateFromString(model.Infix),
                Name.CreateFromString(model.LastName));

            @class.AddStudent(student);
            ValidationMessage result = await _classRepository.UpdateAsync(@class);

            if (result != null)
            {
                return new AddStudentResult(result);
            }

            return new AddStudentResult();
        }
    }
}
