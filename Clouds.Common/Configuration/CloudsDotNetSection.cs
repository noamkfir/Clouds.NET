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
        [ConfigurationProperty("Connection")]
        public ConnectionElement Connection
        {
            get
            {
                return (ConnectionElement)this["Connection"];
            }
            set
            { 
                this["Connection"] = value; 
            }
        }
    }
}
