using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace AzureStorageExample._Api.Controllers.Upload.Helper
{
    public class AzureStorageMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private readonly CloudBlobContainer _blobContainer;
        public static string url;

        public AzureStorageMultipartFormDataStreamProvider(CloudBlobContainer blobContainer) : base("azure") => _blobContainer = blobContainer;
        public String GetURL() => url;
        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent is null) throw new ArgumentNullException(nameof(parent));
            if (headers is null) throw new ArgumentNullException(nameof(headers));

            var fileName = Guid.NewGuid().ToString();

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(fileName);
            url = blob.Uri.AbsoluteUri;

            if (!(headers.ContentType is null))
                blob.Properties.ContentType = headers.ContentType.MediaType;

            FileData.Add(new MultipartFileData(headers, blob.Name));

            return blob.OpenWrite();
        }
    }
}