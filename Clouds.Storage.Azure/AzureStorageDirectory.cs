using System;
using System.Collections.Generic;
using System.Linq;
using Clouds.Common.Storage.Interfaces;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Clouds.Storage.Azure
{
    public class AzureStorageDirectory : IStorageDirectory
    {
        private readonly CloudBlobContainer _container;
        private readonly CloudBlobDirectory _directory;

        public AzureStorageDirectory(CloudBlobContainer container) : this(container, null)
        {
            throw new NotImplementedException();
        }

        public AzureStorageDirectory(CloudBlobContainer container, CloudBlobDirectory directory)
        {
            _container = container;
            _directory = directory;
            Uri = directory != null ? directory.Uri : container.Uri;
            Name = directory != null
                       ? directory.Uri.Segments[directory.Uri.Segments.Length - 1]
                       : container.Name;
        }

        public Uri Uri { get; private set; }
        public string Name { get; private set; }

        public IStorageDirectory GetParent()
        {
            if (_directory != null)
            {
                return new AzureStorageDirectory(_container, _directory.Parent);
            }

            return null;
        }

        public IStorageDirectory GetRoot()
        {
            return new AzureStorageDirectory(_container.ServiceClient.GetRootContainerReference());
        }

        public IEnumerable<IStorageDirectory> GetDirectories()
        {
            if (_directory != null)
                return
                    _directory.ListBlobs()
                              .OfType<CloudBlobDirectory>()
                              .Select(blob => new AzureStorageDirectory(_container, blob));

            return _container.ListBlobs()
                             .OfType<CloudBlobDirectory>()
                             .Select(blob => new AzureStorageDirectory(_container, blob));
        }

        public IEnumerable<IStorageFile> GetFiles()
        {
            if (_directory != null)
                return
                    _directory.ListBlobs()
                              .OfType<CloudBlockBlob>()
                              .Select(blob => new AzureStorageFile(_container, blob));

            return _container.ListBlobs()
                             .OfType<CloudBlockBlob>()
                             .Select(blob => new AzureStorageFile(_container, blob));
        }
    }
}