using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DefiantCode.Hal.Core;

namespace DefiantCode.Hal.WebApi.Formatters.HalJson
{
    public class HalJsonResource : Dictionary<string, object>
    {
        public HalJsonResource(object obj)
        {
            LoadObject(obj);
        }

        private void LoadObject(object obj)
        {
            Type t = obj.GetType();
            foreach (var p in t.GetProperties())
            {
                var propValue = p.GetValue(obj);
                var customAttributes = p.GetCustomAttributes(true);
                if (customAttributes.Any(x => x is HalLinksAttribute) || propValue is IEnumerable<HalLink>)
                {
                    if (propValue != null)
                        Add("_links", ((IEnumerable<HalLink>)propValue).AsDictionary());
                }
                else if (customAttributes.Any(x => x is HalEmbeddedResourceAttribute))
                {
                    bool keyExists;
                    var embedded = ProcessEmbeddedResource(p.Name, propValue, obj, out keyExists);

                    if (!keyExists)
                        Add("_embedded", embedded);
                    else
                        this["_embedded"] = embedded;
                }
                else
                {
                    Add(p.Name, propValue);
                }
            }
        }

        private Dictionary<string, object> ProcessEmbeddedResource(string propName, object propValue, object obj, out bool keyExists)
        {
            object val;
            Dictionary<string, object> embedded;
            keyExists = TryGetValue("_embedded", out val);
            if (keyExists)
                embedded = val as Dictionary<string, object>;
            else
                embedded = new Dictionary<string, object>();

            embedded.Add(propName, ObjectToDictionary(propValue));
            return embedded;
        }

        private static Dictionary<string, object> ObjectToDictionary(object obj)
        {
            var dict = new Dictionary<string, object>();
            Type t = obj.GetType();
            foreach (var p in t.GetProperties())
            {
                var customAttributes = p.GetCustomAttributes(true);
                if (p.GetValue(obj) is IEnumerable<HalLink> || customAttributes.Any(x => x is HalLinksAttribute))
                {
                    dict.Add("_links", ((IEnumerable<HalLink>)p.GetValue(obj)).AsDictionary());
                }
                else
                {
                    dict.Add(p.Name, p.GetValue(obj));
                }
            }

            return dict;
        }
    }
}
