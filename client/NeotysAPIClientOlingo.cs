using System;
using System.Collections.Generic;
using Edm = Microsoft.Data.Edm.IEdmModel;
using Simple.OData.Client;
using EdmxReader = Microsoft.Data.Edm.Csdl.EdmxReader;
using System.Threading.Tasks;
using NeotysAPIException = Neotys.CommonAPI.Error.NeotysAPIException;
using System.Text.RegularExpressions;
using Neotys.CommonAPI.Data;
using System.Net;
using System.IO;
using System.Text;

/*
 * Copyright (c) 2016, Neotys
 * All rights reserved.
 */
namespace Neotys.CommonAPI.Client
{
    /// <summary>
    /// Contains common utilities to connect to a Neotys OData API Server using Apache Olingo implementation.
    /// 
    /// @author srichert
    /// 
    /// </summary>
    public abstract class NeotysAPIClientOlingo
    {
        private readonly Edm edm;
        private readonly string url;
        private readonly bool enabled;

        private const string Metadata = "$metadata";
        private const string Separator = "/";
        private const String applicationJson = "application/json";
        private const String methodPost = "POST";

        /// <summary>
        /// Use -Dnl.client.enabled=false to disable the interaction with the Rest API server.
        /// </summary>
        private const string ArgClientEnabled = "nl.client.enabled";

        protected internal NeotysAPIClientOlingo(string url)
        {
            this.enabled = CheckArgumentIsEnabled();
            if (enabled)
            {
                this.edm = ReadEdm(url);
                this.url = url;
            }
            else
            {
                this.edm = null;
                this.url = null;
            }
        }

