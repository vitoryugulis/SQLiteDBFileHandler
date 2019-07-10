using API.Models;
using Core;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class FileHandlerController : ApiController
    {
        [Route("files/sendbase64")]
        [HttpGet]
        public async Task<IHttpActionResult> SendFileAsBase64()
        {
            var apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            var fileToReadPath = ConfigurationManager.AppSettings["fileToReadPath"];
            var service = new FileHandlerService();
            var response = await service.PostFileToAPIAsBase64(fileToReadPath, apiUrl + "files/receivebase64");
            return Ok(response);
        }

        [Route("files/receivebase64")]
        [HttpPost]
        public IHttpActionResult SaveBase64FileToDatabase(FileModel file)
        {
            var fileToWritePath = ConfigurationManager.AppSettings["fileToWritePath"];
            var service = new FileHandlerService();
            service.WriteBase64File(fileToWritePath, file.base64file);
            return Ok();
        }


        [Route("files/sendmultipartform")]
        [HttpGet]
        public async Task<IHttpActionResult> SendFileAsMultipartForm()
        {
            var apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            var fileToReadPath = ConfigurationManager.AppSettings["fileToReadPath"];
            var service = new FileHandlerService();
            var response = await service.PostFileToAPIAsMultipartForm(fileToReadPath, apiUrl + "files/receivemultipartform");
            return Ok(response);
        }

        [Route("files/receivemultipartform")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveMultipartFormFileToDatabase()
        {
            var fileToWritePath = ConfigurationManager.AppSettings["fileToWritePath"];
            var content = await Request.Content.ReadAsByteArrayAsync();

            var service = new FileHandlerService();
            File.WriteAllBytes(fileToWritePath + "transaction2.db", content);
            return Ok();
        }
    }
}