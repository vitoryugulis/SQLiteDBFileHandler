using Core.Models;
using Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FileHandlerService
    {
        public readonly HttpClient client = new HttpClient();

        public async Task<string> PostFileToAPIAsMultipartForm(string filePath, string apiUrl)
        {
            var form = new MultipartFormDataContent();
            var byteArray = File.ReadAllBytes(filePath);
            var content = new ByteArrayContent(byteArray);
            form.Add(content, "file", "transaction2.db");
            var response = await client.PostAsync(apiUrl, content);
            client.Dispose();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostFileToAPIAsBase64(string filePath, string apiUrl)
        {
            var base64 = FileHandler.FileToBase64(filePath);
            var file = new DatabaseFile { base64file = base64 };
            var jsonBody = JsonConvert.SerializeObject(file);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            client.Dispose();
            return await response.Content.ReadAsStringAsync();
        }

        public bool WriteBase64File(string filePath, string base64)
        {
            var result = FileHandler.Base64ToFile(base64, filePath + "transaction3.db");
            return result;
        }
        
    }
}
