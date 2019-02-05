using System;
using Moq;
using Notifications.Common.Interfaces;
using Xunit;

namespace Notifications.Tests.NotificationsService
{
    public class GetNotificationsByUser
    {
        [Fact]
        public void WhenMethodIsCalledDataAccessIsCalledWithTheRightId()
        {
            var userGuid = Guid.Parse("7c1e916c-6984-44b8-9778-c89704d08dda");
            // Arrange

            var mockDataAccess = new Mock<INotificationsAccess>();
            var mockParser = new Mock<IBodyParser>();


            var systemUnderTest = new Services.NotificationsService(mockDataAccess.Object, mockParser.Object);

            // Act
            systemUnderTest.GetNotificationsByUser(userGuid);

            // Assert
            mockDataAccess.Verify(access => access
                .GetNotificationsById(It.Is<Guid>(x => x == userGuid)));

        }

    }

}
