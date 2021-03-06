using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Projeto_Rumos.ApiConector
{
    public class ApiConnector
    {
        private string Url { get; set; }
        public ApiConnector()
        {
        
            Url = "https://webapifrutaria.azurewebsites.net/api/v1/";
            //Url = "https://localhost:44300/api/v1/";
        }

        public ApiConnector(string url)
        {
            Url = url;
        }

        public string Get(string ControllerName)
        {
            var requisicaoWeb = WebRequest.CreateHttp(Url + ControllerName);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.Timeout = 12000;
            requisicaoWeb.ContentType = "application/json";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                string objResponse = reader.ReadToEnd();
                return objResponse;
            }
        }

        public string GetById(string ControllerName, int id)
        {

            var requisicaoWeb = WebRequest.CreateHttp(Url + ControllerName + "/" + id);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.Timeout = 12000;
            requisicaoWeb.ContentType = "application/json";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamDados);
                string ObjResponse = streamReader.ReadToEnd();
                return ObjResponse;
            }
        }

        public string Post(string ControllerName, string data)
        {
            try
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsync(Url + ControllerName, content).GetAwaiter().GetResult();
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return result;
            }
            catch (Exception msg)
            {
                return msg.Message;
            }
        }

        public string Update(string ControllerName, string value)
        {
            try
            {
                var content = new StringContent(value, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = httpClient.PutAsync(Url + ControllerName, content).GetAwaiter().GetResult();
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return result;
            }
            catch (Exception msg)
            {
                return msg.Message;
            }
        }

        public string Delete(string ControllerName, int id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.DeleteAsync(Url + ControllerName + "/" + id).GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return result;
        }

        public string SaveImageAzureBlob(string file, string controllerName, string route)
        {
            var convert = new StringContent(file, Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();  
            HttpResponseMessage response = httpClient.PostAsync(Url + controllerName + "/" + route, convert).GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return result;

        }
    }
}
