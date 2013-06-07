using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clouds.Storage.Core.Interfaces
{
    public interface IStorageDirectory
    {
        /// <summary>
        /// Gets the Uri of the Directory
        /// </summary>
        Uri Uri { get; }

        IStorageDirectory GetParent();

        IEnumerable<IStorageDirectory> GetDirectories();

        IEnumerable<IStorageFile> GetFiles();
    }
}