using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.PackageUpdate.Nuget
{
    class NullLogger : ILogger
    {
        public void LogDebug(string data) { }
        public void LogVerbose(string data) { }
        public void LogInformation(string data) { }
        public void LogMinimal(string data) { }
        public void LogWarning(string data) { }
        public void LogError(string data) { }
        public void LogSummary(string data) { }
        public void LogInformationSummary(string data) { }
        public void LogErrorSummary(string data) { }
    }
}
