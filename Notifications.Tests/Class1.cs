using System;
using Moq;
using Notifications.Common.Interfaces;
using Xunit;

namespace Notifications.Tests
{
    public class Class1
    {
        [Fact]
        public void TestExample()
        {
            //Arrange
            var mockService = new Mock<INotificationsService>();

            //var systemUnderTest = new Notifications.Controllers.NotificationsController(mockService.Object);

            //Act
            //var result = systemUnderTest.Post(null);

            //Assert
            Assert.True(true);
        }
    }
}
