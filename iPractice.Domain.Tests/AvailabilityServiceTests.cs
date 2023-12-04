using iPractice.DataAccess.Models;
using iPractice.Domain.Exceptions;
using iPractice.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace iPractice.Domain.Tests
{
    public class AvailabilityServiceTests
    {
        [Fact]
        public async Task CreateAvailability_WhenPsychologystNotFound_ThrowsException()
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

            var service = new AvailabilityService(mockContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<PsychologistNotFoundException>(() => service.CreateAsync(new Models.Availability
            {
                PsychologistId = 0,
            }));
        }

        [Theory, MemberData(nameof(StartAndEndDate), MemberType = typeof(AvailabilityServiceTests))]
        public async Task CreateAvailability_WhenTimeIsInvalid_ThrowsException(DateTime? startDate, DateTime? endDate)
        {
            // Arrange

            var psychologist = new Psychologist
            {
                Id = 1,
                Name = "Psychologist",
                Clients = new List<Client>()
            };

            var psychologists = new List<Psychologist>
            {
                psychologist
            };

            var psychologistsMock = InitHelpers.GetQueryableMockDbSet(psychologists);

            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Psychologists).Returns(psychologistsMock.Object);

            var service = new AvailabilityService(mockContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(new Models.Availability
            {
                PsychologistId = psychologist.Id,
                Start = startDate.Value,
                End = endDate.Value
            }));
        }


        public static readonly object[][] StartAndEndDate =
        {
            new object[] { new DateTime(2023,3,1,10,37,0), new DateTime(2023,3,1,11,37,0) },
            new object[] { new DateTime(2023,3,1,10,30,0), new DateTime(2023,3,1,10,30,0) },
            new object[] { new DateTime(2023,3,1,12,30,0), new DateTime(2023,3,1,12,00,0) },
        };
    }
}
