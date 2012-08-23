using System;
using System.Collections.Generic;
using System.Linq;
using DefiantCode.Hal.Core;

namespace HalRestApiExample.Resources
{
    [HalResource]
    public class EmployeeDetailResource
    {
        public List<HalLink> Links { get; set; }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OfficeLocation { get; set; }

        public EmployeeDetailResource()
        {
            Links = new List<HalLink>();
        }

        public EmployeeDetailResource(string selfHref)
            : this()
        {
            Links.Add(new HalLink(HalLinkTypes.Self, selfHref));
        }
    }
}