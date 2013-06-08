using Amazon.S3.IO;
using Clouds.Common.Storage.Interfaces;
using Clouds.Storage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clouds.Storage.AWS.Extensions;

namespace Clouds.Storage.AWS
{
    public class AWSStorageDirectory : IStorageDirectory
    {
        public S3DirectoryInfo DirectoryInfo { get; set; }

        public AWSStorageDirectory(S3DirectoryInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;
            this.Uri = new Uri(directoryInfo.GetDirectoryPath(), UriKind.Relative);
        }

        public Uri Uri { get; private set; }

        public IStorageDirectory GetParent()
        {
            if (DirectoryInfo.Parent.Name == DirectoryInfo.Root.Name)
                return null;
            else
                return DirectoryInfo.Parent.ToStorageDirectory();
        }

        public IEnumerable<IStorageDirectory> GetDirectories()
        {
            return DirectoryInfo.GetDirectories().Select(s3dir => s3dir.ToStorageDirectory());
        }

        public IEnumerable<IStorageFile> GetFiles()
        {
            return DirectoryInfo.GetFiles().Select(s3file => s3file.ToStorageFile());
        }


        public string Name
        {
            get { return DirectoryInfo.Name; }
        }

        public IStorageDirectory GetRoot()
        {
            return DirectoryInfo.Bucket.ToStorageDirectory();
        }
    }
}
