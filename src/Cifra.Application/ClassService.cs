using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Results;
using Cifra.Commands;
using Cifra.Domain;
using Cifra.Domain.Validation;

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

            if (!newClassResult.IsSuccess)
            {
                return new CreateClassResult(newClassResult.ValidationMessage!);
            }

            uint id = await _classRepository.CreateAsync(newClassResult.Value!);

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
            Class? retrievedClass = await _classRepository.GetAsync(id);
            return new GetClassResult { RetrievedClass = retrievedClass };
        }

        /// <inheritdoc/>
        public async Task<UpdateClassResult> UpdateClassAsync(UpdateClassCommand model)
        {
            var updatedStudentsResult = TryCreateStudents(model.Class.Students);

            if(!updatedStudentsResult.IsSuccess)
            {
                return new UpdateClassResult(updatedStudentsResult.ValidationMessage!);
            }

            var updatedClassResult = Class.TryCreate(model.Class.Id, model.Class.Name, updatedStudentsResult.Value!);
            var originalClass = await _classRepository.GetAsync(model.Class.Id);

            if (!updatedClassResult.IsSuccess)
            {
                return new UpdateClassResult(updatedClassResult.ValidationMessage!);
            }

            if (originalClass is null)
            {
                return new UpdateClassResult(ValidationMessage.Create(nameof(model.Class.Id), "Class to update cannot be found"));
            }

            originalClass.UpdateFromOtherClass(updatedClassResult.Value!);

            uint id = await _classRepository.UpdateAsync(originalClass);

            return new UpdateClassResult(id);
        }

        private Result<IEnumerable<Student>> TryCreateStudents(IEnumerable<Commands.Models.Student> students)
        {
            var studentsResults = students.Select(s => Student.TryCreate(s.Id, s.FirstName, s.Infix, s.LastName));

            var failedStudents = studentsResults.Where(s => !s.IsSuccess);
            if (failedStudents.Any(s => !s.IsSuccess))
            {
                var combinedValidationMessages = failedStudents
                    .Select(s => s.ValidationMessage!.Message)
                    .Aggregate((originalString, newEntry) => $"{originalString},{Environment.NewLine} {newEntry}");

                var validationMessage = ValidationMessage.Create(nameof(students), combinedValidationMessages);
                return Result<IEnumerable<Student>>.Fail<IEnumerable<Student>>(validationMessage);
            }

            return Result<IEnumerable<Student>>.Ok<IEnumerable<Student>>(studentsResults.Select(s => s.Value!));
        }
    }
}
