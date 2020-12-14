using Cifra.Application.Models.Class;
using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the names of the students.
    /// </summary>
    internal class StudentNamesBlock
    {
        private readonly StudentNamesBlockInput input;

        public StudentNamesBlock(StudentNamesBlockInput input)
        {
            this.input = input;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = input.StartPoint;
            spreadsheetWriter
                .Write("Naam")
                .MoveRight();
            foreach (Student student in input.Students)
            {
                string infix = student.Infix ?? " ";
                spreadsheetWriter
                    .SetTextRotation(40)
                    .Write(student.FirstName.Value)
                    .Write(infix)
                    .Write(student.LastName.Value)
                    .ResetStyling();
            }
        }

        public class StudentNamesBlockInput : BlockInputBase
        {
            public IEnumerable<Student> Students { get; }

            public StudentNamesBlockInput(Point startPoint,
                IEnumerable<Student> students) : base(startPoint)
            {
                Students = students;
            }
        }
    }
}
