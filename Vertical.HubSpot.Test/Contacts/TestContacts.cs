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

        public TestContacts(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        // private async Task<TestHubSpotContact> FindContact(Api.HubSpot hubspot, string email)
        // {
        //     var contact = await hubspot.Contacts.Get<TestHubSpotContact>(email);
        //     Assert.True(contact != null, $"Contact with email {email} not found");
        //     return contact;
        // }

        private async Task<TestHubSpotContact> GetContact(long id)
        {
            var hubspot = GetHubSpot();
            var contact = await hubspot.Contacts.Get<TestHubSpotContact>(id);
            Assert.True(contact != null, $"Contact with id {id} not found");
            return contact;
        }

        [Theory]
        [InlineData(HubSpotTestContact_Email,true)]
        public async Task<long> CreateContact(string email, bool deleteAftewards)
        {
            var hubspot = GetHubSpot();
            var contactId = await hubspot.Contacts.CreateOrUpdate(email, new TestHubSpotContact
            {
                FirstName = "Donald",
                LastName = "Duck",
                Country = "Disneyland",
            });
            _outputHelper.WriteLine($"CreateContact: id={contactId}");
            if (deleteAftewards)
                await DeleteContact(contactId);
            return contactId;
        }

        [Fact]
        public async Task CreateContactWithoutEmail()
        {
            var hubspot = GetHubSpot();
            var contactId = await hubspot.Contacts.CreateOrUpdate(new TestHubSpotContact
            {
                FirstName = "Mickey",
                LastName = "Mouse",
                Country = "Disneyland",
            });
            _outputHelper.WriteLine($"CreateContact: id={contactId}");

            await DeleteContact(contactId);
        }

        [Fact]
        public async Task CreateContactWithoutAndButWithEmail()
        {
            var hubspot = GetHubSpot();
            var contactId = await hubspot.Contacts.CreateOrUpdate(new TestHubSpotContact
            {
                FirstName = "Mickey",
                LastName = "Mouse",
                Country = "Disneyland",
                Email = "mickey.mouse@disneyland.com"
            });
            _outputHelper.WriteLine($"CreateContact: id={contactId}");
            await DeleteContact(contactId);
        }

        [Fact]
        public async Task UpdateContact()
        {
            var hubspot = GetHubSpot();
            var contactId = await CreateContact(HubSpotTestContact_Email,false);
            var contact = await GetContact(contactId);

            await hubspot.Contacts.Update(new TestHubSpotContact
            {
                ID = contact.ID,
                FirstName = "Donald2",
                LastName = "Duck2",
                Country = "Disneyland2",
            });
            _outputHelper.WriteLine($"UpdateContact: id={contact.ID}");

            await DeleteContact(contactId);
        }

        private async Task DeleteContact(long contactId)
        {
            var hubspot = GetHubSpot();
            var contact = await GetContact(contactId);
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
                var response = await hubspot.Contacts.ListPage<TestHubSpotContact>(offset,19, nameof(TestHubSpotContact.Email));
                offset = response.Offset;
                hasMore = response.HasMore ?? false;
            } while (hasMore);
        }

    }
}