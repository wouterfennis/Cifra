using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Cifra.Application.Models.Test;
using SpreadsheetWriter.Abstractions;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write the questions.
    /// </summary>
    internal class AssignmentsBlock
    {
        public Point StartPoint { get; private set; }

        public int NumberOfVersions { get; }

        public IEnumerable<Assignment> Assignments { get; }

        public Point ScoresHeaderPosition { get; private set; }

        public int LastQuestionRow { get; private set; }

        public List<int> AssignmentBottomRows { get; }

        public AssignmentsBlock(Point startPoint,
            IEnumerable<Assignment> assignments,
            int numberOfVersions)
        {
            StartPoint = startPoint;
            Assignments = assignments;
            NumberOfVersions = numberOfVersions;
            AssignmentBottomRows = new List<int>();
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPoint;
            var questionNamesCollumns = NumberOfVersions;

            spreadsheetWriter
                .SetFontBold(true)
                .Write("Opgave")
                .MoveRightTimes(questionNamesCollumns)
                .Write("Punten")
                .SetFontBold(false);
            ScoresHeaderPosition = spreadsheetWriter.CurrentPosition;

            LastQuestionRow = PrintAssignments(spreadsheetWriter);
        }

        private int PrintAssignments(ISpreadsheetWriter spreadsheetWriter)
        {
            int lastQuestionRow = 0;
            var totalAssignments = Assignments.Count();
            for (int assignmentIndex = 0; assignmentIndex < totalAssignments; assignmentIndex++)
            {
                Assignment assignment = Assignments.ElementAt(assignmentIndex);

                spreadsheetWriter.MoveDownTimes(assignment.NumberOfQuestions);
                AssignmentBottomRows.Add(spreadsheetWriter.CurrentPosition.Y + 1);

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
