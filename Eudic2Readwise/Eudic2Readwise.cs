using Eudic2Readwise.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Eudic2Readwise
{
    public static class Eudic2Readwise
    {
        public static string EudicAPIKey = Environment.GetEnvironmentVariable("EudicAPIKey");
        public static string EudicDomain = Environment.GetEnvironmentVariable("EudicDomain");
        public static string EudicRetrieveEndpointPath = Environment.GetEnvironmentVariable("EudicRetrieveEndpointPath");

        public static string ReadWiseAPIToken = Environment.GetEnvironmentVariable("ReadWiseAPIToken");
        public static string ReadWiseDomain = Environment.GetEnvironmentVariable("ReadWiseDomain");
        public static string ReadWiseAddHighlightsEndpointPath = Environment.GetEnvironmentVariable("ReadWiseAddHighlightsEndpointPath");

        [FunctionName("Eudic2Readwise")]
        public static void Run([TimerTrigger("%TimerSchedule%", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            var runIdentifier = Guid.NewGuid();

            log.LogInformation($"Eudic2Readwise started at: {DateTime.Now} ID: {runIdentifier}");
            log.LogInformation($"Eudic2Readwise schdule is {myTimer.Schedule} ID: {runIdentifier}");
            log.LogInformation($"Eudic2Readwise schdule last ran at: {myTimer.ScheduleStatus.Last} ID: {runIdentifier}");
            log.LogInformation($"Eudic2Readwise schdule next run at: {myTimer.ScheduleStatus.Next} ID: {runIdentifier}");

            try
            {
                var syncService = new SyncService(EudicAPIKey, EudicDomain, EudicRetrieveEndpointPath, ReadWiseAPIToken, ReadWiseDomain, ReadWiseAddHighlightsEndpointPath);

                var words = syncService.RetrieveEudicWordList().data.Select(x => x.word).ToArray();

                log.LogInformation($"{words.Count()} found at Eudic at: {DateTime.Now} ID: {runIdentifier}");

                var tenwords = string.Join(", ", words.TakeLast(10));

                log.LogInformation($"Latest 10 words found are {tenwords} from Eudic at: {DateTime.Now} ID: {runIdentifier}");

                var results = syncService.AddHighlights(words);

                log.LogInformation($"Added highlights, {results} returned by Readwise at: {DateTime.Now} ID: {runIdentifier}");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Error when running syncing. ID: {runIdentifier}");
            }

            log.LogInformation($"Eudic2Readwise finished at: {DateTime.Now} ID: {runIdentifier}");
        }
    }
}
