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
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name { get; set; }

        [ConfigurationProperty("provider", IsRequired = true)]
        public CloudProvider Provider { get; set; }

        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString { get; set; }

    }
}
