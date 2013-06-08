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
        [ConfigurationProperty("connections")]
        public ConnectionElementCollection Connections
        {
            get
            {
                return (ConnectionElementCollection)this["connections"];
            }
            set
            { 
                this["connections"] = value; 
            }
        }
    }
}
