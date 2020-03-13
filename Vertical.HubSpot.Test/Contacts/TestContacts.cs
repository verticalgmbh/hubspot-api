using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
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

        protected Api.HubSpot GetHubSpot()
        {
            return new Api.HubSpot(Configuration["Hubspot:ApiKey"]);
        }

        [Fact]
        public async Task CreateContact()
        {
            var hubspot = GetHubSpot();
            var contactId = await hubspot.Contacts.CreateOrUpdate("donald.duck@disney.com", new TestHubSpotContact
            {
                FirstName = "Donald",
                LastName = "Duck",
                Country = "Disneyland",
            });
            _outputHelper.WriteLine($"CreateContact: id={contactId}");
        }

        private async Task<TestHubSpotContact> FindContact(string email)
        {
            var hubspot = new Api.HubSpot(Configuration["Hubspot:ApiKey"]);
            var contact = await hubspot.Contacts.Get<TestHubSpotContact>(email);
            Assert.True(contact != null, "Contact not found");
            return contact;
        }

        [Fact]
        public async Task UpdateContact()
        {
            var contact = await FindContact("donald.duck@disney.com");

            var hubspot = GetHubSpot();
            await hubspot.Contacts.CreateOrUpdate("donald.duck@disney.com", new TestHubSpotContact
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
            var contact = await FindContact("donald.duck@disney.com");
            Assert.NotNull(contact);

            var hubspot = GetHubSpot();
            await hubspot.Contacts.Delete(contact.ID);
            _outputHelper.WriteLine($"DeleteContact: id={contact.ID}");

        }

    }
}