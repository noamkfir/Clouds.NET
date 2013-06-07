using System;
using System.IO;
using System.Threading.Tasks;
using Clouds.Storage.Core.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Clouds.Storage.Azure
{
    public class AzureStorageProvider : IStorageProvider
    {
        private readonly CloudStorageAccount _account;

        public AzureStorageProvider(string connectionString)
        {
            _account = CloudStorageAccount.Parse(connectionString);
        }

        public Task<IStorageDirectory> CreateDirectory(IStorageDirectory parent, string name)
        {
            throw new NotImplementedException();
        }

        public async Task CreateFile(IStorageDirectory directory, string name, Stream file)
        {
            CloudBlobClient blobClient = _account.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(directory.GetRoot().Name);

            if (!await Task<bool>.Factory.FromAsync(container.BeginExists, container.EndExists, null))
            {
                await Task.Factory.FromAsync(container.BeginSetPermissions, container.EndSetPermissions,
                                             new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob},
                                             null);

                CloudBlockBlob blob = container.GetBlockBlobReference(name);

                using (file)
                {
                    await Task.Factory.FromAsync(blob.BeginUploadFromStream, blob.EndUploadFromStream, file, null);
                }
            }
        }
    }
}