using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DefiantCode.Hal.Core;
using Newtonsoft.Json;

namespace DefiantCode.Hal.WebApi.Formatters.HalJson
{
    public class HalJsonResourceCollection
    {
        [JsonProperty("_links")]
        public IDictionary<string, object> Links { get; private set; }
        [JsonProperty("_embedded")]
        public IDictionary<string, object> Embedded { get; private set; }

        private List<IDictionary<string, object>> _items = new List<IDictionary<string, object>>();

        public HalJsonResourceCollection(IEnumerable<HalLink> links, IEnumerable<object> items, PropertyInfo collectionProperty)
        {
            Embedded = new Dictionary<string, object>();
            string collectionName = null;
            foreach (var o in items)
            {
                var item = new Dictionary<string, object>();

                //var eo = AutoMapper.Mapper.DynamicMap<ExpandoObject>(o); 
                Type t = o.GetType();
                if (String.IsNullOrEmpty(collectionName))
                {
                    var collectionAttribute = collectionProperty.GetCustomAttributes(true)
                                                                .Where(x => x is HalEmbeddedResourceCollectionAttribute)
                                                                .Cast<HalEmbeddedResourceCollectionAttribute>()
                                                                .SingleOrDefault();
                    collectionName = collectionAttribute != null ? collectionAttribute.CollectionName : null;

                    if (String.IsNullOrEmpty(collectionName))
                        collectionName = collectionProperty.Name[0].ToString().ToLower() + collectionProperty.Name.Substring(1);
                }

                foreach (var p in t.GetProperties())
                {
                    string key = p.Name;
                    object value = p.GetValue(o);

                    if (value != null && value is IEnumerable<HalLink>)
                    {
                        var linkProps = value as IEnumerable<HalLink>;
                        if (linkProps.Count() > 0)
                        {
                            key = "_links";
                            value = linkProps.AsDictionary();
                            item.Add(key, value);
                        }

                    }
                    else
                        item.Add(key, value);

                    
                }

                _items.Add(item);
            }

            Embedded.Add(collectionName, _items);

            //convert list into dictionary for hal format
            Links = links != null ? links.AsDictionary() : null;
        }
    }
}