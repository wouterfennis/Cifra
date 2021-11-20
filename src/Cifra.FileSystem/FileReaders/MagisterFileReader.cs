﻿using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Magister;
using Cifra.FileSystem.FileEntity.Csv;
using Cifra.FileSystem.FileSystemInfo;
using Cifra.FileSystem.Mapping;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Cifra.FileSystem.FileReaders
{
    /// <inheritdoc/>
    public class MagisterFileReader : IMagisterFileReader
    {
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;

        public MagisterFileReader(IFileInfoWrapperFactory fileInfoWrapperFactory)
        {
            _fileInfoWrapperFactory = fileInfoWrapperFactory;
        }

        /// <inheritdoc/>
        public MagisterClass ReadClass(Application.Models.ValueTypes.Path fileLocation)
        {
            IFileInfoWrapper fileInfo = _fileInfoWrapperFactory.Create(fileLocation);
            using (var reader = new StreamReader(fileInfo.OpenRead()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var magisterRecords = csv
                    .GetRecords<MagisterRecord>()
                    .ToList();
                //var students = magisterRecords.MapToMagisterStudents();

                return new MagisterClass
                {
                    Name = magisterRecords.First().Klas,
                    Students = null, //TODO FIX
                };
            }
        }
    }
}