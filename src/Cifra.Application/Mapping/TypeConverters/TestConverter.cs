using AutoMapper;
using Cifra.Core.Models.Test;
using Cifra.Core.Models.ValueTypes;
using System.Collections.Generic;

namespace Cifra.Application.Mapping.TypeConverters
{
    internal class TestConverter : ITypeConverter<Database.Schema.Test, Test>
    {
        public Test Convert(Database.Schema.Test source, Test destination, ResolutionContext context)
        {
            Name name = context.Mapper.Map<Name>(source.Name);
            StandardizationFactor standardizationFactor = context.Mapper.Map<StandardizationFactor>(source.StandardizationFactor);
            Grade minimumGrade = context.Mapper.Map<Grade>(source.MinimumGrade);
            List<Assignment> assignments = context.Mapper.Map<List<Assignment>>(source.Assignments);
            return new Test(source.Id, name, standardizationFactor, minimumGrade, assignments, source.NumberOfVersions);
        }
    }
}