        private static bool CheckArgumentIsEnabled()
        {
            string environmentVariableArgument = Environment.GetEnvironmentVariable(ArgClientEnabled);
            string[] clas = Environment.GetCommandLineArgs();
            List<string> commandLineArgumentsList = new List<string>();
            commandLineArgumentsList.AddRange(clas);


            if (environmentVariableArgument != null)
            {
                try
                {
                    return Convert.ToBoolean(environmentVariableArgument);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            foreach (string arg in commandLineArgumentsList)
            {
                if (string.Equals(ArgClientEnabled + "=false", arg, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        private static Edm ReadEdm(string serviceUrl)
        {
            System.IO.Stream content = Execute(serviceUrl + Separator + Metadata);
            System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(serviceUrl + Separator + Metadata);

            Edm edm = EdmxReader.Parse(xmlReader);

            return edm;
        }

        protected internal virtual ODataEntry ReadEntity(string entitySetName, IDictionary<string, object> properties)
        {
            if (!enabled)
            {
                return null;
            }
            return WriteEntity(edm, url, entitySetName, properties);
        }

        protected internal virtual BinaryData ReadBinary(string entitySetName, string apiKey, string jsonContent)
        {
            if (!enabled) {
                return null;
            }
            return WriteBinary(edm, url, entitySetName, apiKey, jsonContent);
        }

        protected internal virtual void CreateEntity(string entitySetName, IDictionary<string, object> properties)
        {
            if (!enabled)
            {
                return;
            }
            WriteEntity(edm, url, entitySetName, properties);
        }

        protected internal virtual void CreateFeed(string entitySetName, IList<IDictionary<string, object>> propertiesList)
        {
            if (!enabled)
            {
                return;
            }
            WriteFeed(edm, url, entitySetName, propertiesList);
        }

        private ODataEntry WriteEntity(Edm edm, string url, string entitySetName, IDictionary<string, object> data)
        {
            ODataClient client = new ODataClient(new System.Uri(url));

            System.Threading.Tasks.Task<IDictionary<string, object>> task = client.For(entitySetName).Set(data).InsertEntryAsync();
            WaitForTaskToComplete(task);

            Exception e = task.Exception;
            if (e != null)
            {
                if (e.InnerException is Simple.OData.Client.WebRequestException)
                {
                    // this means we got a response from the server with a specific message.
                    Simple.OData.Client.WebRequestException inner = (Simple.OData.Client.WebRequestException)e.InnerException;
                    NeotysAPIException napie = NeotysAPIException.Parse(inner.Response);
                    if (napie != null)
                    {
                        throw napie;
                    }
                    throw inner;

                }
                throw e;
            }

            IDictionary<string, object> result = task.Result;
            ODataEntry returnValue = new ODataEntry(result);

            return returnValue;
        }

        private void WriteFeed(Edm edm, string absolutUri, string entitySetName, IList<IDictionary<string, object>> dataList)
        {
            ODataClient client = new ODataClient(new System.Uri(url));
            ODataBatch batch = new ODataBatch(client);

            foreach (IDictionary<string, object> data in dataList)
            {
                batch += c => client.For("Entry").Set(data).InsertEntryAsync();
            }

            System.Threading.Tasks.Task task = batch.ExecuteAsync();
            WaitForTaskToComplete(task);

            Exception e = task.Exception;
            if (e != null)
            {
                if (e.InnerException is Simple.OData.Client.WebRequestException)
                {
                    // this means we got a response from the server with a specific message.
                    Simple.OData.Client.WebRequestException inner = (Simple.OData.Client.WebRequestException)e.InnerException;
                    throw new System.Exception(inner.Message + " : " + inner.Response);
                }
                throw e;
            }
        }

        private BinaryData WriteBinary(Edm edm, string url, string entitySetName, string apiKey, string jsonContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url + Separator + entitySetName));
            // TODO https?
            request.Method = methodPost;
            request.ContentType = applicationJson;

            Stream postStream = request.GetRequestStream();

            StringBuilder contentBuilder = new StringBuilder();
            contentBuilder.Append(" { \"d\": { ").Append(jsonContent);
            if (apiKey != null)
            {
                contentBuilder.Append(", \"ApiKey\": \"").Append(apiKey).Append("\"");
            }
            contentBuilder.Append("}}");

            byte[] postBytes = Encoding.UTF8.GetBytes(contentBuilder.ToString());
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Flush();
            postStream.Close();

            WebResponse response;
            try
            {
                response = request.GetResponse();
            }
            catch(WebException exception)
            {
                byte[] error = ReadFully(exception.Response.GetResponseStream());
                throw NeotysAPIException.Parse(Encoding.UTF8.GetString(error));
            }

            string fileName = ExtractFileNameFromContentDispositionHeader(response.Headers.Get("Content-Disposition"));
            return new BinaryData(fileName, ReadFully(response.GetResponseStream()));
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Extract the file name from the content disposition header:
        /// EX: Content-Disposition : form-data; name="file"; filename="test_example.txt" </summary>
        private static string ExtractFileNameFromContentDispositionHeader(string contentDispositionHeader)
        {
            if (contentDispositionHeader != null && contentDispositionHeader.Length != 0)
            {
                string pattern = "(?<=filename=\\\").*?(?=\\\")";
                MatchCollection matchCollection = Regex.Matches(contentDispositionHeader, pattern);
                if (matchCollection.Count > 0)
                {
                    return matchCollection[0].Groups[0].Value;
                }
            }
            return "";
        }

        private void WaitForTaskToComplete(Task task)
        {
            do
            {
                System.Threading.Thread.Sleep(200);
                if (System.Threading.Tasks.TaskStatus.RanToCompletion.Equals(task.Status) ||
                    System.Threading.Tasks.TaskStatus.Faulted.Equals(task.Status) ||
                    System.Threading.Tasks.TaskStatus.Canceled.Equals(task.Status))
                {
                    break;
                }

            } while (true);
        }


        private static System.IO.Stream Execute(string relativeUri)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] data = wc.DownloadData(relativeUri);

            return new System.IO.MemoryStream(data);
        }

        protected internal virtual bool Enabled
        {
            get
            {
                return enabled;
            }
        }

        protected internal virtual Edm Edm
        {
            get
            {
                return edm;
            }
        }
    }

}