using System;
using System.Collections.Generic;
using System.Linq;
using DefiantCode.Hal.Core;

namespace HalRestApiExample.Resources
{
    [HalResourceCollection]
    public class EmployeeResultsResource
    {
        public List<HalLink> Links { get; set; }
        public IEnumerable<EmployeeSummaryResource> Employees { get; private set; }

        public EmployeeResultsResource()
        {
            Links = new List<HalLink>();
        }

        public EmployeeResultsResource(IEnumerable<EmployeeSummaryResource> employees, string selfHref, string nextHref = null, string prevHref = null)
        {
            Employees = employees;
            Links = new List<HalLink>();
            Links.Add(new HalLink(HalLinkTypes.Self, selfHref));
            
            if(!String.IsNullOrEmpty(nextHref))
                Links.Add(new HalLink(HalLinkTypes.Next, nextHref));
            if (!String.IsNullOrEmpty(prevHref))
                Links.Add(new HalLink(HalLinkTypes.Previous, prevHref));
        }

    }
}