using AzureStorageExample._Api.Controllers.Upload.Helper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AzureStorageExample._Api.Controllers.Upload
{
    [RoutePrefix("api")]
    public class UploadController : ApiController
    {
        private const string Container = "test-blob";

        [HttpPost, Route("upload")]
        public async Task<IHttpActionResult> UploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            /* TODO: It is advisable to use a Web.config */
            var credentials = new
            {
                accountName = "xxx",
                accountKey = "xxx"
            };

            var storageAccount = new CloudStorageAccount(
                new StorageCredentials(credentials.accountName, credentials.accountKey), true
            );

            var blobClient = storageAccount.CreateCloudBlobClient();
            var imagesContainer = blobClient.GetContainerReference(Container);
            var provider = new AzureStorageMultipartFormDataStreamProvider(imagesContainer);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occured. Error: {ex.Message}");
            }


            var filename = provider.FileData.FirstOrDefault()?.LocalFileName;

            if (string.IsNullOrEmpty(filename))
                return BadRequest("An error has occured!");

            return Ok(new { url = provider.GetURL() });

        }
    }
}
