using System;
using System.Collections.Generic;
using System.Linq;
using DefiantCode.Hal.Core;

namespace HalRestApiExample.Resources
{
    public class EmployeeDetailResource : BaseResource
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OfficeLocation { get; set; }

        public EmployeeDetailResource()
        {
            Links = new HashSet<HalLink>();
        }

        public EmployeeDetailResource(string selfHref) : this()
        {
            AddSelfLink(selfHref);
        }
    }
}