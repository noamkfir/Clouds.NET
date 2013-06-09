using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clouds.Common.Storage.Interfaces;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Clouds.Storage.Azure
{
    public class AzureStorageFile : IStorageFile
    {
        private CloudBlockBlob _file;
        private CloudBlobContainer _container;

        public AzureStorageFile(CloudBlobContainer container, CloudBlockBlob file)
        {
            _container = container;
            _file = file;
        }
    }
}
