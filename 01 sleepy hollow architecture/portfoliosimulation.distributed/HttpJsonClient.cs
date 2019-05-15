using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace portfoliosimulation.distributed
{
    class HttpJsonClient
    {
        private readonly string _baseUrl;

        public HttpJsonClient(string baseUrl) { _baseUrl = baseUrl; }
        
        
        public TResponse Execute<TResponse>(string endpoint, object request)
        {
            var endpointUrl = _baseUrl + endpoint;
            Console.WriteLine("🌐...");

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(endpointUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                streamWriter.Write(JsonConvert.SerializeObject(request));
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                var response = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<TResponse>(response);
            }
        }
    }
}