using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clouds.Storage.Core;
using Clouds.Common.Storage.Interfaces;
using System.Collections.Generic;

namespace Clouds.Storage.AWS.Tests
{
    [TestClass]
    public class DirectoryTests
    {
        AWSStorageProvider _client;        

        [TestInitialize]
        public void Initialize()
        {
            _client = new AWSStorageProvider("AccessKey=AKIAIVZFGHQYW332IPOA;SecretKey=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;bucket=cloudsnet");
        }

        [TestMethod]
        public void CreateDirectory()
        {
            _client.CreateDirectory(null, Guid.NewGuid().ToString()).Wait();

            // Cleanup
        }

        [TestMethod]
        public void CreateSubDirectory()
        {
            // Setup
            IStorageDirectory newDirectory = _client.CreateDirectory(null, Guid.NewGuid().ToString()).Result;
            // Test
            IStorageDirectory subDirectory = _client.CreateDirectory(newDirectory, "sub").Result;

            Assert.AreEqual(subDirectory.GetParent().Name, newDirectory.Name);
            Assert.AreEqual(newDirectory.GetDirectories().Single().Name, "sub");            
            // Cleanup
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void CreateExistingDirectory()
        {
            string name = Guid.NewGuid().ToString();

            _client.CreateDirectory(null, name).Wait();
            try
            {
                _client.CreateDirectory(null, name).Wait();
            }
            finally
            {
                // Cleanup                
            }
        }

        [TestMethod]
        public void GetRootDirectory()
        {            
            IStorageDirectory directory = _client.GetDirectory(null).Result;
            
            Assert.AreEqual(directory.Name, "cloudsnet");
            // Cleanup
        }

        [TestMethod]
        public void GetFirstLevelSubDirectory()
        {
            string name = Guid.NewGuid().ToString();
            Uri uri = new Uri(name + "/", UriKind.Relative);

            _client.CreateDirectory(null, name).Wait();
            IStorageDirectory directory = _client.GetDirectory(uri).Result;

            Assert.AreEqual(directory.Uri.ToString(), uri.ToString());
            // Cleanup
        }

        [TestMethod]
        public void GetDeepSubDirectory()
        {
            string name = Guid.NewGuid().ToString();
            
            IStorageDirectory newDirectory = _client.CreateDirectory(null, name).Result;            
            _client.CreateDirectory(newDirectory, "sub").Wait();

            Uri uri = new Uri(name + "/sub/", UriKind.Relative);

            IStorageDirectory directory = _client.GetDirectory(uri).Result;                       

            Assert.AreEqual(directory.Uri.ToString(), uri.ToString());

            // Cleanup
        }
    }
}
