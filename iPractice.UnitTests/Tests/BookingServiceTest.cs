using DeepEqual.Syntax;
using iPractice.Application.Contract.Dtos;
using iPractice.Application.Services;
using iPractice.UnitTests.MockUps;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace iPractice.UnitTests.Tests
{
    public class BookingServiceTest : Base
    {
        [Test]
        [TestCaseSource(typeof(BookingServiceTest), "GetTimeSlotValidInputs")]
        public void GetTimeSlotValidTest(long timeSlotId, TimeSlotDto expectedTimeSlot)
        {
            UnitOfWork.Setup(x => x.TimeSlot.FindAsync(It.IsAny<long>()).Result).Returns(expectedTimeSlot);
            var bookingService = new BookingService(UnitOfWork.Object, Mapper);
            var actualTimeSlot = bookingService.GetTimeSlotAsync(timeSlotId).Result;
            Assert.True((actualTimeSlot == null && expectedTimeSlot == null) ||
            (actualTimeSlot?.IsDeepEqual(expectedTimeSlot) == true));
        }

        [Test]
        [TestCaseSource(typeof(BookingServiceTest), "CreateAppointmentInvalidInputs")]
        public void CreateAppointmentInvalidTest(long clientId, long timeSlotId, TimeSlotDto expectedTimeSlot)
        {
            UnitOfWork.Setup(x => x.TimeSlot.FindAsync(expectedTimeSlot.Id).Result).Returns(expectedTimeSlot);
            UnitOfWork.Setup(x => x.TimeSlot.UpdateAsync(It.IsAny<TimeSlotDto>()).Result).Returns(expectedTimeSlot);
            var bookingService = new BookingService(UnitOfWork.Object, Mapper);
            Assert.That(() => bookingService.CreateAppointmentAsync(clientId, timeSlotId).RunSynchronously(), Throws.TypeOf<KeyNotFoundException>());
        }

        public static IEnumerable<TestCaseData> GetTimeSlotValidInputs
        {
            get
            {
                yield return new TestCaseData(TimeSlots.TimeSlot11.Id, TimeSlots.TimeSlot11);
                yield return new TestCaseData(TimeSlots.TimeSlot21.Id, TimeSlots.TimeSlot21);
                yield return new TestCaseData(TimeSlots.TimeSlot31.Id, TimeSlots.TimeSlot31);
                yield return new TestCaseData(TimeSlots.TimeSlot41.Id, TimeSlots.TimeSlot41);
                yield return new TestCaseData(-1, null);
            }
        }

        public static IEnumerable<TestCaseData> CreateAppointmentInvalidInputs
        {
            get
            {
                yield return new TestCaseData(Clients.Client1.Id, TimeSlots.TimeSlot21.Id, TimeSlots.TimeSlot11);
            }
        }
    }
}
