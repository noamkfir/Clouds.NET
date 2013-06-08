using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.IO;
using Clouds.Storage.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clouds.Storage.AWS.Extensions;
using Clouds.Common.Configuration;
using Amazon.S3.Model;
using System.IO;

namespace Clouds.Storage.AWS
{
    public class AWSStorageProvider : IStorageProvider
    {
        const char ConnectionStringPartSeparator = ';';
        const char KeyValueSeparator = '=';
        static readonly string[] connectionStringParts = new string[] { "accesskey", "secretkey", "bucket" };
        AmazonS3 _client;
        S3DirectoryInfo _rootDirectory;

        public AWSStorageProvider(string connectionString)
        {
            // Parse the connection string to the access key and secret key
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Connection string cannot be empty");
            else
            {
                // Split the connection string parts (make keys lower-cased for comparison)
                var parts = connectionString.ToDictionary(KeyValueSeparator, ConnectionStringPartSeparator, makeKeysLowercased: true);
                // Check if the connection string contains all required parts
                if (parts == null || connectionStringParts.Except(parts.Keys).Count() > 0)
                {
                    throw new ArgumentException("Connection string must include the access key, secret key, and bucket of your AWS account");
                }
                else
                {
                    var credentials = new BasicAWSCredentials(parts["accesskey"], parts["secretkey"]);
                    _client = Amazon.AWSClientFactory.CreateAmazonS3Client(credentials);
                    _rootDirectory = new S3DirectoryInfo(_client, parts["bucket"]);
                    //_client = Amazon.AWSClientFactory.CreateAmazonS3Client(Amazon.RegionEndpoint.USWest2);
                }
            }
        }

        public async Task<IStorageDirectory> CreateDirectory(IStorageDirectory parent, string name)
        {
            AWSStorageDirectory newDirectory = null;

            // Verify the directory name ends with a / (specifies its a directory in AWS)
            

            S3DirectoryInfo parentDirectory;
            if (parent == null)
            {
                parentDirectory = _rootDirectory;
            }
            else
            {
                parentDirectory = ((AWSStorageDirectory)parent).DirectoryInfo;
            }

            S3DirectoryInfo directory = parentDirectory.GetDirectory(name);
            // Get the full path of the directory, without the bucket name at the beginning, and the tracking :/
            string fullPath = directory.FullName.Remove(0, directory.Bucket.Name.Length+2);
            fullPath = fullPath.Replace('\\', '/');

            // Check if directory already exists 
            if (!directory.Exists)
            {
                var request = new PutObjectRequest { BucketName = _rootDirectory.Name, Key = fullPath, InputStream = new MemoryStream() };
                PutObjectResponse response = await Task.Factory.FromAsync(
                                                _client.BeginPutObject(request, null, null),
                                                (result) => _client.EndPutObject(result));

                newDirectory = directory.ToStorageDirectory();
            }
            else
            {
                throw new Exception("Directory already exists");
            }

            return newDirectory;
        }

        public Task CreateFile(IStorageDirectory directory, string name, Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
