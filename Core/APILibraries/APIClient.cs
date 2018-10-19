using Automation.UI.Core.APILibraries.TranscriptData;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace Automation.UI.Core.APILibraries
{
    public class APIClient
    {
        public const string TOKEN_TYPE_BEARER = "Bearer";

        /// <summary>
        /// Login to the API server to get Access Token
        /// </summary>
        /// <param name="requestURL"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="audienceURL"></param>
        /// <param name="domainName"></param>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <returns>Login Token object</returns>
        public static LoginToken Login(string requestURL, string username, string password,
            string audienceURL, string domainName, string clientID, string clientSecret)
        {
            var client = new RestClient(requestURL)
            {
                Authenticator = new HttpBasicAuthenticator(clientID, clientSecret)
            };

            RestRequest request = new RestRequest() { Method = Method.POST };

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");

            request.AddParameter("grant_type", "password");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("audience", audienceURL);
            request.AddParameter("scope", "openid");
            request.AddParameter("domain", domainName);

            IRestResponse response = client.Execute(request);

            if (response.ErrorException != null)
            {
                TestContext.Out.WriteLine("API Login err: {0}", response.ErrorException.Message);

                throw new Exception("API Client Login Exception");
            }

            return JsonConvert.DeserializeObject<LoginToken>(response.Content);
        }

        /// <summary>
        /// Get the transcript file object from API server
        /// </summary>
        /// <param name="requestURL"></param>
        /// <param name="transcriptID"></param>
        /// <param name="tokenType"></param>
        /// <param name="accessToken"></param>
        /// <returns>Transcript JSON File object</returns>
        public static TranscriptJSONFile GetTranscript(string requestURL, string transcriptID,
            string tokenType, string accessToken)
        {
            // create the GET URL
            string url = string.Format("{0}/api/transcriptions/{1}/transcript", requestURL, transcriptID);

            RestClient client = new RestClient(url);

            switch (tokenType)
            {
                case TOKEN_TYPE_BEARER:
                    client.Authenticator = new JwtAuthenticator(accessToken);
                    break;
            }

            RestRequest request = new RestRequest(Method.GET);

            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);

            if (response.ErrorException != null)
            {
                TestContext.Out.WriteLine("API GetTranscript err: {0}", response.ErrorException.Message);

                throw new Exception("API Client GetTranscript Exception");
            }

            return JsonConvert.DeserializeObject<TranscriptJSONFile>(response.Content);
        }

        /// <summary>
        /// Get the transcript json file from API
        /// </summary>
        /// <param name="apiLoginAuthURL"></param>
        /// <param name="apiUsername"></param>
        /// <param name="apiPassword"></param>
        /// <param name="apiAudience"></param>
        /// <param name="apiDomain"></param>
        /// <param name="apiClientID"></param>
        /// <param name="apiClientSecret"></param>
        /// <param name="apiBaseURL"></param>
        /// <param name="transcriptionFileID"></param>
        /// <returns>Transcript json file</returns>
        public static TranscriptJSONFile GetTranscriptJSONFile(string apiLoginAuthURL, string apiUsername, string apiPassword,
            string apiAudience, string apiDomain, string apiClientID, string apiClientSecret, string apiBaseURL,
            string transcriptionFileID)
        {
            // get the page from API to re-check
            LoginToken loginToken = APIClient.Login(apiLoginAuthURL, apiUsername, apiPassword,
                apiAudience, apiDomain, apiClientID, apiClientSecret);

            return APIClient.GetTranscript(apiBaseURL, transcriptionFileID,
                loginToken.Token_Type, loginToken.Access_Token);
        }
    }
}
