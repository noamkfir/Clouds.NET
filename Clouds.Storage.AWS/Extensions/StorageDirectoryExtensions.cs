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
    }
}
