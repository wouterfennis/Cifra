using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.FileEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.FileSystem.Mapping
{
    internal static class TestMapping
    {
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

        public static IEnumerable<FileEntity.Assignment> MapToFileEntity(this IEnumerable<Application.Models.Test.Assignment> input)
        {
            ValidateNullInput(input);

            return input.Select(x => new FileEntity.Assignment
            {
                Id = x.Id,
                Questions = x.Questions.MapToFileEntity()
            });
        }

        public static IEnumerable<FileEntity.Question> MapToFileEntity(this IEnumerable<Application.Models.Test.Question> input)
        {
            ValidateNullInput(input);

            return input.Select(x => new FileEntity.Question
            {
                MaximalScore = x.MaximumScore.Value,
                QuestionNames = x.QuestionNames.Select(n => n.Value)
            });
        }

        public static Application.Models.Test.Test MapToModel(this FileEntity.Test input)
        {
            ValidateNullInput(input);

            return new Application.Models.Test.Test(input.Id, Name.CreateFromString(input.Name), StandardizationFactor.CreateFromByte(input.StandardizationFactor), Grade.CreateFromByte(input.MinimumGrade), input.Assignments.MapToModel());
        }

        public static List<Application.Models.Test.Assignment> MapToModel(this IEnumerable<FileEntity.Assignment> input)
        {
            ValidateNullInput(input);
            return input.Select(x => new Application.Models.Test.Assignment(
                x.Id,
                x.Questions.MapToModel())).ToList();
        }

        public static List<Application.Models.Test.Question> MapToModel(this IEnumerable<FileEntity.Question> input)
        {
            ValidateNullInput(input);
            return input.Select(x => new Application.Models.Test.Question(
                x.QuestionNames.Select(n => Name.CreateFromString(n)),
                QuestionScore.CreateFromByte(x.MaximalScore)))
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
