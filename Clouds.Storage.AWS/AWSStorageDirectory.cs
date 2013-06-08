using Amazon.S3.IO;
using Clouds.Storage.Core;
using Clouds.Common.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Storage.AWS
{
    public class AWSStorageDirectory : IStorageDirectory
    {
        public S3DirectoryInfo DirectoryInfo { get; set; }

        public AWSStorageDirectory(S3DirectoryInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;
        }

        public Uri Uri
        {
            get { throw new NotImplementedException(); }
        }

        public IStorageDirectory GetParent()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IStorageDirectory> GetDirectories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IStorageFile> GetFiles()
        {
            throw new NotImplementedException();
        }


        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public IStorageDirectory GetRoot()
        {
            throw new NotImplementedException();
        }
    }
}
