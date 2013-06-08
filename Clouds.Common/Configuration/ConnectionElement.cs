using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Common.Configuration
{
    public class ConnectionElement : ConfigurationElement
    {

        public CloudProvider Provider { get; set; }
        public string ConnectionString { get; set; }
        public string Name { get; set; }
    }
}
