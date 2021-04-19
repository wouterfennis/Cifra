using System;

namespace Cifra.ConsoleHost.Install
{
    internal class AppSettingsDefaultTemplate
    {
        private const string _classesFileName = "classes.json";
        private const string _testsFileName = "tests.json";
        private const string _spreadsheetDirectoryName = "Spreadsheets";
        private const string _magisterDirectoryName = "Magister";
        private const string _standardEpplusLicense = "NonCommercial";

        /// <summary>
        /// Creates a default AppSettings object.
        /// </summary>
        public static object Create() {
            string currentDirectory = Environment.CurrentDirectory;
            return new
            {
                AppSettings = new
                {
                    InstallationDate = DateTime.Now,
                    ClassRepository = $"{currentDirectory}\\{_classesFileName}",
                    TestRepository = $"{currentDirectory}\\{_testsFileName}",
                    SpreadsheetDirectory = $"{currentDirectory}\\{_spreadsheetDirectoryName}",
                    MagisterDirectory = $"{currentDirectory}\\{_magisterDirectoryName}",
                },
                EPPlus = new
                {
                    ExcelPackage = new
                    {
                        LicenseContext = _standardEpplusLicense
                    }
                }
            };
        }
    }
}