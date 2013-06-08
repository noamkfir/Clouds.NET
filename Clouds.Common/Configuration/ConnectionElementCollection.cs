using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Common.Configuration
{
    public class ConnectionElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConnectionElement)element).Name;
        }

        public new ConnectionElement this[string name]
        {
            get
            {
                return (ConnectionElement)this.BaseGet(name);
            }
            // set
            //{
            //    if (BaseGet(name) != null)
            //    {
            //        BaseRemoveAt(name);
            //    }
            //    BaseAdd(name, value);
            //}
        }

    }
}
