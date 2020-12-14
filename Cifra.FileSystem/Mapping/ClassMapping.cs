using Cifra.Application.Models.Class.Magister;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.FileEntity;
using Cifra.FileSystem.FileEntity.Csv;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.FileSystem.Mapping
{
    internal static class ClassMapping
    {
        public static Class MapToFileEntity(this Application.Models.Class.Class input)
        {
            ValidateNullInput(input);

            return new Class
            {
                Id = input.Id,
                Name = input.Name.Value,
                Students = input.Students.MapToFileEntity()
            };
        }

        public static IEnumerable<Student> MapToFileEntity(this IEnumerable<Application.Models.Class.Student> input)
        {
            ValidateNullInput(input);

            return input.Select(x => new Student
            {
                FirstName = x.FirstName.Value,
                Infix = x.Infix,
                LastName = x.LastName.Value
            });
        }

        public static Application.Models.Class.Class MapToModel(this Class input)
        {
            ValidateNullInput(input);

            return new Application.Models.Class.Class(input.Id, Name.CreateFromString(input.Name), input.Students.MapToModel());
        }

        public static List<Application.Models.Class.Student> MapToModel(this IEnumerable<Student> input)
        {
            ValidateNullInput(input);
            return input.Select(x => new Application.Models.Class.Student(
                Name.CreateFromString(x.FirstName),
                x.Infix,
                Name.CreateFromString(x.LastName)))
                .ToList();
        }

        public static List<MagisterStudent> MapToMagisterStudents(this IEnumerable<MagisterRecord> input)
        {
            ValidateNullInput(input);
            return input.Select(x => new MagisterStudent
            {
                FirstName = x.Roepnaam,
                Infix = x.Tussenvoegsel,
                LastName = x.Achternaam
            })
            .ToList();
        }

        private static void ValidateNullInput(object input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
        }
    }
}
