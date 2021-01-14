using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Http;
using Vertical.HubSpot.Api.Query;
using Vertical.HubSpot.Api.Tests.Data;

namespace Vertical.HubSpot.Api.Tests {

    [TestFixture, Parallelizable]
    public class ContactApiTests {

        [Test, Parallelizable]
        public async Task TestQueryResponse() {
            Mock<IHttpClient> httpmock = new Mock<IHttpClient>();
            httpmock.Setup(s => s.PostAsync("crm/v3/objects/contacts/search?hapikey=test", It.IsAny<HttpContent>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) {
                Content = new StreamContent(typeof(ContactApiTests).Assembly.GetManifestResourceStream("Vertical.HubSpot.Api.Tests.Resources.sampleresponse.json"))
            });

            HubSpot hubspot=new HubSpot(new HubSpotOptions {
                ApiKey = "test",
                HttpClient = httpmock.Object
            });

            QueryPage<TestContact> contactresult=await hubspot.Contacts.Query<TestContact>(new ObjectQuery());
            Assert.NotNull(contactresult);
            Assert.AreEqual("10", contactresult.Paging.Next.After);
            Assert.AreEqual(1, contactresult.Results.Length);
            Assert.AreEqual(174, contactresult.Results[0].ID);
            Assert.AreEqual("Brantley", contactresult.Results[0].FirstName);
            Assert.AreEqual("Forrest", contactresult.Results[0].LastName);
            Assert.AreEqual("bforrest8@acme.com", contactresult.Results[0].Email);
        }
    }
}