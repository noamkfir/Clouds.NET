using Clouds.Common.Storage.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Storage.AWS.Tests
{
    [TestClass]
    public class FileTests
    {
        AWSStorageProvider client;

        [TestInitialize]
        public void Initialize()
        {
            client = new AWSStorageProvider("AccessKey=AKIAIVZFGHQYW332IPOA;SecretKey=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;bucket=cloudsnet");
        }

        [TestMethod]
        public void CreateFileInRoot()
        {
            IStorageDirectory directory = client.GetDirectory(null).Result;
            MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes("this is a test"));
            // Create a new random named file
            client.CreateFile(directory, Guid.NewGuid().ToString(), ms).Wait();

            Assert.AreEqual(directory.GetFiles().Count(), 1);
        }

        [TestMethod]
        public void CreateFileInSubDir()
        {
            IStorageDirectory directory = client.CreateDirectory(null, Guid.NewGuid().ToString()).Result;
            MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes("this is a test"));
            client.CreateFile(directory, Guid.NewGuid().ToString(), ms).Wait();

            Assert.AreEqual(directory.GetFiles().Count(), 1);
        }

    }
}
