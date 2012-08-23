using System;
using System.Collections.Generic;
using System.Linq;
using DefiantCode.Hal.Core;
using System.Xml.Linq;

namespace DefiantCode.Hal.WebApi.Formatters.HalXml
{
    public class HalXmlResource
    {
        public XDocument XmlDocument { get; private set; }

        public HalXmlResource(object obj)
        {
            Type t = obj.GetType();
            
            var xml = new XElement(t.Name);
            foreach (var p in t.GetProperties())
            {
                
                var customAttributes = p.GetCustomAttributes(true);
                if (p.GetValue(obj) is IEnumerable<HalLink> || customAttributes.Any(x => x is HalLinksAttribute))
                {
                    var links = p.GetValue(obj) as IEnumerable<HalLink>;
                    foreach (var link in links)
                    {
                        if (link.Name == HalLinkTypes.Self)
                            xml.Add(new XAttribute("href", link.Href));
                            
                    }
                    //Add("_links", ((IEnumerable<HalLink>)p.GetValue(obj)).AsDictionary());
                }
                else if (customAttributes.Any(x => x is HalEmbeddedResourceAttribute))
                {
                    //bool keyExists;
                    //var embedded = ProcessEmbeddedResource(p, obj, out keyExists);

                    //if (!keyExists)
                    //    Add("_embedded", embedded);
                    //else
                    //    this["_embedded"] = embedded;
                }
                else
                {
                    //Add(p.Name, p.GetValue(obj));
                }
            }

            XmlDocument = new XDocument(xml);
        }
    }
}
