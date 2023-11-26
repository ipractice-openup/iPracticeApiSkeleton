using DeepEqual.Syntax;
using iPractice.Application.Contract.Dtos;
using iPractice.Application.Services;
using iPractice.Domain.Models;
using iPractice.UnitTests.MockUps;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPractice.UnitTests.Tests
{
    public class AvailabilityServiceTest : Base
    {
        [Test]
        [TestCaseSource(typeof(AvailabilityServiceTest), "GetAvailabilityValidInputs")]
        public void GetAvailabilityValidTest(long availabilityId, AvailabilityDto expectedAvailability, IEnumerable<PsychologistDto> expectedPsychologists)
        {
            UnitOfWork.Setup(x => x.Availability.FindAsync(It.IsAny<long>()).Result).Returns(expectedAvailability);
            UnitOfWork.Setup(x => x.Psychologist.GetPsychologists(It.IsAny<long>()).Result).Returns(expectedPsychologists);
            var psychologistService = new PsychologistService(UnitOfWork.Object, Mapper);
            var availabilityService = new AvailabilityService(UnitOfWork.Object, Mapper, psychologistService);
            var actualAvailability = availabilityService.GetAvailabilityAsync(availabilityId);
            Assert.True((actualAvailability == null && expectedAvailability == null) ||
            (actualAvailability?.IsDeepEqual(expectedAvailability) == true));
        }

        [Test]
        [TestCaseSource(typeof(AvailabilityServiceTest), "CreateAvailabilityValidInputs")]
        public void CreateAvailabilityValidTest(AvailabilityDto availability, AvailabilityDto expectedAvailability)
        {
            var mappedavailability = Mapper.Map<Availability>(availability);
            UnitOfWork.Setup(x => x.Availability.CreateAsync(It.IsAny<AvailabilityDto>()).Result).Returns(expectedAvailability);
            var psychologistService = new PsychologistService(UnitOfWork.Object, Mapper);
            var availabilityService = new AvailabilityService(UnitOfWork.Object, Mapper, psychologistService);
            var actualAvailabilityDto = availabilityService.CreateAvailabilityAsync(mappedavailability);
            var actualAvailability = Mapper.Map<Availability>(actualAvailabilityDto);
            Assert.True(actualAvailability?.IsDeepEqual(expectedAvailability) == true);
        }

        [Test]
        [TestCaseSource(typeof(AvailabilityServiceTest), "CreateAvailabilityInvalidInputs")]
        public void CreateAvailabilityInvalidTest(AvailabilityDto availability, AvailabilityDto expectedAvailability)
        {
            var mappedavailability = Mapper.Map<Availability>(availability);
            UnitOfWork.Setup(x => x.Availability.CreateAsync(It.IsAny<AvailabilityDto>()).Result).Returns(expectedAvailability);
            var psychologistService = new PsychologistService(UnitOfWork.Object, Mapper);
            var availabilityService = new AvailabilityService(UnitOfWork.Object, Mapper, psychologistService);
            Assert.That(() => availabilityService.CreateAvailabilityAsync(mappedavailability).RunSynchronously(), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCaseSource(typeof(AvailabilityServiceTest), "CreateAvailabilityValidInputs")]
        public void UpdateAvailabilityValidTest(AvailabilityDto availability, AvailabilityDto expectedAvailability)
        {
            var mappedavailability = Mapper.Map<Availability>(availability);
            UnitOfWork.Setup(x => x.Availability.UpdateAsync(It.IsAny<AvailabilityDto>()).Result).Returns(expectedAvailability);
            var psychologistService = new PsychologistService(UnitOfWork.Object, Mapper);
            var availabilityService = new AvailabilityService(UnitOfWork.Object, Mapper, psychologistService);
            var actualAvailabilityDto = availabilityService.UpdateAvailabilityAsync(mappedavailability);
            var actualAvailability = Mapper.Map<Availability>(actualAvailabilityDto);
            Assert.True(actualAvailability?.IsDeepEqual(expectedAvailability) == true);
        }

        [Test]
        [TestCaseSource(typeof(AvailabilityServiceTest), "CreateAvailabilityInvalidInputs")]
        public void UpdateAvailabilityInvalidTest(AvailabilityDto availability, AvailabilityDto expectedAvailability)
        {
            var mappedavailability = Mapper.Map<Availability>(availability);
            UnitOfWork.Setup(x => x.Availability.UpdateAsync(It.IsAny<AvailabilityDto>()).Result).Returns(expectedAvailability);
            var psychologistService = new PsychologistService(UnitOfWork.Object, Mapper);
            var availabilityService = new AvailabilityService(UnitOfWork.Object, Mapper, psychologistService);
            Assert.That(() => availabilityService.UpdateAvailabilityAsync(mappedavailability).RunSynchronously(), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCaseSource(typeof(AvailabilityServiceTest), "GetAvailableTimeSlotsValidInputs")]
        public void GetAvailableTimeSlotsValidTest(long clientId, IEnumerable<AvailabilityDto> expectedAvailabilities, IEnumerable<PsychologistDto> expectedPsychologists)
        {
            UnitOfWork.Setup(x => x.Psychologist.GetPsychologists(It.IsAny<long>()).Result).Returns(expectedPsychologists);
            UnitOfWork.Setup(x => x.Availability.GetByIds(It.IsAny<IEnumerable<long>>())).Returns(expectedAvailabilities);
            var psychologistService = new PsychologistService(UnitOfWork.Object, Mapper);
            var availabilityService = new AvailabilityService(UnitOfWork.Object, Mapper, psychologistService);
            var actualAvailabilities = availabilityService.GetAvailableTimeSlotsAsync(clientId);
            Assert.True((actualAvailabilities == null && expectedAvailabilities == null)
                || actualAvailabilities?.IsDeepEqual(expectedAvailabilities) == true);
        }

        public static IEnumerable<TestCaseData> GetAvailabilityValidInputs
        {
            get
            {
                yield return new TestCaseData(Availabilities.Availability1.Id, Availabilities.Availability1, new List<PsychologistDto> { Availabilities.Availability1.Psychologist });
                yield return new TestCaseData(Availabilities.Availability2.Id, Availabilities.Availability2, new List<PsychologistDto> { Availabilities.Availability2.Psychologist });
                yield return new TestCaseData(Availabilities.Availability3.Id, Availabilities.Availability3, new List<PsychologistDto> { Availabilities.Availability3.Psychologist });
                yield return new TestCaseData(Availabilities.Availability4.Id, Availabilities.Availability4, new List<PsychologistDto> { Availabilities.Availability4.Psychologist });
                yield return new TestCaseData(-1, null, null);
            }
        }

        public static IEnumerable<TestCaseData> CreateAvailabilityValidInputs
        {
            get
            {
                yield return new TestCaseData(Availabilities.Availability1, Availabilities.Availability1);
                yield return new TestCaseData(Availabilities.Availability2, Availabilities.Availability2);
                yield return new TestCaseData(Availabilities.Availability3, Availabilities.Availability3);
                yield return new TestCaseData(Availabilities.Availability4, Availabilities.Availability4);
            }
        }

        public static IEnumerable<TestCaseData> CreateAvailabilityInvalidInputs
        {
            get
            {
                yield return new TestCaseData(null, Availabilities.Availability1);
            }
        }

        public static IEnumerable<TestCaseData> GetAvailableTimeSlotsValidInputs
        {
            get
            {
                yield return new TestCaseData(Clients.Client1.Id, Clients.Client1.Psychologists.SelectMany(x => x.Availabilities), Clients.Client1.Psychologists);
                yield return new TestCaseData(Clients.Client2.Id, Clients.Client2.Psychologists.SelectMany(x => x.Availabilities), Clients.Client2.Psychologists);
                yield return new TestCaseData(Clients.Client3.Id, Clients.Client3.Psychologists.SelectMany(x => x.Availabilities), Clients.Client3.Psychologists);
                yield return new TestCaseData(Clients.Client4.Id, Clients.Client4.Psychologists.SelectMany(x => x.Availabilities), Clients.Client4.Psychologists);
                yield return new TestCaseData(-1, Enumerable.Empty<AvailabilityDto>(), Enumerable.Empty<PsychologistDto>());
            }
        }
    }
}