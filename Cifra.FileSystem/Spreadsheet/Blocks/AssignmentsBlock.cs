using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.Abstractions;
using System;
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
                .Write("Punten")
                .NewLine();
            var questionNamesColumnTopLeft = spreadsheetWriter.CurrentPosition;

            foreach (Assignment assignment in input.Assignments)
            {
                // TODO color change per assignment
                foreach (Question question in assignment.Questions)
                {
                    foreach (Name questionName in question.QuestionNames)
                    {
                        spreadsheetWriter
                            .Write(questionName.Value)
                            .MoveRight();
                    }
                    spreadsheetWriter
                        .Write(question.MaximumScore.Value)
                        .NewLine();
                }
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
