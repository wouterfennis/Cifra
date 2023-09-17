using System.Collections.Generic;
using System.Linq;

namespace Cifra.Database.Mapping
{
    internal static class SchemaMapper
    {
        public static List<Domain.Class> MapToDomain(this IEnumerable<Schema.Class> schemaClasses)
        {
            return schemaClasses.Select(c => c.MapToDomain()).ToList();
        }

        public static Domain.Class MapToDomain(this Schema.Class schemaClass)
        {
            List<Domain.Student> students = schemaClass.Students.MapToDomain();
            return new Domain.Class(schemaClass.Id, schemaClass.Name, students);
        }

        public static List<Domain.Student> MapToDomain(this IEnumerable<Schema.Student> schemaStudents)
        {
            return schemaStudents.Select(s => new Domain.Student(s.Id, s.FirstName, s.Infix, s.LastName)).ToList();
        }

        public static List<Domain.Test> MapToDomain(this IEnumerable<Schema.Test> schemaTests)
        {
            return schemaTests.Select(c => c.MapToDomain()).ToList();
        }

        public static Domain.Test MapToDomain(this Schema.Test schemaTest)
        {
            List<Domain.Assignment> assignments = schemaTest.Assignments.MapToDomain();
            return new Domain.Test(schemaTest.Id, schemaTest.Name, schemaTest.StandardizationFactor, schemaTest.MinimumGrade, assignments, schemaTest.NumberOfVersions);
        }

        public static List<Domain.Assignment> MapToDomain(this IEnumerable<Schema.Assignment> schemaAssignments)
        {
            return schemaAssignments.Select(a => new Domain.Assignment(a.Id, a.NumberOfQuestions)).ToList();
        }
    }
}
