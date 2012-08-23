using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;


namespace DefiantCode.Hal.WebApi.Formatters.HalXml
{
    public class HalXmlFormatter : MediaTypeFormatter
    {
        public HalXmlFormatter()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+xml"));
        }

        public HalXmlFormatter(IEnumerable<string> customMediaTypes)
            : this()
        {
            foreach (var mediaType in customMediaTypes)
            {
                SupportedMediaTypes.Add(new MediaTypeHeaderValue(mediaType + ".hal+xml"));
            }
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, System.Net.TransportContext transportContext)
        {
            if (type != null)
                if (stream != null)
                {
                    return Task.Factory.StartNew(() =>
                    {
                        using (var writer = System.Xml.XmlWriter.Create(stream))
                        {
                            var r = new HalXmlResource(value);
                            r.XmlDocument.WriteTo(writer);
                        }
                    });
                }
                else
                {
                    throw new System.ArgumentNullException("stream");
                }
            else
            {
                throw new System.ArgumentNullException("type");
            }
        }
    }
}
