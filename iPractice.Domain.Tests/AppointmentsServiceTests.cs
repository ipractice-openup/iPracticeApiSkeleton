using iPractice.DataAccess.Models;
using iPractice.Domain.Exceptions;
using iPractice.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace iPractice.Domain.Tests
{
    public class AppointmentsServiceTests
    {
        [Fact]
        public async Task CreateAppointment_WhenPsychologystNotFound_ThrowsException()
        {
            // Arrange
            var client = new Client
            {
                Id = 1,
                Name = "Client",
                Psychologists = new List<Psychologist>()
            };

            var clients = new List<Client>
            {
                client
            };

            var psychologists = new List<Psychologist>();

            var clientsMock = InitHelpers.GetQueryableMockDbSet(clients);
            var psychologistsMock = InitHelpers.GetQueryableMockDbSet(psychologists);

            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Clients).Returns(clientsMock.Object);
            mockContext.Setup(m => m.Psychologists).Returns(psychologistsMock.Object);

            var service = new AppointmentsService(mockContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<PsychologistNotFoundException>(() => service.CreateAsync(new Models.Appointment
            {
                ClientId = client.Id,
                PsychologistId = 0,
            }));
        }

        [Fact]
        public async Task CreateAppointment_WhenClientNotFound_ThrowsException()
        {
            // Arrange

            var clients = new List<Client>();
            var psychologists = new List<Psychologist>();

            var clientsMock = InitHelpers.GetQueryableMockDbSet(clients);
            var psychologistsMock = InitHelpers.GetQueryableMockDbSet(psychologists);

            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Clients).Returns(clientsMock.Object);
            mockContext.Setup(m => m.Psychologists).Returns(psychologistsMock.Object);

            var service = new AppointmentsService(mockContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ClientNotFoundException>(() => service.CreateAsync(new Models.Appointment
            {
                ClientId = 0,
                PsychologistId = 0,
            }));
        }

        [Fact]
        public async Task CreateAppointment_WhenPsychologystNotConnectedToClient_ThrowsException()
        {
            // Arrange
            var client = new Client
            {
                Id = 1,
                Name = "Client",
                Psychologists = new List<Psychologist>()
            };

            var psychologist = new Psychologist
            {
                Id = 1,
                Name = "Psychologist",
                Clients = new List<Client>()
            };

            client.Psychologists.Add(psychologist); 
            psychologist.Clients.Add(client);

            var notConnectedPsychologyst = new Psychologist
            {
                Id = 100,
                Name = "NotConnected Psychologist",
                Clients = new List<Client>()
            };

            var clients = new List<Client>
            {
                client
            };

            var psychologists = new List<Psychologist> 
            { 
                psychologist, notConnectedPsychologyst 
            };

            var clientsMock = InitHelpers.GetQueryableMockDbSet(clients);
            var psychologistsMock = InitHelpers.GetQueryableMockDbSet(psychologists);

            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Clients).Returns(clientsMock.Object);
            mockContext.Setup(m => m.Psychologists).Returns(psychologistsMock.Object);

            var service = new AppointmentsService(mockContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(new Models.Appointment
            {
                ClientId = client.Id,
                PsychologistId = notConnectedPsychologyst.Id,
            }));
        }

        [Theory, MemberData(nameof(StartAndEndDate), MemberType = typeof(AppointmentsServiceTests))]
        public async Task CreateAppointment_WhenTimeIsInvalid_ThrowsException(DateTime? startDate, DateTime? endDate)
        {
            // Arrange
            var client = new Client
            {
                Id = 1,
                Name = "Client",
                Psychologists = new List<Psychologist>()
            };

            var psychologist = new Psychologist
            {
                Id = 1,
                Name = "Psychologist",
                Clients = new List<Client>()
            };

            client.Psychologists.Add(psychologist);
            psychologist.Clients.Add(client);

            var clients = new List<Client>
            {
                client
            };

            var psychologists = new List<Psychologist>
            {
                psychologist
            };

            var clientsMock = InitHelpers.GetQueryableMockDbSet(clients);
            var psychologistsMock = InitHelpers.GetQueryableMockDbSet(psychologists);

            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Clients).Returns(clientsMock.Object);
            mockContext.Setup(m => m.Psychologists).Returns(psychologistsMock.Object);

            var service = new AppointmentsService(mockContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(new Models.Appointment
            {
                ClientId = client.Id,
                PsychologistId = psychologist.Id,
                Start = startDate.Value,
                End = endDate.Value
            }));
        }


        public static readonly object[][] StartAndEndDate =
        {
            new object[] { new DateTime(2023,3,1,10,30,0), new DateTime(2023,3,1,11,30,0) },
            new object[] { new DateTime(2023,3,1,10,37,0), new DateTime(2023,3,1,11,07,0) },
            new object[] { new DateTime(2023,3,1,12,30,0), new DateTime(2023,3,1,12,00,0) },
        };
    }
}
