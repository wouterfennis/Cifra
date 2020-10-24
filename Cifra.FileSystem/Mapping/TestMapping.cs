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
                Questions = input.Questions.MapToFileEntity()
            };
        }

        public static IEnumerable<FileEntity.Question> MapToFileEntity(this IEnumerable<Application.Models.Test.Question> input)
        {
            ValidateNullInput(input);

            return input.Select(x => new FileEntity.Question
            {
                MaximalScore = x.MaximalScore.Value,
                QuestionNames = x.QuestionNames.Select(n => n.Value)
            });
        }

        public static Application.Models.Test.Test MapToModel(this FileEntity.Test input)
        {
            ValidateNullInput(input);

            return new Application.Models.Test.Test(input.Id, Name.CreateFromString(input.Name), StandardizationFactor.CreateFromByte(input.StandardizationFactor), Grade.CreateFromByte(input.MinimumGrade), input.Questions.MapToModel());
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
