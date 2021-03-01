using System;
using System.Collections.Generic;
using System.Linq;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.FileEntity;

namespace Cifra.FileSystem.Mapping
{
    /// <summary>
    /// Mapping for <see cref="Application.Models.Test.Test"/> to other types.
    /// </summary>
    internal static class TestMapping
    {
        /// <summary>
        /// Maps <see cref="Application.Models.Test.Test"/> to <see cref="Test"/>.
        /// </summary>
        public static FileEntity.Test MapToFileEntity(this Application.Models.Test.Test input)
        {
            ValidateNullInput(input);

            return new FileEntity.Test
            {
                Id = input.Id,
                Name = input.Name.Value,
                MinimumGrade = input.MinimumGrade.Value,
                StandardizationFactor = input.StandardizationFactor.Value,
                Assignments = input.Assignments.MapToFileEntity()
            };
        }

        /// <summary>
        /// Maps a list of <see cref="Application.Models.Test.Assignment"/> to a list of <see cref="Assignment"/>.
        /// </summary>
        public static IEnumerable<FileEntity.Assignment> MapToFileEntity(this IEnumerable<Application.Models.Test.Assignment> input)
        {
            ValidateNullInput(input);

            return input.Select(x => new FileEntity.Assignment
            {
                Id = x.Id,
                NumberOfQuestions = x.NumberOfQuestions
            });
        }

        /// <summary>
        /// Maps a list of <see cref="Application.Models.Test.Test"/> to a list of <see cref="Test"/>.
        /// </summary>
        public static Application.Models.Test.Test MapToModel(this FileEntity.Test input)
        {
            ValidateNullInput(input);

            return new Application.Models.Test.Test(input.Id, Name.CreateFromString(input.Name), StandardizationFactor.CreateFromByte(input.StandardizationFactor), Grade.CreateFromByte(input.MinimumGrade), input.Assignments.MapToModel());
        }

        /// <summary>
        /// Maps a list of <see cref="Application.Models.Test.Assignment"/> to a list of <see cref="Assignment"/>.
        /// </summary>
        public static List<Application.Models.Test.Assignment> MapToModel(this IEnumerable<FileEntity.Assignment> input)
        {
            ValidateNullInput(input);
            return input.Select(x => new Application.Models.Test.Assignment(
                x.Id,
                x.NumberOfQuestions))
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
