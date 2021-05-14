using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eudic2Readwise.Services;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eudic2Readwise.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        public static Dictionary<string, string> Config = JsonConvert.DeserializeObject<Dictionary<string, string>>(System.IO.File.ReadAllText("local.settings.json"));
        
        public static string EudicAPIKey = Config["EudicAPIKey"];
        public static string EudicDomain = Config["EudicDomain"];
        public static string EudicRetrieveEndpointPath = Config["EudicRetrieveEndpointPath"];

        public static string ReadWiseAPIToken = Config["ReadWiseAPIToken"];
        public static string ReadWiseDomain = Config["ReadWiseDomain"];
        public static string ReadWiseAddHighlightsEndpointPath = Config["ReadWiseAddHighlightsEndpointPath"];

        [TestMethod]
        public void TestGetWords()
        {
            var ss = new SyncService(EudicAPIKey, EudicDomain, EudicRetrieveEndpointPath, ReadWiseAPIToken, ReadWiseDomain, ReadWiseAddHighlightsEndpointPath);
            var wordList = ss.RetrieveEudicWordList();
            Assert.IsNotNull(wordList);
            Assert.IsTrue(wordList.data.Count > 0);
        }

        [TestMethod]
        public void TestAddingHighlights()
        {
            var ss = new SyncService(EudicAPIKey, EudicDomain, EudicRetrieveEndpointPath, ReadWiseAPIToken, ReadWiseDomain, ReadWiseAddHighlightsEndpointPath);
            var testwords = new string[] { "test2", "test3" };

            var results = ss.AddHighlights(testwords);
            Assert.IsFalse(string.IsNullOrEmpty(results));
        }
    }
}
