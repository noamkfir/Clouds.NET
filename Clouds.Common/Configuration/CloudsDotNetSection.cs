using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Common.Configuration
{
    public class CloudsDotNetSection : ConfigurationSection
    {
        [ConfigurationProperty("Connections")]
        public ConnectionElement[] Connections
        {
            get
            {
                return (ConnectionElement[])this["Connections"];
            }
            set
            { 
                this["Connections"] = value; 
            }
        }
    }
}
