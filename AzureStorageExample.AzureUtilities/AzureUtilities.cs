using AzureStorageExample.Blobs;
using AzureStorageExample.Queues;
using AzureStorageExample.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageExample
{
    public static class AzureUtilities
    {
        public static AzureBlob GetAzureBlob() => new AzureBlob();
        public static AzureQueues GetAzureQueue() => new AzureQueues();
        public static AzureTable GetAzureTable() => new AzureTable();
    }
}
