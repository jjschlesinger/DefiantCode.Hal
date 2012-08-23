using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using DefiantCode.Hal.Core;

namespace DefiantCode.Hal.WebApi.Formatters.HalJson
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, object> AsDictionary(this IEnumerable<HalLink> links)
        {
            links = links ?? new HalLink[0];
            var dict = new Dictionary<string, object>();
            foreach (var l in links)
            {
                var linkObject = new Dictionary<string, object>();
                linkObject.Add("href", l.Href);
                if (l.Title != null)
                    linkObject.Add("title", l.Title);

                dict.Add(l.Name, linkObject);
            }

            return dict;
        }
    }
}
