using Cifra.Application.Models.Test;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Styling;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the questions.
    /// </summary>
    internal class AssignmentsBlock
    {
        public Point StartPoint { get; private set; }

        public int NumberOfVersions { get; }

        public IEnumerable<Assignment> TotalAssignments { get; }

        public Point ScoresHeaderPosition { get; private set; }

        public int LastQuestionRow { get; private set; }

        public List<int> AssignmentBottomRows { get; }

        public AssignmentsBlock(Point startPoint,
            IEnumerable<Assignment> assignments,
            int numberOfVersions)
        {
            StartPoint = startPoint;
            TotalAssignments = assignments;
            NumberOfVersions = numberOfVersions;
            AssignmentBottomRows = new List<int>();
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPoint;
            var questionNamesCollumns = NumberOfVersions;

            spreadsheetWriter
                .SetBorder(BorderStyle.Thin, BorderDirection.Bottom, Color.Black)
                .SetHorizontalAlignment(HorizontalAlignment.Center)
                .SetFontBold(true)
                .Write("Opgave");

            for (int i = 0; i < questionNamesCollumns; i++)
            {
                spreadsheetWriter.MoveRight();
                spreadsheetWriter.Write(string.Empty);
            }
            spreadsheetWriter
                .Write("Punten")
                .ResetStyling();
            ScoresHeaderPosition = spreadsheetWriter.CurrentPosition;

            LastQuestionRow = PrintAssignments(spreadsheetWriter);
        }

        private int PrintAssignments(ISpreadsheetWriter spreadsheetWriter)
        {
            int lastQuestionRow = 0;
            var totalAssignments = TotalAssignments.Count();
            for (int assignmentIndex = 0; assignmentIndex < totalAssignments; assignmentIndex++)
            {
                Assignment assignment = TotalAssignments.ElementAt(assignmentIndex);

                spreadsheetWriter.MoveDownTimes(assignment.NumberOfQuestions);
                AssignmentBottomRows.Add(spreadsheetWriter.CurrentPosition.Y);

                bool isLastAssignmentReached = assignmentIndex == totalAssignments - 1;
                if (isLastAssignmentReached)
                {
                    lastQuestionRow = spreadsheetWriter.CurrentPosition.Y;
                }
            }

            return lastQuestionRow;
        }
    }
}
