using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiantCode.Hal.Core
{
    public class HalEmbeddedResourceAttribute : Attribute
    {
        public string ResourceName { get; set; }

        public HalEmbeddedResourceAttribute(){}

        public HalEmbeddedResourceAttribute(string resourceName) 
        {
            ResourceName = resourceName;
        }

    }
}
