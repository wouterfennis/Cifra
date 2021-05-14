using Cifra.ConsoleHost.Install;
using Cifra.ConsoleHost.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cifra.ConsoleHost.UnitTests.Install
{
    [TestClass]
    public class AppSettingsDefaultTemplateTests
    {
        [TestMethod]
        public void Create_WithCurrentDirectory_CreatesDefaultAppSettingsObject()
        {
            // Arrange

            // Act
            object result = AppSettingsDefaultTemplate.Create();

            // Assert
            object appSettings = result.GetType().GetProperty("AppSettings").GetValue(result);
            appSettings.Should().NotBeNull();
        }

        [TestMethod]
        public void Create_WithCurrentDirectory_CreatesDefaultInstallationDate()
        {
            // Arrange
            DateTime expectedDateTime = DateTime.Now;
            DateTimeProvider.Now = () => expectedDateTime; 

            // Act
            object result = AppSettingsDefaultTemplate.Create();

            // Assert
            object appSettings = result.GetType().GetProperty("AppSettings").GetValue(result);
            object installationDateObj = appSettings.GetType().GetProperty("InstallationDate").GetValue(appSettings);
            DateTime installationDate = (DateTime)installationDateObj;

            installationDate.Should().Be(expectedDateTime);
        }

        [TestMethod]
        public void Create_WithCurrentDirectory_CreatesDefaultClassRepositoryLocation()
        {
            // Arrange

            // Act
            object result = AppSettingsDefaultTemplate.Create();

            // Assert
            object appSettings = result.GetType().GetProperty("AppSettings").GetValue(result);
            object classRepositoryObj = appSettings.GetType().GetProperty("ClassRepository").GetValue(appSettings);
            string classRepository = (string)classRepositoryObj;

            classRepository.Should().Contain("classes.json");
        }

        [TestMethod]
        public void Create_WithCurrentDirectory_CreatesDefaultTestRepositoryLocation()
        {
            // Arrange

            // Act
            object result = AppSettingsDefaultTemplate.Create();

            // Assert
            object appSettings = result.GetType().GetProperty("AppSettings").GetValue(result);
            object testRepositoryObj = appSettings.GetType().GetProperty("TestRepository").GetValue(appSettings);
            string testRepository = (string)testRepositoryObj;

            testRepository.Should().Contain("tests.json");
        }

        [TestMethod]
        public void Create_WithCurrentDirectory_CreatesDefaultSpreadsheetLocation()
        {
            // Arrange

            // Act
            object result = AppSettingsDefaultTemplate.Create();

            // Assert
            object appSettings = result.GetType().GetProperty("AppSettings").GetValue(result);
            object spreadsheetDirectoryObj = appSettings.GetType().GetProperty("SpreadsheetDirectory").GetValue(appSettings);
            string spreadsheetDirectory = (string)spreadsheetDirectoryObj;

            spreadsheetDirectory.Should().Contain("Spreadsheets");
        }

        [TestMethod]
        public void Create_WithCurrentDirectory_CreatesDefaultMagisterLocation()
        {
            // Arrange

            // Act
            object result = AppSettingsDefaultTemplate.Create();

            // Assert
            object appSettings = result.GetType().GetProperty("AppSettings").GetValue(result);
            object magisterDirectoryObj = appSettings.GetType().GetProperty("MagisterDirectory").GetValue(appSettings);
            string magisterDirectory = (string)magisterDirectoryObj;

            magisterDirectory.Should().Contain("Magister");
        }

        [TestMethod]
        public void Create_WithCurrentDirectory_CreatesDefaultEppPlusObject()
        {
            // Arrange

            // Act
            object result = AppSettingsDefaultTemplate.Create();

            // Assert
            object epplusSettings = result.GetType().GetProperty("EPPlus").GetValue(result);
            epplusSettings.Should().NotBeNull();
        }

        [TestMethod]
        public void Create_WithCurrentDirectory_CreatesDefaultEppPlusLicense()
        {
            // Arrange

            // Act
            object result = AppSettingsDefaultTemplate.Create();

            // Assert
            object epplusSettings = result.GetType().GetProperty("EPPlus").GetValue(result);
            object excelPackage = epplusSettings.GetType().GetProperty("ExcelPackage").GetValue(epplusSettings);
            excelPackage.Should().NotBeNull();
            object licenseContextObj = excelPackage.GetType().GetProperty("LicenseContext").GetValue(excelPackage);
            licenseContextObj.Should().BeOfType<string>();
            string licenseContext = (string)licenseContextObj;

            licenseContext.Should().Be("NonCommercial");
        }
    }
}
