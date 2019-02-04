using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Notifications;
using Notifications.Common.Models;
using Xunit;

namespace Web.Api.IntegrationTests.Controllers
{
    public class PlayersControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PlayersControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetAllNotifications()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/Notifications");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var notifications = JsonConvert.DeserializeObject<IEnumerable<NotificationModel>>(stringResponse);
            Assert.Contains(notifications, p => p.Title == "title1");
            Assert.Contains(notifications, p => p.Text == "text1");
        }


        //[Fact]
        //public async Task CanGetPlayerById()
        //{
        //    // The endpoint or route of the controller action.
        //    var httpResponse = await _client.GetAsync("/api/players/1");

        //    // Must be successful.
        //    httpResponse.EnsureSuccessStatusCode();

        //    // Deserialize and examine results.
        //    var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        //    var player = JsonConvert.DeserializeObject<Player>(stringResponse);
        //    Assert.Equal(1,player.Id);
        //    Assert.Equal("Wayne", player.FirstName);
        //}
    }
}
