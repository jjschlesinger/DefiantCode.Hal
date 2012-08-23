using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DefiantCode.Hal.Core;
using HalRestApiExample.Resources;

namespace HalRestApiExample.Controllers
{
    
    public class EmployeesController : ApiController
    {
        public EmployeeResultsResource Get()
        {
            var employees = new List<EmployeeSummaryResource>();
            employees.Add(new EmployeeSummaryResource(Request.RequestUri.AbsoluteUri + "/ABC123") { Id = "ABC123", FullName = "A Gorilla", Email = "agorilla@example.com", Title = "Alpha Male" });

            var results = new EmployeeResultsResource(employees, Request.RequestUri.AbsoluteUri);
            return results;
        }

        public EmployeeDetailResource Get(string id)
        {
            var employee = new EmployeeDetailResource(Request.RequestUri.AbsoluteUri) { Id = "ABC123", FirstName = "A", LastName = "Gorilla", Email = "agorilla@example.com", Title = "Alpha Male", Phone = "555-123-4567", OfficeLocation = "Chicago, IL" };

            return employee;
        }
    }
}
