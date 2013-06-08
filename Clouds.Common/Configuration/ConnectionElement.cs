using System.Configuration;

namespace Clouds.Common.Configuration
{
    public class ConnectionElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return base["name"] as string; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("provider", IsRequired = true)]
        public CloudProvider Provider
        {
            get { return (CloudProvider) base["provider"]; }
            set { base["provider"] = value; }
        }

        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return base["connectionString"] as string; }
            set { base["connectionString"] = value; }
        }
    }
}