using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiantCode.Hal.Core
{
    public class HalEmbeddedResourceCollectionAttribute : Attribute
    {
        public string CollectionName { get; set; }

        public HalEmbeddedResourceCollectionAttribute()
        {
        	
        }

        public HalEmbeddedResourceCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
