using System;
using System.Collections.Generic;
using DefiantCode.Hal.Core;
using DefiantCode.Hal.WebApi.Formatters.HalJson;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefiantCode.Hal.Tests.UnitTests.WebApi.Formatters
{
    [TestClass]
    public class HalJsonResourceTests
    {
    
        [TestMethod]
        public void Constructer_WithLinks()
        {
            var obj = new AResource
            {
                Links = new List<HalLink> { new HalLink("self", "/path/2") },
                Property1 = "Test"
            };

            TestHalResourceLink(obj, obj.Links[0].Href);
        }

        private void TestHalResourceLink(object obj, string validHref)
        {
            var halResource = new HalJsonResource(obj);
            var links = halResource["_links"] as Dictionary<string, object>;
            var href = ((Dictionary<string, object>)links["self"])["href"].ToString();
            Assert.IsTrue(href == validHref);
        }
    }
    
    
    class AResource
    {
        public List<HalLink> Links { get; set; }

        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
        public string Property5 { get; set; }
        public string Property6 { get; set; }
        public string Property7 { get; set; }
        public string Property8 { get; set; }
        public string Property9 { get; set; }
        public string Property10 { get; set; }
        public string Property11 { get; set; }
        public string Property12 { get; set; }
    }
}
