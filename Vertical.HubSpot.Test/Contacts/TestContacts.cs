using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Vertical.HubSpot.Api;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Test.Test;
using Xunit;
using Xunit.Abstractions;

namespace Vertical.HubSpot.Test.Contacts
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
        public const string HubSpotTestContact_Email = "donald.duck@disney.com";
        public const long HubSpotTestContact_Id = 1149651;

        public TestContacts(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        protected Api.HubSpot GetHubSpot()
        {
            return new Api.HubSpot(new HubSpotOptions
            {
                ApiKey = Configuration["Hubspot:ApiKey"],
                Contact = new HubSpotContactOptions()
                {
                    IgnorePropertiesWithNullValues = true
                }
            });
        }

        // private async Task<TestHubSpotContact> FindContact(Api.HubSpot hubspot, string email)
        // {
        //     var contact = await hubspot.Contacts.Get<TestHubSpotContact>(email);
        //     Assert.True(contact != null, $"Contact with email {email} not found");
        //     return contact;
        // }

        private async Task<TestHubSpotContact> FindContact(Api.HubSpot hubspot, long id)
        {
            var contact = await hubspot.Contacts.Get<TestHubSpotContact>(id);
            Assert.True(contact != null, $"Contact with id {id} not found");
            return contact;
        }

        [Fact]
        public async Task CreateContact()
        {
            var hubspot = GetHubSpot();
            var contactId = await hubspot.Contacts.CreateOrUpdate(HubSpotTestContact_Email, new TestHubSpotContact
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
            var hubspot = GetHubSpot();
            var contact = await FindContact(hubspot, HubSpotTestContact_Id);

            await hubspot.Contacts.CreateOrUpdate(HubSpotTestContact_Email, new TestHubSpotContact
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
            var hubspot = GetHubSpot();
            var contact = await FindContact(hubspot, HubSpotTestContact_Id);
            Assert.NotNull(contact);

            await hubspot.Contacts.Delete(contact.ID);
            _outputHelper.WriteLine($"DeleteContact: id={contact.ID}");

        }

        [Fact]
        public async Task ListContacts()
        {
            var hubspot = GetHubSpot();

            // two pages...
            long? offset = null;
            bool hasMore;
            do
            {
                var response = await hubspot.Contacts.ListPage<TestHubSpotContact>(offset,2);
                offset = response.Offset;
                hasMore = response.HasMore ?? false;
            } while (hasMore);
        }

    }
}