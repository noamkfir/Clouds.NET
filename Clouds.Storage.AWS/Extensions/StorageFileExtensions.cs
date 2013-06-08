using Amazon.S3.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Storage.AWS.Extensions
{
    static public class StorageFileExtensions
    {
        static public AWSStorageFile ToStorageFile(this S3FileInfo fileInfo)
        {
            AWSStorageFile targetFile = new AWSStorageFile(fileInfo);

            // Copy properties
            return targetFile;
        }

    }
}
