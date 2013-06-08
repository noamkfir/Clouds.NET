using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Common.Storage.Interfaces
{
    public interface IStorageProvider
    {
        Task<IStorageDirectory> GetDirectory(Uri uri);

        Task<IStorageDirectory> CreateDirectory(IStorageDirectory parent, string name);

        Task CreateFile(IStorageDirectory directory, string name, Stream file);


    }
}
