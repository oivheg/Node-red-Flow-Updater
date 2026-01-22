using RestSharp;
using System;
using System.Net;
using System.Text.Json;


namespace AquateknikkUpdater
{
    internal class ApiClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public string CheckConnection(string ipAdress, string port)
        {

            var restClient = new RestClient($"http://{ipAdress}:{port}");
            var restRequest = new RestRequest("/auth/login", Method.Get);

            RestResponse response = restClient.Execute(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Access Token cannot obtain, process terminate");
                return "-1";
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                return "1";
            }

            try
            {
                using var doc = JsonDocument.Parse(response.Content);
                if (doc.RootElement.ValueKind == JsonValueKind.Object)
                {
                    return "2";
                }
            }
            catch
            {
                // If content is not JSON object, treat as login required
            }

            return "1";
        }
        public Token Authenticate(string ipAdress, string port, string user, string pass)
        {
            var restClient = new RestClient($"http://{ipAdress}:{port}");

            var restRequest = new RestRequest("/auth/token", Method.Post);

            restRequest.AddParameter("application/x-www-form-urlencoded", $"client_id=node-red-editor&grant_type=password&scope=*&username={user}&password={pass}", ParameterType.RequestBody);

            RestResponse response = restClient.Execute(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Access Token cannot obtain, process terminate");
                return null;
            }
            Console.WriteLine("Access Token obtained");
            var tokenResponse = response.Content;

            if (string.IsNullOrWhiteSpace(tokenResponse)) return null;

            var Token = JsonSerializer.Deserialize<Token>(tokenResponse, JsonOptions);
            return Token;
        }

        public string Get_Flows(string token, string ipAdress, string port)
        {
            var restClient = new RestClient($"http://{ipAdress}:{port}");

            if (!string.IsNullOrWhiteSpace(token))
            {
                restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            }

            var restRequest = new RestRequest("/flows", Method.Get);

            RestResponse response = restClient.Execute(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Flow not found");
                return null;
            }
            Console.WriteLine("Flow found");
            var Flow = response.Content;

            return Flow;
        }

        public void Send_Flow(string file, string token, string ipAdress, string port)
        {
            var restClient = new RestClient($"http://{ipAdress}:{port}");

            if (!string.IsNullOrEmpty(token))
            {
                restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            }

            var restRequest = new RestRequest("/flows", Method.Post);

            restRequest.AddParameter("application/json", file, ParameterType.RequestBody);
            RestResponse response = restClient.Execute(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("File not sent");
            }
            else
            {
                Console.WriteLine("File sent");
            }

        }
    }
}