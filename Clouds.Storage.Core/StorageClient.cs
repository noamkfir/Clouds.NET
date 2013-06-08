using Clouds.Common.Configuration;
using Clouds.Common.Storage.Interfaces;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Clouds.Storage.Core
{
    public class StorageClient
    {

        private IStorageProvider _provider;

        public StorageClient(string connectionName)
        {
            var section = (CloudsDotNetSection)ConfigurationManager.GetSection("CloudsDotNet");
            var connection = (from c in section.Connections
                              where c.Name == connectionName
                              select c).FirstOrDefault();

            Init(connection.ConnectionString, connection.Provider);
        }

        public StorageClient(string connectionString, CloudProvider provider)
        {
            Init(connectionString, provider);
        }

        private void Init(string connectionString, CloudProvider provider)
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly().CodeBase);
            var container = new CompositionContainer(catalog);
            _provider = container.GetExportedValue<IStorageProvider>(provider.ToString());

        }

        //public Task<StorageDirectory> CreateDirectory(string path)
        //{

        //}

        public Task<IStorageDirectory> GetDirectory(Uri uri)
        {
            return _provider.GetDirectory(uri);
        }

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