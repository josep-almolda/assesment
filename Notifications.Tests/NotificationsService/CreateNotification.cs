using System;
using Moq;
using Notifications.Common.Enums;
using Notifications.Common.Interfaces;
using Notifications.Common.Models;
using Xunit;

namespace Notifications.Tests.NotificationsService
{
    public class CreateNotification
    {
        [Fact]
        public void WhenMethodIsCalledBodyParserIsCalledWithRightData()
        {
            var userGuid = Guid.Parse("7c1e916c-6984-44b8-9778-c89704d08dda");
            // Arrange
            var model = new EventModel
            {
                Type = EventType.AppointmentCancelled,
                UserId = userGuid,
                Data = new EventData
                {
                    FirstName = "name",
                    Reason = "reason"
                }
            };
            var mockDataAccess = new Mock<INotificationsAccess>();
            var mockParser = new Mock<IBodyParser>();
            mockDataAccess.Setup(access => access.GetTemplate(It.IsAny<EventType>()))
                .Returns(new TemplateModel());

            var systemUnderTest = new Services.NotificationsService(mockDataAccess.Object, mockParser.Object);

            // Act
            systemUnderTest.CreateNotification(model);

            // Assert
            mockParser.Verify(parser => parser
                .ParseEventBody(It.IsAny<string>(), 
                                It.Is<EventData>(data => data.Reason == "reason" && data.FirstName == "name")));

        }

        [Fact]
        public void WhenMethodIsCalledBodyParserIsCalledWithRightTemplateBody()
        {
            var userGuid = Guid.Parse("7c1e916c-6984-44b8-9778-c89704d08dda");
            // Arrange
            var model = new EventModel
            {
                Type = EventType.AppointmentCancelled,
                UserId = userGuid,
                Data = new EventData
                {
                    FirstName = "name",
                    Reason = "reason"
                }
            };
            var mockDataAccess = new Mock<INotificationsAccess>();
            var mockParser = new Mock<IBodyParser>();
            mockDataAccess.Setup(access => access.GetTemplate(It.IsAny<EventType>()))
                .Returns(new TemplateModel
                {
                    Body = "body"
                });

            var systemUnderTest = new Services.NotificationsService(mockDataAccess.Object, mockParser.Object);

            // Act
            systemUnderTest.CreateNotification(model);

            // Assert
            mockParser.Verify(parser => parser
                .ParseEventBody(It.Is<string>(body => body == "body"),
                                It.IsAny<EventData>()));

        }

        [Fact]
        public void WhenMethodIsCalledDataAccessIsCalledWithTheParsedData()
        {
            var userGuid = Guid.Parse("7c1e916c-6984-44b8-9778-c89704d08dda");
            // Arrange
            var model = new EventModel
            {
                Type = EventType.AppointmentCancelled,
                UserId = userGuid,
                Data = new EventData
                {
                    FirstName = "name",
                    Reason = "reason"
                }
            };
            var mockDataAccess = new Mock<INotificationsAccess>();
            mockDataAccess.Setup(access => access.GetTemplate(It.IsAny<EventType>()))
                .Returns(new TemplateModel
                {
                    Body = "body",
                    Title = "title"
                });

            var mockParser = new Mock<IBodyParser>();
            mockParser.Setup(parser => parser.ParseEventBody(It.IsAny<string>(), It.IsAny<EventData>()))
                .Returns("parsed body");


            var systemUnderTest = new Services.NotificationsService(mockDataAccess.Object, mockParser.Object);

            // Act
            systemUnderTest.CreateNotification(model);

            // Assert
            mockDataAccess.Verify(access => access
                .AddNotification(It.Is<NotificationModel>(notification => notification.Text == "parsed body")));

        }

        [Fact]
        public void WhenDataAccessFailsServiceThrowsException()
        {
            var userGuid = Guid.Parse("7c1e916c-6984-44b8-9778-c89704d08dda");
            // Arrange
            var model = new EventModel
            {
                Type = EventType.AppointmentCancelled,
                UserId = userGuid,
                Data = new EventData
                {
                    FirstName = "name",
                    Reason = "reason"
                }
            };
            var mockDataAccess = new Mock<INotificationsAccess>();
            mockDataAccess.Setup(access => access.GetTemplate(It.IsAny<EventType>()))
                .Throws(new Exception());

            var mockParser = new Mock<IBodyParser>();
            mockParser.Setup(parser => parser.ParseEventBody(It.IsAny<string>(), It.IsAny<EventData>()))
                .Returns("parsed body");


            var systemUnderTest = new Services.NotificationsService(mockDataAccess.Object, mockParser.Object);

            // Act/Assert
            Assert.Throws<Exception>(() => systemUnderTest.CreateNotification(model));


        }
    }



}
