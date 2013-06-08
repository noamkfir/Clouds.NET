using System;
using System.IO;
using System.Threading.Tasks;
using Clouds.Common.Storage.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.ComponentModel.Composition;
using Clouds.Common.Configuration;

namespace Clouds.Storage.Azure
{
    [Export("Azure", typeof(IStorageProvider))]
    public class AzureStorageProvider : IStorageProvider
    {
        private readonly CloudStorageAccount _account;
        private readonly IStorageDirectory _rootDir;

        [ImportingConstructor]
        public AzureStorageProvider(string connectionString)
        {
            _account = CloudStorageAccount.Parse(connectionString);

            var client = _account.CreateCloudBlobClient();

            _rootDir = new AzureStorageDirectory(client.GetRootContainerReference());
        }

        public Task<IStorageDirectory> GetDirectory(Uri uri)
        {
            throw new NotImplementedException();
        }

        public async Task<IStorageDirectory> CreateDirectory(IStorageDirectory parent, string name)
        {
            var newName = name + "/";

            await CreateFile(parent, newName, new MemoryStream(0));
            return await GetDirectory(new Uri(parent.Uri + newName));
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

                CloudBlobDirectory d;

                CloudBlockBlob blob = container.GetBlockBlobReference(name);

                using (file)
                {
                    await Task.Factory.FromAsync(blob.BeginUploadFromStream, blob.EndUploadFromStream, file, null);
                }
            }
        }
    }
}