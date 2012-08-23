using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using DefiantCode.Hal.Core;
using DefiantCode.Hal.WebApi.Formatters;

namespace DefiantCode.Hal.WebApi.Formatters.HalJson
{
    public class HalJsonFormatter : JsonMediaTypeFormatter
    {
        public HalJsonFormatter()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+json"));
        }

        public HalJsonFormatter(IEnumerable<string> customMediaTypes) : this()
        {
            foreach (var mediaType in customMediaTypes)
            {
                SupportedMediaTypes.Add(new MediaTypeHeaderValue(mediaType + ".hal+json"));
            }
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, System.Net.TransportContext transportContext)
        {
            var attributes = value.GetType().GetCustomAttributes(true);
            if (attributes.Any(x => x is HalResourceCollectionAttribute || x is HalResourceAttribute))
            {
                IEnumerable<HalLink> halLinks = null;
                var lpName = "Links";
                var lp = value.GetType().GetProperties().Where(x => x.GetCustomAttributes(true).Any(x2 => x2 is HalLinksAttribute)).FirstOrDefault();
                if (lp == null)
                    lp = value.GetType().GetProperty(lpName);

                if (lp != null)
                    halLinks = lp.GetValue(value) as IEnumerable<HalLink>;

                lpName = lp != null ? lp.Name : lpName;

                if (attributes.Any(x => x is HalResourceCollectionAttribute))
                {
                    PropertyInfo collectionProperty;
                    collectionProperty = value.GetType().GetProperties().Where(x => x.GetCustomAttributes(true).Any(x2 => x2 is HalEmbeddedResourceCollectionAttribute)).FirstOrDefault();
                    if (collectionProperty == null)
                    {
                        //find first property that is IEnumerable to use as the collection (skip Links)
                        collectionProperty = value.GetType().GetProperties().Where(x => x.Name != lpName && x.PropertyType.GetInterface("IEnumerable") != null).FirstOrDefault();
                        if (collectionProperty == null)
                            throw new Exception("couldn't find a usable property for the resource collection");
                    }
                    var collection = collectionProperty.GetValue(value) as IEnumerable<object>;
                    var results = new HalJsonResourceCollection(halLinks, collection, collectionProperty);
                    return base.WriteToStreamAsync(results.GetType(), results, stream, contentHeaders, transportContext);
                }
                else
                {
                    var result = new HalJsonResource(value);
                    return base.WriteToStreamAsync(result.GetType(), result, stream, contentHeaders, transportContext);
                }
            }
            else if (value.GetType().IsArray)
            {
                var values = value as object[];
                var items = new HalJsonResource[values.Length];
                for (int i = 0; i < items.Length; i++)
                {
                    IEnumerable<HalLink> halLinks = null;

                    var lp = value.GetType().GetProperty("Links");
                    if (lp != null)
                    {
                        halLinks = lp.GetValue(value) as IEnumerable<HalLink>;
                    }

                    items[i] = new HalJsonResource(values[i]);
                }

                return base.WriteToStreamAsync(items.GetType(), items, stream, contentHeaders, transportContext);
            }

            return base.WriteToStreamAsync(type, value, stream, contentHeaders, transportContext);
        }

        public override System.Threading.Tasks.Task<object> ReadFromStreamAsync(Type type, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, System.Net.Http.Formatting.IFormatterLogger formatterLogger)
        {
            return base.ReadFromStreamAsync(type, stream, contentHeaders, formatterLogger);
        }

        /// <summary> Gets the default media type for HalJson, namely "application/hal+json". </summary>
        /// <returns>
        /// <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue"/>
        /// </returns>
        public static new System.Net.Http.Headers.MediaTypeHeaderValue DefaultMediaType
        {
            get
            {
                return new System.Net.Http.Headers.MediaTypeHeaderValue("application/hal+json");
            }
        }
    }
}