using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clouds.Storage.Core;
using Clouds.Common.Storage.Interfaces;

namespace Clouds.Storage.AWS.Tests
{
    [TestClass]
    public class DirectoryTests
    {
        AWSStorageProvider client = new AWSStorageProvider("AccessKey=AKIAIVZFGHQYW332IPOA;SecretKey=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;bucket=cloudsnet");

        [TestMethod]
        public void CreateDirectory()
        {
            client.CreateDirectory(null, Guid.NewGuid().ToString()).Wait();
        }

        [TestMethod]
        public void CreateSubDirectory()
        {
            // setup
            IStorageDirectory newDirectory = client.CreateDirectory(null, Guid.NewGuid().ToString()).Result;
            client.CreateDirectory(newDirectory, "sub").Wait();
        }

        [TestMethod]
        public void CreateExistingDirectory()
        {
            string name = Guid.NewGuid().ToString();

            client.CreateDirectory(null, name).Wait();
            client.CreateDirectory(null, name).Wait();
        }
    }
}
