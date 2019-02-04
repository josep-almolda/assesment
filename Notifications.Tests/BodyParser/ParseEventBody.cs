using System;
using Moq;
using Notifications.Common.Enums;
using Notifications.Common.Interfaces;
using Notifications.Common.Models;
using Xunit;

namespace Notifications.Tests.BodyParser
{
    public class ParseEventBody
    {
        [Fact]
        public void WhenMethodCalledDataPropertiesAreInterpolated()
        {
            // Arrange
            var data = new EventData
            {
                Firstname = "name",
                Reason = "reason",
                AppointmentDateTime = new DateTime(2020, 01, 02),
                OrganisationName = "org"
            };

            const string body = "{FirstName}|{Reason}|{AppointmentDateTime}|{OrganisationName}";

            var systemUnderTest = new Utils.BodyParser();

            // Act
            var result = systemUnderTest.ParseEventBody(body, data);

            // Assert
            var parts = result.Split('|');
            Assert.Equal("name", parts[0]);
            Assert.Equal("reason", parts[1]);
            Assert.Equal(new DateTime(2020, 01, 02).ToString("G"), parts[2]);
            Assert.Equal("org", parts[3]);
        }

        [Fact]
        public void WhenMethodCalledDataPropertyIsInterpolatedMultipleTimes()
        {
            // Arrange
            var data = new EventData
            {
                Firstname = "name",
                Reason = "reason",
                AppointmentDateTime = new DateTime(2020, 01, 02),
                OrganisationName = "org"
            };

            const string body = "{FirstName}|{FirstName}|{FirstName}|{FirstName}";

            var systemUnderTest = new Utils.BodyParser();

            // Act
            var result = systemUnderTest.ParseEventBody(body, data);

            // Assert
            var parts = result.Split('|');
            Assert.Equal("name", parts[0]);
            Assert.Equal("name", parts[1]);
            Assert.Equal("name", parts[2]);
            Assert.Equal("name", parts[3]);
        }
    }
}
