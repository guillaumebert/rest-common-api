using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Neotys.CommonAPI.Client
{
    /// <summary>
    /// Contains common utilities to connect to a Neotys Rest Json API Server.
    /// 
    /// @author gbert
    /// 
    /// </summary>
    public class NeotysAPIClientJson
    {
        private readonly string url;
        private readonly string apiKey;

        private const String GET = "GET";
        private const String POST = "POST";
        private const String PUT = "PUT";
        private const String PATCH = "PATCH";
        private const String DELETE = "DELETE";
        private const String CONTENT_TYPE_JSON = "application/json";

        private const String PATH_USAGE = "/usage";

        public NeotysAPIClientJson(string host, string port, string apiKey)
        {
            this.url = "http://" + host + ":" + port + "/api/v1";
            this.apiKey = apiKey;
        }

        public Task<WebResponse> PostUsage(string json)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + PATH_USAGE);
            httpWebRequest.ContentType = CONTENT_TYPE_JSON;
            httpWebRequest.Method = POST;
            httpWebRequest.Headers.Add("apiKey", apiKey);
            Console.WriteLine(url + PATH_USAGE);


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }
            return httpWebRequest.GetResponseAsync();
        }
    }
}
