using Cifra.Application.Models.Class.Magister;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.FileEntity;
using Cifra.FileSystem.FileEntity.Csv;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.FileSystem.Mapping
{
    /// <summary>
    /// Mapping for <see cref="Application.Models.Class.Class"/> to other types.
    /// </summary>
    internal static class ClassMapping
    {
        /// <summary>
        /// Maps <see cref="Application.Models.Class.Class"/> to <see cref="Class"/>.
        /// </summary>
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

        /// <summary>
        /// Maps a list of <see cref="Application.Models.Class.Student"/> to a list of <see cref="Student"/>.
        /// </summary>
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

        /// <summary>
        /// Maps <see cref="Class"/> to <see cref="Application.Models.Class.Class"/>.
        /// </summary>
        public static Application.Models.Class.Class MapToModel(this Class input)
        {
            ValidateNullInput(input);

            return new Application.Models.Class.Class(input.Id, Name.CreateFromString(input.Name), input.Students.MapToModel());
        }

        /// <summary>
        /// Maps a list of <see cref="Application.Models.Class.Student"/> to a list of <see cref="Student"/>.
        /// </summary>
        public static List<Application.Models.Class.Student> MapToModel(this IEnumerable<Student> input)
        {
            ValidateNullInput(input);
            return input.Select(x => new Application.Models.Class.Student(
                Name.CreateFromString(x.FirstName),
                x.Infix,
                Name.CreateFromString(x.LastName)))
                .ToList();
        }

        /// <summary>
        /// Maps a list of <see cref="MagisterRecord"/> to a list of <see cref="MagisterStudent"/>.
        /// </summary>
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
