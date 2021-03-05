using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cifra.Application.Models.Class;
using SpreadsheetWriter.Abstractions;

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
            IOrderedEnumerable<Student> orderedStudents = input.Students
                .OrderBy(x => x.LastName.Value);

            foreach (Student student in orderedStudents)
            {
                // append instead of write
                // with rich text it should be possible to mark the lastname bold
                var nameBuilder = new StringBuilder(student.FirstName.Value);
                if (student.Infix != null)
                {
                    nameBuilder.Append(" ");
                    nameBuilder.Append(student.Infix);
                }
                nameBuilder.Append(" ");
                nameBuilder.Append(student.LastName.Value);

                spreadsheetWriter
                    .SetTextRotation(40)
                    .Write(nameBuilder.ToString())
                    .ResetStyling()
                    .MoveRight();
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
