﻿using Cifra.ConsoleHost.Utilities;
using System;
using System.Runtime.InteropServices;

namespace Cifra.ConsoleHost.Install
{
    /// <summary>
    /// Template for a default appsettings file.
    /// </summary>
    internal class AppSettingsDefaultTemplate
    {
        private const string _classesFileName = "classes.json";
        private const string _testsFileName = "tests.json";
        private const string _spreadsheetDirectoryName = "Spreadsheets";
        private const string _classesDirectoryName = "Classes";
        private const string _standardEpplusLicense = "NonCommercial";

        /// <summary>
        /// Creates a default AppSettings object.
        /// </summary>
        public static object Create() {
            string currentDirectory = Environment.CurrentDirectory;
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            string slash = isWindows ? "\\" : "/";
            return new
            {
                AppSettings = new
                {
                    InstallationDate = DateTimeProvider.Now(),
                    ClassRepository = $"{currentDirectory}{slash}{_classesFileName}",
                    TestRepository = $"{currentDirectory}{slash}{_testsFileName}",
                    SpreadsheetDirectory = $"{currentDirectory}{slash}{_spreadsheetDirectoryName}",
                    ClassesDirectory = $"{currentDirectory}{slash}{_classesDirectoryName}",
                },
                EPPlus = new
                {
                    ExcelPackage = new
                    {
                        LicenseContext = _standardEpplusLicense
                    }
                },
                Serilog = new
                {
                    Using = new[] { "Serilog.Sinks.File" },
                    MinimumLevel = "Information",
                    WriteTo = new dynamic[] {
                        new {
                        Name = "File",
                        Args = new {
                            path = $"{currentDirectory}{slash}logs.json",
                            formatter = "Serilog.Formatting.Json.JsonFormatter, Serilog"
                            }
                        }
                    },
                    Properties = new {
                        Application = "Cifra"
                    }
                }
            };
        }
    }

}