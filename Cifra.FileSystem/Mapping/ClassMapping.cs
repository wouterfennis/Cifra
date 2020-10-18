using Cifra.FileSystem.FileEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cifra.FileSystem.Mapping
{
    internal static class ClassMapping
    {
        public static Class MapToFileEntity(this Application.Models.Class.Class input)
        {
            return new Class
            {
                Id = input.Id,
                Name = input.Name.Value,
                Students = input.Students.ToFileEntity()
            };
        }

        public static IEnumerable<Student> ToFileEntity(this IEnumerable<Application.Models.Class.Student> input)
        {
           return  input.Select(x => new Student { FullName = x.FullName.Value });
        }
    }
}
