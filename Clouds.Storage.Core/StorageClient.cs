using Clouds.Common.Configuration;
using Clouds.Storage.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Storage.Core
{
    public class StorageClient
    {
        private IStorageProvider _provider;

        public StorageClient(string connectionName)
        {
 
        }

        public StorageClient(string connectionStrin, CloudProvider provider)
        {

        }

        //public Task<StorageDirectory> CreateDirectory(string path)
        //{

        //}

        public Task<IStorageDirectory> CreateDirectory(IStorageDirectory parent, string name)
        {
            return _provider.CreateDirectory(parent, name);
        }

        public Task CreateFile(IStorageDirectory directory, string name, Stream file)
        {
            return _provider.CreateFile(directory, name, file); 
        }

        //public Task CreateFile(string path, Stream file)
        //{

        //}
    }
}
