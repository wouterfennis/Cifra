using Cifra.Application.Interfaces;
using Cifra.Application.Models;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Application
{
    public class ClassController
    {
        private readonly IClassRepository _classRepository;
        private readonly IValidator<CreateClassRequest> _classValidator;
        private readonly IValidator<AddStudentRequest> _studentValidator;

        public ClassController(IClassRepository classRepository,
            IValidator<CreateClassRequest> classValidator,
            IValidator<AddStudentRequest> studentValidator)
        {
            _classRepository = classRepository;
            _classValidator = classValidator;
            _studentValidator = studentValidator;
        }

        public CreateClassResult CreateClass(CreateClassRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _classValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new CreateClassResult(validationMessages);
            }

            var @class = new Class(Name.CreateFromString(model.Name));
            Guid classId = _classRepository.Create(@class);

            return new CreateClassResult(classId);
        }

        public AddStudentResult AddStudent(AddStudentRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _studentValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new AddStudentResult(validationMessages);
            }

            var @class = _classRepository.Get(model.ClassId);
            if (@class == null)
            {
                return new AddStudentResult(new ValidationMessage(nameof(model.ClassId), "No class was found"));
            }

            var student = new Student(Name.CreateFromString(model.FullName));

            @class.AddStudent(student);
            ValidationMessage result = _classRepository.Update(@class);

            if (result != null)
            {
                return new AddStudentResult(result);
            }

            return new AddStudentResult();
        }
    }
}
