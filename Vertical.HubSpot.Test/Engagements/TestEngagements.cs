using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Vertical.HubSpot.Api;
using Vertical.HubSpot.Api.Engagements;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Test.Test;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace Vertical.HubSpot.Test.Engagements
{
    public class TestEngagements : TestBase
    {
        private readonly ITestOutputHelper _outputHelper;
        public const long HubSpotTestContact_Id = 1149101;

        public TestEngagements(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        protected Api.HubSpot GetHubSpot()
        {
            return new Api.HubSpot(new HubSpotOptions
            {
                ApiKey = Configuration["Hubspot:ApiKey"]
            });
        }

        private async Task<HubSpotEngagementResult> FindEngagement(Api.HubSpot hubspot, long id)
        {
            var engagement = await hubspot.Engagements.GetEngagement<HubSpotEngagementResult>(id);
            Assert.True(engagement != null, $"Engagement with id {id} not found");
            return engagement;
        }

        [Fact]
        public async Task CreateEngagement()
        {
            var hubspot = GetHubSpot();
            
            var engagement = await hubspot.Engagements.CreateEngagement<HubSpotEngagementResult>(new HubSpotEngagementResult
            {
                Engagement = new HubspotEngagement { Active = true, OwnerId = 1149101, Type = "NOTE", Timestamp = 1599172644789, CreatedBy = 1149101 },
                Associations = new HubSpotEngagementAssociations { ContactIds = new List<long>() { 1149101 } },
                Attachments = new List<object>(),
                Metadata = new HubspotEngagementMetadata { Body = "Amazing note body" },
            });
            _outputHelper.WriteLine($"CreateContact: id={engagement.Engagement.Id}");
        }

        [Fact]
        public async Task UpdateEngagement()
        {
            var hubspot = GetHubSpot();
            
            var engagement = await hubspot.Engagements.CreateEngagement<HubSpotEngagementResult>(new HubSpotEngagementResult
            {
                Engagement = new HubspotEngagement { Active = true, OwnerId = 1149101, Type = "NOTE", Timestamp = 1599172644789, CreatedBy = 1149101 },
                Associations = new HubSpotEngagementAssociations { ContactIds = new List<long>() { 1149101 } },
                Attachments = new List<object>(),
                Metadata = new HubspotEngagementMetadata { Body = "Amazing note body" },
            });

            await hubspot.Engagements.UpdateEngagement<HubSpotEngagementResult>(engagement.Engagement.Id, new HubSpotEngagementResult
            {
                Engagement = new HubspotEngagement { Active = true, OwnerId = 1149101, Type = "NOTE", Timestamp = 1599172644789, CreatedBy = 1149101 },
                Associations = new HubSpotEngagementAssociations { ContactIds = new List<long>() { 1149101 } },
                Attachments = new List<object>(),
                Metadata = new HubspotEngagementMetadata { Body = "UPDATED Amazing note body" },
            });
            _outputHelper.WriteLine($"UpdateEngagement: id={engagement.Engagement.Id}");
            
        }

        [Fact]
        public async Task DeleteEngagement()
        {
            var hubspot = GetHubSpot();
            var engagement = await FindEngagement(hubspot, 6274996078);
            Assert.NotNull(engagement);

            await hubspot.Engagements.DeleteEngagement(engagement.Engagement.Id);
            _outputHelper.WriteLine($"DeleteEngagement: id={engagement.Engagement.Id}");

        }

        [Fact]
        public async Task ListEngagements()
        {
            var hubspot = GetHubSpot();

            long? offset = null;
            bool hasMore;
            do
            {
                var response = await hubspot.Engagements.ListAssociatedEngagements<HubSpotEngagementResult>(ObjectType.Contact, HubSpotTestContact_Id, offset, 19);
                offset = response.Offset;
                hasMore = response.HasMore ?? false;
            } while (hasMore);
        }

    }
}