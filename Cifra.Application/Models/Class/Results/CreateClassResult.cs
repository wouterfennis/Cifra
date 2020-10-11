using Cifra.Application.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Results
{
    public class CreateClassResult
    {
        public Guid ClassId { get; }

        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        public CreateClassResult(Guid classId)
        {
            ClassId = classId;
            ValidationMessages = new List<ValidationMessage>();
        }

        public CreateClassResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }
    }
}
