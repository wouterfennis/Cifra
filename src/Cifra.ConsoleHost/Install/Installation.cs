using Cifra.ConsoleHost.Install;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Istall
{
    internal static class Installation
    {
        private const string _appsettingsFileName = "appsettings.json";

        public static async Task Start(IConfigurationRoot configuration)
        {
            if (IsInstallationRequired(configuration))
            {
                object apsettingsContent = AppSettingsDefaultTemplate.Create();
                var appsettingsFile = new FileInfo(Path.Combine(Environment.CurrentDirectory, _appsettingsFileName));

                using var writer = new StreamWriter(appsettingsFile.OpenWrite());
                await writer.WriteAsync(JsonConvert.SerializeObject(apsettingsContent));
                writer.Close();
                configuration.Reload();
            }
        }

        private static bool IsInstallationRequired(IConfigurationRoot configuration)
        {
            var appsettings = configuration.GetSection("AppSettings");
            bool isAppsettingsDefined = appsettings != null;
            bool isAppsettingsExisting = appsettings.Exists();
            bool isInstalltionDatePresent = !string.IsNullOrEmpty(configuration["InstallationDate"]);
            if (isAppsettingsDefined &&
                isAppsettingsExisting &&
                isInstalltionDatePresent)
            {
                return false;
            }
            return true;
        }
    }
}
