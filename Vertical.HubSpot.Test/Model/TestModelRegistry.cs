using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Models;
using Xunit;

namespace Vertical.HubSpot.Test.Model
{
    public class TestModelRegistry
    {
        private class TestJsonProperties
        {
            [JsonProperty("name2")]
            public string Name1 { get; set; }

            public string NameA { get; set; }

            [JsonIgnore]
            public string Ignore { get; set; }
        }

        private class TestInternalProperties
        {
            [Name("name2")]
            public string Name1 { get; set; }

            public string NameA { get; set; }

            [IgnoreDataMember]
            public string Ignore { get; set; }
        }


        [Theory]
        [InlineData(typeof(TestInternalProperties))]
        [InlineData(typeof(TestJsonProperties))]
        public void TestNamedAndIgnoredProerties(Type type)
        {
            var modelRegistry = new ModelRegistry();
            var properties = modelRegistry.Get(type);

            var name2 = properties.GetMapping("Name1");
            Assert.True(name2 == "name2");

            var nameA = properties.GetMapping("NameA");
            Assert.True(nameA == "namea");

            var ignore = properties.GetMapping("Ignore");
            Assert.Null(ignore);
        }
    }
}