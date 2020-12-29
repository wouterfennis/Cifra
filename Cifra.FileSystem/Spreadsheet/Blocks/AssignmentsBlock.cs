using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.Abstractions;
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
            var questionNamesCollumns = input.Assignments.Max(x => x.GetMaximumQuestionNamesPerQuestion());

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
                var totalQuestions = assignment.Questions.Count;
                for (int questionIndex = 0; questionIndex < totalQuestions; questionIndex++)
                {
                    Question question = assignment.Questions.ElementAt(questionIndex);
                    PrintQuestionNames(spreadsheetWriter, question);
                    spreadsheetWriter
                        .Write(question.MaximumScore.Value);
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

        private static void PrintQuestionNames(ISpreadsheetWriter spreadsheetWriter, Question question)
        {
            foreach (Name questionName in question.QuestionNames)
            {
                spreadsheetWriter
                    .Write(questionName.Value)
                    .MoveRight();
            }
        }

        public class AssignmentsBlockInput : BlockInputBase
        {
            public IEnumerable<Assignment> Assignments { get; }

            public AssignmentsBlockInput(Point startPoint,
            IEnumerable<Assignment> assignments) : base(startPoint)
            {
                Assignments = assignments;
            }
        }
    }
}
