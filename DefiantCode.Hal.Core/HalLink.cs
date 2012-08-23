using System;
using System.Linq;

namespace DefiantCode.Hal.Core
{
    public class HalLink
    {
        public HalLink()
        {
        }

        public HalLink(string name, string href, string title = null, bool templated = false)
        {
            Name = name;
            Href = href;
            Title = title;
            Templated = templated;
        }

        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Href { get; private set; }
        public bool Templated { get; private set; }
    }
}