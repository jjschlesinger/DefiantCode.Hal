using System;
using System.Collections.Generic;
using System.Linq;
using DefiantCode.Hal.Core;

namespace HalRestApiExample.Resources
{
    public class EmployeeSummaryResource
    {
        public List<HalLink> Links { get; set; }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }

        public EmployeeSummaryResource()
        {
            Links = new List<HalLink>();
        }

        public EmployeeSummaryResource(string selfHref)
            : this()
        {
            Links.Add(new HalLink(HalLinkTypes.Self, selfHref));
        }
    }
}