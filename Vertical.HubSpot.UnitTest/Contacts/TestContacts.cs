using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Vertical.HubSpot.Api;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.UnitTest.Test;
using Xunit;
using Xunit.Abstractions;

namespace Vertical.HubSpot.UnitTest.Contacts
{
    public class TestContacts : TestBase
    {
        private class TestHubSpotContact : HubSpotContact
        {
            [Name("email")]
            public string Email { get; set; }
            [Name("firstname")]
            public string FirstName { get; set; }
            [Name("lastname")]
            public string LastName { get; set; }
            [Name("country")]
            public string Country { get; set; }

            [IgnoreDataMember]
            public string TestIgnoreMember { get; set; } = "ignore this value";
        }

        private readonly ITestOutputHelper _outputHelper;

        public TestContacts(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task CreateContact()
        {
            var hubspotApi = GetHubSpotApi();
            var contactId = await hubspotApi.Contacts.CreateOrUpdate("donald.duck@disney.com", new TestHubSpotContact
            {
                FirstName = "Donald",
                LastName = "Duck",
                Country = "Disneyland",
            });
            _outputHelper.WriteLine($"CreateContact: id={contactId}");
        }

        [Fact]
        public async Task UpdateContact()
        {
            var contact = await FindContactByEmail<TestHubSpotContact>("donald.duck@disney.com");

            var options = new HubSpotOptions();
            options.Contact.IgnorePropertiesWithNullValues = true;
            var hubspotApi = GetHubSpotApi(options);
            await hubspotApi.Contacts.CreateOrUpdate("donald.duck@disney.com", new TestHubSpotContact
            {
                ID = contact.ID,
                FirstName = "Donald2",
                LastName = "Duck2",
                Country = "Disneyland2",
            });
            _outputHelper.WriteLine($"UpdateContact: id={contact.ID}");
            
        }

        [Fact]
        public async Task DeleteContact()
        {
            var contact = await FindContactByEmail<TestHubSpotContact>("donald.duck@disney.com");
            Assert.NotNull(contact);

            var hubspotApi = GetHubSpotApi();
            await hubspotApi.Contacts.Delete(contact.ID);
            _outputHelper.WriteLine($"DeleteContact: id={contact.ID}");

        }

    }
}