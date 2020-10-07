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

        public ClassController(IClassRepository classRepository, IValidator<CreateClassRequest> classValidator)
        {
            _classRepository = classRepository;
            _classValidator = classValidator;
        }

        public CreateClassResult CreateClass(CreateClassRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _classValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new CreateClassResult(validationMessages);
            }

            var @class = new Class(Name.CreateFromString(model.Name));
            ValidationMessage result = _classRepository.Create(@class);

            if(result != null)
            {
                return new CreateClassResult(result);
            }

            return new CreateClassResult(@class);
        }
    }
}
