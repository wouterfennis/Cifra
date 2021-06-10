using Cifra.Application.Models.Class;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Styling;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the names of the students.
    /// </summary>
    internal class StudentNamesBlock
    {
        private readonly int _lastQuestionRow;

        public Point StartPoint { get; }
        public IEnumerable<Student> Students { get; }
        public int MostOuterColumn { get; private set; }


        public StudentNamesBlock(Point startPoint, IEnumerable<Student> students, int lastQuestionRow)
        {
            StartPoint = startPoint;
            Students = students;
            _lastQuestionRow = lastQuestionRow;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPoint;
            IOrderedEnumerable<Student> orderedStudents = Students
                .OrderBy(x => x.LastName.Value);

            foreach (Student student in orderedStudents)
            {
                var nameBuilder = new StringBuilder(student.FirstName.Value);
                if (student.Infix != null)
                {
                    nameBuilder.Append(' ');
                    nameBuilder.Append(student.Infix);
                }
                nameBuilder.Append(' ');
                nameBuilder.Append(student.LastName.Value);

                spreadsheetWriter
                    .SetTextRotation(40)
                    .SetBorder(BorderStyle.Thin, BorderDirection.Bottom, Color.Black)
                    .SetHorizontalAlignment(HorizontalAlignment.Center)
                    .Write(nameBuilder.ToString())
                    .ResetStyling();

                for (int i = 0; i < _lastQuestionRow; i++)
                {
                    spreadsheetWriter
                        .MoveDown()
                        .SetHorizontalAlignment(HorizontalAlignment.Center)
                        .Write(string.Empty);
                }

                spreadsheetWriter.CurrentPosition = new Point(spreadsheetWriter.CurrentPosition.X + 1, StartPoint.Y);
            }
            MostOuterColumn = spreadsheetWriter.CurrentPosition.X;
        }
    }
}
