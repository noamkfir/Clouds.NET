using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Storage.Core.Interfaces
{
    public interface IStorageProvider
    {
        Task<IStorageDirectory> CreateDirectory(IStorageDirectory parent, string name);

        Task CreateFile(IStorageDirectory directory, string name, Stream file);
    }
}
