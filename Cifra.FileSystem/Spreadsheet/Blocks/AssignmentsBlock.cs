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
        private readonly AssignmentsBlockInput input;

        public Point ScoresHeaderPosition { get; private set; }

        public Point LastMaximumValuePosition { get; private set; }

        public AssignmentsBlock(AssignmentsBlockInput input)
        {
            this.input = input;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = input.StartPoint;
            var questionNamesCollumns = input.NumberOfVersions;

            spreadsheetWriter
                .Write("Opgave")
                .MoveRightTimes(questionNamesCollumns)
                .Write("Punten");
            ScoresHeaderPosition = spreadsheetWriter.CurrentPosition;
            spreadsheetWriter
                .NewLine();

            LastMaximumValuePosition = PrintAssignments(spreadsheetWriter);
        }

        private Point PrintAssignments(ISpreadsheetWriter spreadsheetWriter)
        {
            Point lastMaximumValuePosition;
            var totalAssignments = input.Assignments.Count();
            for (int assignmentIndex = 0; assignmentIndex < totalAssignments; assignmentIndex++)
            {
                Assignment assignment = input.Assignments.ElementAt(assignmentIndex);
                var totalQuestions = assignment.NumberOfQuestions;
                for (int questionIndex = 0; questionIndex < assignment.NumberOfQuestions; questionIndex++)
                {
                    bool isLastMaximumValueReached = assignmentIndex == totalAssignments - 1 && questionIndex == totalQuestions - 1;
                    if (isLastMaximumValueReached)
                    {
                        lastMaximumValuePosition = spreadsheetWriter.CurrentPosition;
                    }
                    spreadsheetWriter
                        .NewLine();
                }
            }
            return lastMaximumValuePosition;
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
