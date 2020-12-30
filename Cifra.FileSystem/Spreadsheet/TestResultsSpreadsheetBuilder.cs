﻿using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Mapping;
using Cifra.FileSystem.Spreadsheet.Blocks;
using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.EPPlus;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Cifra.FileSystem.Spreadsheet
{
    public class TestResultsSpreadsheetBuilder : ITestResultsSpreadsheetFactory
    {
        private readonly IFileLocationProvider _locationProvider;
        private readonly ISpreadsheetFileFactory _spreadsheetFileFactory;
        private readonly IFormulaBuilderFactory _formulaBuilderFactory;

        public TestResultsSpreadsheetBuilder(IFileLocationProvider locationProvider,
            ISpreadsheetFileFactory spreadsheetFileFactory,
            IFormulaBuilderFactory formulaBuilderFactory)
        {
            _locationProvider = locationProvider;
            _spreadsheetFileFactory = spreadsheetFileFactory;
            _formulaBuilderFactory = formulaBuilderFactory;
        }

        public async Task CreateTestResultsSpreadsheetAsync(Class @class, Test test, Application.Models.Spreadsheet.Metadata metadata)
        {
            var libraryMetadata = metadata.MapToLibraryModel();
            ISpreadsheetFile spreadsheetFile = _spreadsheetFileFactory.Create(_locationProvider.GetSpreadsheetDirectoryPath().Value, libraryMetadata);

            ISpreadsheetWriter spreadsheetWriter = spreadsheetFile.GetSpreadsheetWriter();

            AddTitle(test, metadata, spreadsheetWriter);
            spreadsheetWriter.NewLine();

            var configurationBlock = AddConfiguration(test, spreadsheetWriter);

            PrintScoreSheet(
                @class, 
                test, 
                spreadsheetWriter, 
                configurationBlock);

            await spreadsheetFile.SaveAsync();
        }

        private static void AddTitle(Test test, Application.Models.Spreadsheet.Metadata metadata, ISpreadsheetWriter spreadsheetWriter)
        {
            var titleInput = new TitleBlock.TitleBlockInput(spreadsheetWriter.CurrentPosition,
                test.Name,
                metadata.Created);
            var titleBlock = new TitleBlock(titleInput);
            titleBlock
                .Write(spreadsheetWriter);
        }

        private static ConfigurationBlock AddConfiguration(Test test, ISpreadsheetWriter spreadsheetWriter)
        {
            var configurationInput = new ConfigurationBlock.ConfigurationBlockInput(spreadsheetWriter.CurrentPosition,
                test.GetMaximumPoints(),
                test.StandardizationFactor,
                test.MinimumGrade);
            var configurationBlock = new ConfigurationBlock(configurationInput);
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
            var assignmentsBlockInput = new AssignmentsBlock.AssignmentsBlockInput(spreadsheetWriter.CurrentPosition,
                test.Assignments);
            var assignmentsBlock = new AssignmentsBlock(assignmentsBlockInput);
            assignmentsBlock.Write(spreadsheetWriter);

            spreadsheetWriter.CurrentPosition = assignmentsBlock.ScoresHeaderPosition;
            spreadsheetWriter.MoveRight();
            var studentNamesInput = new StudentNamesBlock.StudentNamesBlockInput(spreadsheetWriter.CurrentPosition, @class.Students);
            var studentNamesBlock = new StudentNamesBlock(studentNamesInput);
            studentNamesBlock.Write(spreadsheetWriter);

            spreadsheetWriter.CurrentPosition = assignmentsBlock.LastMaximumValuePosition;
            spreadsheetWriter.NewLine();
            var scoresTopRow = new Point(assignmentsBlock.ScoresHeaderPosition.X, assignmentsBlock.ScoresHeaderPosition.Y + 1);
            AddTotalPointsRow(spreadsheetWriter,
                scoresTopRow,
                @class.Students.Count);
            var achievedScoresRow = spreadsheetWriter.CurrentPosition.Y;
            spreadsheetWriter.NewLine();

            AddGradesRow(spreadsheetWriter,
                achievedScoresRow,
                assignmentsBlock.LastMaximumValuePosition.X,
                configurationBlock.MaximumPointsPosition,
                configurationBlock.MinimumGradePosition,
                configurationBlock.StandardizationfactorPosition,
                @class.Students.Count,
                _formulaBuilderFactory.Create()) ;

            AddAverageResults();
        }

        private static void AddTotalPointsRow(ISpreadsheetWriter spreadsheetWriter,
            Point scorePointTop,
            int numberOfStudents)
        {
            var totalPointsBlockInput = new TotalScoresBlock.TotalScoresBlockInput(
                spreadsheetWriter.CurrentPosition,
                scorePointTop,
                numberOfStudents);
            var totalPointsBlock = new TotalScoresBlock(totalPointsBlockInput);
            totalPointsBlock.Write(spreadsheetWriter);
        }

        private static void AddGradesRow(ISpreadsheetWriter spreadsheetWriter,
            int achievedScoresRow,
            int scoresStartColumn,
            Point maximumPointsPosition,
            Point minimumGradePosition,
            Point standardizationfactorPosition,
            int numberOfStudents,
            IFormulaBuilder formulaBuilder)
        {
            var totalPointsBlockInput = new GradesBlock.GradesBlockInput(
                spreadsheetWriter.CurrentPosition,
                formulaBuilder,
                achievedScoresRow,
                scoresStartColumn,
                maximumPointsPosition,
                standardizationfactorPosition,
                minimumGradePosition,
                numberOfStudents);
            var totalPointsBlock = new GradesBlock(totalPointsBlockInput);
            totalPointsBlock.Write(spreadsheetWriter);
        }

        private static void AddAverageResults()
        {
            // throw new NotImplementedException();
        }
    }
}
