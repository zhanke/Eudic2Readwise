using Eudic2Readwise.Models.EudicResponse;
using Eudic2Readwise.Models.ReadwiseRequest;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Eudic2Readwise.Services
{
    public class SyncService
    {
        public string EudicAPIKey { get; private set; }
        public string EudicDomain { get; private set; }
        public string EudicRetrieveEndpointPath { get; private set; }
        public string ReadWiseAPIToken { get; private set; }
        public string ReadWiseDomain { get; private set; }
        public string ReadWiseAddHighlightsEndpointPath { get; private set; }

        public SyncService(string eudicAPIKey, string eudicDomain, string eudicRetrieveEndpointPath,
            string readWiseAPIToken, string readWiseDomain,string readWiseAddHighlightsEndpointPath)
        {
            EudicAPIKey = eudicAPIKey;
            EudicDomain = eudicDomain;
            EudicRetrieveEndpointPath = eudicRetrieveEndpointPath;

            ReadWiseAPIToken = readWiseAPIToken;
            ReadWiseDomain = readWiseDomain;
            ReadWiseAddHighlightsEndpointPath = readWiseAddHighlightsEndpointPath;
        }

        public EudicRoot RetrieveEudicWordList()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(EudicDomain + EudicRetrieveEndpointPath);
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", EudicAPIKey);

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();

                EudicRoot wordList = JsonConvert.DeserializeObject<EudicRoot>(responseText);

                return wordList;
            }
        }

        public string AddHighlights(string[] words)
        {
            var root = new ReadwiseRoot
            {
                highlights = words.Select(x => new Highlight { text = x }).ToList()
            };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ReadWiseDomain + ReadWiseAddHighlightsEndpointPath);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Token " + ReadWiseAPIToken);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(root));
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }

        private static string GetEnvironmentVariable(string name)
        {
            return 
                System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
