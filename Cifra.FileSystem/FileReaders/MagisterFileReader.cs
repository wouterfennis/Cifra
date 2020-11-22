using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.FileEntity;
using Cifra.FileSystem.FileEntity.Csv;
using Cifra.FileSystem.Mapping;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Cifra.FileSystem.FileReaders
{
    /// <summary>
    /// Reader for files from Magister
    /// </summary>
    internal class MagisterFileReader : IFileReader
    {
        public Class ReadClass(IFileInfoWrapper file)
        {
            using (var reader = new StreamReader(file.OpenRead()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var magisterRecords = csv.GetRecords<MagisterRecord>();
                var students = magisterRecords.MapToStudents();
                // TODO: validate magisterRecords

                return new Class
                {
                    Name = magisterRecords.First().Klas,
                    Students = students,
                };
            }
        }
    }
}