using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Magister;
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

namespace Cifra.FileSystem.FileReaders
{
    /// <inheritdoc/>
    internal class MagisterFileReader : IMagisterFileReader
    {
        /// <inheritdoc/>
        public MagisterClass ReadClass(Application.Models.ValueTypes.Path fileLocation)
        {
            var fileInfo = new FileInfoWrapper(fileLocation);
            using (var reader = new StreamReader(fileInfo.OpenRead()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var magisterRecords = csv
                    .GetRecords<MagisterRecord>()
                    .ToList();
                var students = magisterRecords.MapToMagisterStudents();
                // TODO: validate magisterRecords

                return new MagisterClass
                {
                    Name = magisterRecords.First().Klas,
                    Students = students,
                };
            }
        }
    }
}