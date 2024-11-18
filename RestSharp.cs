using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace AquateknikkUpdater
{
    internal class RestSharp
    {
        private static HttpClient InitializeClient(string ipAddress, string port)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri($"http://{ipAddress}:{port}")
            };
            return client;
        }

        public async Task<string> CheckConnectionAsync(string ipAddress, string port)
        {
            using var client = InitializeClient(ipAddress, port);
            var response = await client.GetAsync("/auth/login");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Access Token cannot obtain, process terminate");
                throw new HttpRequestException("Failed to connect");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(content);

            return result.ValueKind == JsonValueKind.Object ? "2" : "1";
        }

        public async Task<Token> AuthenticateAsync(string ipAddress, string port, string user, string pass)
        {
            using var client = InitializeClient(ipAddress, port);
            var requestContent = new StringContent(
                $"client_id=node-red-editor&grant_type=password&scope=*&username={user}&password={pass}",
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var response = await client.PostAsync("/auth/token", requestContent);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Access Token cannot obtain, process terminate");
                throw new HttpRequestException("Failed to authenticate");
            }

            Console.WriteLine("Access Token obtained");
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Token>(content);
        }

        public async Task<string> GetFlowsAsync(string token, string ipAddress, string port)
        {
            using var client = InitializeClient(ipAddress, port);
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync("/flows");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Flow not found");
                throw new HttpRequestException("Failed to get flows");
            }

            Console.WriteLine("Flow found");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task SendFlowAsync(string file, string token, string ipAddress, string port)
        {
            using var client = InitializeClient(ipAddress, port);
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var requestContent = new StringContent(file, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/flows", requestContent);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("File not sent");
                throw new HttpRequestException("Failed to send flow");
            }

            Console.WriteLine("File sent");
        }
    }
}