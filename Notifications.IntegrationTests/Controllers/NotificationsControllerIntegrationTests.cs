using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Notifications;
using Notifications.Common.Models;
using Xunit;

namespace Notifications.IntegrationTests.Controllers
{
    public class NotificationsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public NotificationsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
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

        [Fact]
        public async Task CanCreateNotification()
        {
            var userId = Guid.Parse("3b9d27b1-60bd-45db-a812-a0070a20b64e");
            var content = new EventModel
            {
                UserId = userId,
                Data = new EventData
                {
                    Firstname = "n",
                    OrganisationName = "o",
                    Reason = "r",
                    AppointmentDateTime = new DateTime(2022, 1, 2)
                }
            };

            // The endpoint or route of the controller action.
            var httpCreateResponse = await _client.PostAsync("/api/Notifications", 
                new StringContent(
                    JsonConvert.SerializeObject(content), 
                    Encoding.UTF8, 
                    "application/json"));

            // check the content has changed
            var httpGetResponse = await _client.GetAsync("/api/Notifications");
            var stringResponse = await httpGetResponse.Content.ReadAsStringAsync();
            var notifications = JsonConvert.DeserializeObject<IEnumerable<NotificationModel>>(stringResponse);
            Assert.Contains(notifications, p => p.Title == "Appointment Cancelled");
            Assert.Contains(notifications, p => 
                p.Text == "Hi n, your appointment with o at 02/01/2022 00:00:00 has been - cancelled for the following reason: r.");
        }

        [Fact]
        public async Task CanGetNotificationsByUser()
        {
            var userId = Guid.Parse("3b9d27b1-60bd-45db-a812-a0070a20b64e");
            var content = new EventModel
            {
                UserId = userId,
                Data = new EventData
                {
                    Firstname = "n",
                    OrganisationName = "o",
                    Reason = "r",
                    AppointmentDateTime = new DateTime(2022, 1, 2)
                }
            };
    
            // add a new notification with a different user
            var httpCreateResponse = await _client.PostAsync("/api/Notifications",
                new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json"));

            // make the call to the controller
            var httpGetResponse = await _client.GetAsync($"/api/Notifications/userId/{userId}");
            var stringResponse = await httpGetResponse.Content.ReadAsStringAsync();
            var notifications = JsonConvert.DeserializeObject<IEnumerable<NotificationModel>>(stringResponse);
            
            // check the notifications with a different user are not retrieved
            Assert.DoesNotContain(notifications, p => p.Title == "title1");
            Assert.DoesNotContain(notifications, p => p.Text == "text1");
        }

    }
}
