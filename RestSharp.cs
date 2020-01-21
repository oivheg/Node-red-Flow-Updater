using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Web.Script.Serialization;

namespace AquateknikkUpdater
{
    internal class RestSharp
    {

        public string CheckConnection(string ipAdress, string port)
        {

            var restClient = new RestClient("http://" + ipAdress + ":" + port + "");
            //restClient.Timeout = 100;
            var restRequest = new RestRequest("/auth/login", Method.GET);

            IRestResponse response = restClient.Execute(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Access Token cannot obtain, process terminate");
                return "-1";
            }
            var ser = new JavaScriptSerializer();
            dynamic redresult = ser.Deserialize<dynamic>(response.Content);
            
            if (redresult.Count == 0)
            {
                return "2";
            }
            string name = redresult["type"];
            return "1";
        }
        public Token Authenticate(string ipAdress, string port, string user, string pass)
        {
            var restClient = new RestClient("http://" + ipAdress + ":" + port + "");

            var restRequest = new RestRequest("/auth/token", Method.POST);

            //restRequest.AddXmlBody("client_id", "node-red-editor");
            //restRequest.AddXmlBody("grant_type", "password");
            //restRequest.AddXmlBody("scope", "*");
            //restRequest.AddXmlBody("username", "admin");
            //restRequest.AddXmlBody("password", "kalinka17");
            //restRequest.AddHeader("content-type", "application/x-www-form-urlencoded");

            restRequest.AddParameter("application/x-www-form-urlencoded", $"client_id=node-red-editor&grant_type=password" + $"&scope=*" + $"&username=" + user + "" + $"&password=" + pass + "", ParameterType.RequestBody);

            //var response = restClient.Post(restRequest);

            IRestResponse response = restClient.Execute(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Access Token cannot obtain, process terminate");
                return null;
            }
            Console.WriteLine("Access Token  obtained");
            var tokenResponse = (response.Content);

            var Token = JsonConvert.DeserializeObject<Token>(tokenResponse);
            return Token;
        }

        public string Get_Flows(string token, string ipAdress, string port)
        {
            var restClient = new RestClient("http://" + ipAdress + ":" + port + "");

            if (token != null)
            {
                restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            }

            var restRequest = new RestRequest("/flows", Method.GET);

            IRestResponse response = restClient.Execute(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Flow not found");
                return null;
            }
            Console.WriteLine("Flow  found");
            var Flow = (response.Content);

            return Flow;
        }

        public void Send_Flow(string file, string token, string ipAdress, string port)
        {
            var restClient = new RestClient("http://" + ipAdress + ":" + port + "");

            if (token != null)
            {
                restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            }

            var restRequest = new RestRequest("/flows", Method.POST);

            //restRequest.AddFile("filename", fileName);
            restRequest.AddParameter("application/json", file, ParameterType.RequestBody);
            //restRequest.AddParameter("content-type", "application/json");
            IRestResponse response = restClient.Execute(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("File not Sendt ");
            }
            else
            {
Console.WriteLine("File  Sendt ");
            }
            
        }
    }
}