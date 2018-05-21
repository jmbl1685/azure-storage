using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageExample.Blobs
{
    public class AzureBlob
    {
        /* Credentials to Azure Storage Emulator. 
             TODO: It is advisable to use a Web.config */
        private const string StorageAccountName = "xxx";
        private const string StorageAccountKey = "xxx";
        private const string FolderPath = "C:\\img";

        #region GetStorageAccount() 
        public CloudStorageAccount GetStorageAccount()
        {
            var StorageCredentials = new StorageCredentials(StorageAccountName, StorageAccountKey);
            return new CloudStorageAccount(StorageCredentials, true);
        }
        #endregion

        #region GetBlobContainer() 
        public CloudBlobContainer GetBlobContainer(string name)
        {
            CloudBlobContainer container = null;

            try
            {
                var blobClient = GetStorageAccount().CreateCloudBlobClient();
                container = blobClient.GetContainerReference(name);

                container.CreateIfNotExists();

                container.SetPermissions(new BlobContainerPermissions()
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            }
            catch (Exception e)
            {
                // TODO: Add method to save exception [message]
                return container;
            }
            return container;
        }
        #endregion

        #region GetBlobClient() 
        public CloudBlobClient GetBlobClient(CloudStorageAccount storageAccount)
            => storageAccount.CreateCloudBlobClient();
        #endregion

        public string InsertFilesTest()
        {
            string blobUrl = null;

            try
            {
                foreach (var filePath in Directory.GetFiles(FolderPath, "*.*"))
                {
                    var fileName = filePath.Split('\\');
                    var blob = GetBlobContainer("test-blob").GetBlockBlobReference(fileName[fileName.Length - 1].ToString());
                    blob.UploadFromFile(filePath);
                    blobUrl = blob.Uri.AbsoluteUri;
                }
            }
            catch (Exception e)
            {
                // TODO: Add method to save exception [message]
                //throw e;
                return blobUrl;
            }
            return blobUrl;
        }

    }
}
