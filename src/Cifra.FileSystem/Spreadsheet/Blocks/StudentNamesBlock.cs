using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cifra.Application.Models.Class;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Styling;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the names of the students.
    /// </summary>
    internal class StudentNamesBlock
    {
        public Point StartPoint { get; }
        public IEnumerable<Student> Students { get; }
        public int MostOuterColumn { get; private set; }

        public StudentNamesBlock(Point startPoint, IEnumerable<Student> students)
        {
            StartPoint = startPoint;
            Students = students;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPoint;
            IOrderedEnumerable<Student> orderedStudents = Students
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
                    .SetBorder(BorderStyle.Thin, BorderDirection.Bottom, Color.Black)
                    .Write(nameBuilder.ToString())
                    .ResetStyling()
                    .MoveRight();
            }
            MostOuterColumn = spreadsheetWriter.CurrentPosition.X;
        }
    }
}
