using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clouds.Common.Configuration
{
    public static class StringExtension
    {
        public static IDictionary<string, string> ToDictionary(this string s, char valueDelim, char pairDelim, bool makeKeysLowercased=false)
        {
            var segments = s.Split(new char[] { pairDelim }, StringSplitOptions.RemoveEmptyEntries);
            var entries = segments.Select(item => item.Split(new char[] { valueDelim }, StringSplitOptions.RemoveEmptyEntries));
            var kvps = entries.Select(kvp => new KeyValuePair<string, string>(kvp[0], kvp.Length > 1 ? kvp[1] : string.Empty));
            return kvps.ToDictionary(k => makeKeysLowercased ? k.Key.ToLower() : k.Key, v => v.Value);
        }
    }

}
