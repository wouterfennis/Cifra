﻿using Cifra.Application.Interfaces;
using Cifra.Domain;
using Cifra.FileSystem.Mapping;
using Cifra.FileSystem.Options;
using Cifra.FileSystem.Spreadsheet.Blocks;
using Microsoft.Extensions.Options;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.File;
using SpreadsheetWriter.Abstractions.Formula;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Cifra.FileSystem.Spreadsheet
{
    public class TestResultsSpreadsheetBuilder : ITestResultsSpreadsheetBuilder
    {
        private readonly SpreadsheetOptions _spreadsheetOptions;
        private readonly ISpreadsheetFileFactory _spreadsheetFileFactory;
        private readonly IFormulaBuilderFactory _formulaBuilderFactory;

        public TestResultsSpreadsheetBuilder(IOptions<SpreadsheetOptions> spreadsheetOptions,
            ISpreadsheetFileFactory spreadsheetFileFactory,
            IFormulaBuilderFactory formulaBuilderFactory)
        {
            _spreadsheetOptions = spreadsheetOptions?.Value ?? throw new ArgumentNullException(nameof(spreadsheetOptions));
            _spreadsheetFileFactory = spreadsheetFileFactory ?? throw new ArgumentNullException(nameof(spreadsheetFileFactory));
            _formulaBuilderFactory = formulaBuilderFactory ?? throw new ArgumentNullException(nameof(formulaBuilderFactory));
        }

        public async Task<FileInfo> CreateTestResultsSpreadsheetAsync(Class @class, Test test, Domain.Spreadsheet.Metadata metadata)
        {
            var libraryMetadata = metadata.MapToLibraryModel();
            Directory.CreateDirectory(_spreadsheetOptions.TestResultsDirectory);
            ISpreadsheetFile spreadsheetFile = _spreadsheetFileFactory.Create(_spreadsheetOptions.TestResultsDirectory, libraryMetadata);
            ISpreadsheetWriter spreadsheetWriter = spreadsheetFile.GetSpreadsheetWriter();

            AddTitle(metadata, spreadsheetWriter);
            spreadsheetWriter.NewLine();

            var configurationBlock = AddConfiguration(test, spreadsheetWriter);

            PrintScoreSheet(
                @class,
                test,
                spreadsheetWriter,
                configurationBlock);

            var result = await spreadsheetFile.SaveAsync();

            return result.FileInfo;
        }

        private static void AddTitle(Domain.Spreadsheet.Metadata metadata, ISpreadsheetWriter spreadsheetWriter)
        {
            var titleBlock = new TitleBlock(spreadsheetWriter.CurrentPosition,
                metadata.FileName,
                metadata.Created,
                metadata.ApplicationVersion);
            titleBlock
               .Write(spreadsheetWriter);
        }

        private static ConfigurationBlock AddConfiguration(Test test, ISpreadsheetWriter spreadsheetWriter)
        {
            var configurationBlock = new ConfigurationBlock(spreadsheetWriter.CurrentPosition,
                test.StandardizationFactor,
                test.MinimumGrade);
            spreadsheetWriter
                .NewLine()
                .NewLine();
            configurationBlock.Write(spreadsheetWriter);

            return configurationBlock;
        }

        private void PrintScoreSheet(
            Class @class,
            Test test,
            ISpreadsheetWriter spreadsheetWriter,
            ConfigurationBlock configurationBlock)
        {
            spreadsheetWriter
                .NewLine()
                .NewLine();

            var assignmentsBlock = new AssignmentsBlock(spreadsheetWriter.CurrentPosition,
                test.Assignments,
                test.NumberOfVersions);
            assignmentsBlock.Write(spreadsheetWriter);

            spreadsheetWriter.CurrentPosition = assignmentsBlock.ScoresHeaderPosition;
            spreadsheetWriter.MoveRight();
            int studentNamesStartColumn = spreadsheetWriter.CurrentPosition.X;
            var studentNamesBlock = new StudentNamesBlock(spreadsheetWriter.CurrentPosition, @class.Students, assignmentsBlock.LastQuestionRow);
            studentNamesBlock.Write(spreadsheetWriter);

            spreadsheetWriter.CurrentPosition = new Point(spreadsheetWriter.CurrentPosition.X, assignmentsBlock.LastQuestionRow);
            spreadsheetWriter.NewLine();

            var scoresTopRow = new Point(assignmentsBlock.ScoresHeaderPosition.X, assignmentsBlock.ScoresHeaderPosition.Y + 1);
            AddTotalPointsRow(spreadsheetWriter,
                scoresTopRow,
                @class.Students.Count);
            var achievedScoresRow = spreadsheetWriter.CurrentPosition.Y;
            spreadsheetWriter.NewLine();

            var numberOfStudents = @class.Students.Count;
            AddGradesRow(spreadsheetWriter,
                achievedScoresRow,
                assignmentsBlock.ScoresHeaderPosition.X,
                new Point(assignmentsBlock.ScoresHeaderPosition.X, achievedScoresRow),
                configurationBlock.MinimumGradePosition,
                configurationBlock.StandardizationfactorPosition,
                numberOfStudents,
                _formulaBuilderFactory);
            var gradesRow = spreadsheetWriter.CurrentPosition.Y;
            spreadsheetWriter.NewLine();
            spreadsheetWriter.NewLine();

            AddAverageResults(
                spreadsheetWriter,
                achievedScoresRow,
                gradesRow,
                studentNamesStartColumn,
                numberOfStudents);

            var borderBlock = new BorderBlock(
                assignmentsBlock.ScoresHeaderPosition.Y,
                assignmentsBlock.AssignmentBottomRows,
                achievedScoresRow,
                gradesRow,
                studentNamesBlock.MostOuterColumn);
            borderBlock.Write(spreadsheetWriter);
        }

        private static void AddTotalPointsRow(ISpreadsheetWriter spreadsheetWriter,
            Point scorePointTop,
            int numberOfStudents)
        {
            var totalPointsBlock = new TotalScoresBlock(
                spreadsheetWriter.CurrentPosition,
                scorePointTop,
                numberOfStudents);
            totalPointsBlock.Write(spreadsheetWriter);
        }

        private static void AddGradesRow(ISpreadsheetWriter spreadsheetWriter,
            int achievedScoresRow,
            int scoresStartColumn,
            Point maximumPointsPosition,
            Point minimumGradePosition,
            Point standardizationfactorPosition,
            int numberOfStudents,
            IFormulaBuilderFactory formulaBuilderFactory)
        {
            var totalPointsBlock = new GradesBlock(
                spreadsheetWriter.CurrentPosition,
                formulaBuilderFactory,
                achievedScoresRow,
                scoresStartColumn,
                maximumPointsPosition,
                standardizationfactorPosition,
                minimumGradePosition,
                numberOfStudents);
            totalPointsBlock.Write(spreadsheetWriter);
        }

        private static void AddAverageResults(ISpreadsheetWriter spreadsheetWriter,
            int achievedScoresRow,
            int gradesRow,
            int scoresStartColumn,
            int numberOfStudents)
        {
            var averagesBlock = new StatisticsBlock(
                spreadsheetWriter.CurrentPosition,
                achievedScoresRow,
                gradesRow,
                scoresStartColumn,
                numberOfStudents);
            averagesBlock.Write(spreadsheetWriter);
        }
    }
}
