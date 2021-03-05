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
        private readonly AssignmentsBlockInput _input;

        public Point ScoresHeaderPosition { get; private set; }

        public int LastQuestionRow { get; private set; }

        public List<int> AssignmentStartRows { get; }

        public AssignmentsBlock(AssignmentsBlockInput input)
        {
            _input = input;
            AssignmentStartRows = new List<int>();
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = _input.StartPoint;
            var questionNamesCollumns = _input.NumberOfVersions;

            spreadsheetWriter
                .Write("Opgave")
                .MoveRightTimes(questionNamesCollumns)
                .Write("Punten");
            ScoresHeaderPosition = spreadsheetWriter.CurrentPosition;

            LastQuestionRow = PrintAssignments(spreadsheetWriter);
        }

        private int PrintAssignments(ISpreadsheetWriter spreadsheetWriter)
        {
            int lastQuestionRow = 0;
            var totalAssignments = _input.Assignments.Count();
            for (int assignmentIndex = 0; assignmentIndex < totalAssignments; assignmentIndex++)
            {
                Assignment assignment = _input.Assignments.ElementAt(assignmentIndex);
                AssignmentStartRows.Add(spreadsheetWriter.CurrentPosition.Y + 1);

                spreadsheetWriter.MoveDownTimes(assignment.NumberOfQuestions);

                bool isLastAssignmentReached = assignmentIndex == totalAssignments - 1;
                if (isLastAssignmentReached)
                {
                    lastQuestionRow = spreadsheetWriter.CurrentPosition.Y;
                }
            }
            return lastQuestionRow;
        }

        public class AssignmentsBlockInput : BlockInputBase
        {
            public int NumberOfVersions { get; }

            public IEnumerable<Assignment> Assignments { get; }

            public AssignmentsBlockInput(Point startPoint,
            IEnumerable<Assignment> assignments,
            int numberOfVersions) : base(startPoint)
            {
                Assignments = assignments;
                NumberOfVersions = numberOfVersions;
            }
        }
    }
}
