using Amazon.S3.IO;
using Clouds.Storage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Storage.AWS.Extensions
{
    static public class StorageDirectoryExtensions
    {
        static public AWSStorageDirectory ToStorageDirectory(this S3DirectoryInfo directoryInfo)
        {
            AWSStorageDirectory targetDirectory = new AWSStorageDirectory(directoryInfo);            
            
            // Copy properties
            return targetDirectory;
        }

        static public string GetDirectoryPath(this S3DirectoryInfo directoryInfo)
        {
            return directoryInfo.FullName.Remove(0, directoryInfo.Bucket.Name.Length + 2).Replace("\\", "/");
        }

        static public string AsKey(this Uri objectUri)
        {
            return objectUri.ToString().Replace("/", "\\");            
        }
    }
}
