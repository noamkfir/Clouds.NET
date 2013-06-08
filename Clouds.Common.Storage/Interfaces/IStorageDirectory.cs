using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clouds.Common.Storage.Interfaces
{
    public interface IStorageDirectory
    {
        /// <summary>
        /// Gets the Uri of the Directory
        /// </summary>
        Uri Uri { get; }

        String Name { get; }

        IStorageDirectory GetParent();

        IStorageDirectory GetRoot();

        IEnumerable<IStorageDirectory> GetDirectories();

        IEnumerable<IStorageFile> GetFiles();
    }
}