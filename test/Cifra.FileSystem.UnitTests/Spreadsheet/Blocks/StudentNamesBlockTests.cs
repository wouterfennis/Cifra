using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AutoFixture;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class StudentNamesBlockTest
    {
        private string[,] _spreadsheet;
        private Point _startpoint;
        private Fixture _fixture;
        private ArrayContentSpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[5, 5];
            _startpoint = new Point(0, 0);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArrayContentSpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithStudents_PutsStudentNamesInRow()
        {
            // Arrange
            var students = _fixture.CreateMany<Student>()
                .OrderBy(x => x.LastName.Value);
            var sut = new StudentNamesBlock(_startpoint, students, 2);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            for (int i = 0; i < students.Count(); i++)
            {
                var expectedStudent = students.ElementAt(i);
                var expectedName = $"{expectedStudent.FirstName.Value} {expectedStudent.Infix} {expectedStudent.LastName.Value}";
                _spreadsheet[i, 0].Should().Be(expectedName);
            }
        }

        [TestMethod]
        public void Write_WithStudents_SortStudentNamesOnLastName()
        {
            // Arrange
            var students = new List<Student> {
                new Student(Name.CreateFromString("-"), null, Name.CreateFromString("Z")),
                new Student(Name.CreateFromString("-"), null, Name.CreateFromString("A"))
            };
            var sut = new StudentNamesBlock(_startpoint, students, 2);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 0].Should().Contain("A");
        }
    }
}
