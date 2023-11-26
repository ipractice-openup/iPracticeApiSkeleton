using DeepEqual.Syntax;
using iPractice.Application.Contract.Dtos;
using iPractice.Application.Services;
using iPractice.UnitTests.MockUps;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace iPractice.UnitTests.Tests
{
    public class PsychologistServiceTest : Base
    {
        [Test]
        [TestCaseSource(typeof(PsychologistServiceTest), "GetPsychologistsValidInputs")]
        public void GetPsychologistsValidTest(long clientId, List<PsychologistDto> expectedPsychologists)
        {
            UnitOfWork.Setup(x => x.Psychologist.GetPsychologists(It.IsAny<long>()).Result).Returns(expectedPsychologists);
            var psychologistService = new PsychologistService(UnitOfWork.Object, Mapper);
            var actualPsychologists = psychologistService.GetPsychologistsAsync(clientId);
            Assert.True((actualPsychologists == null && expectedPsychologists == null) ||
            (actualPsychologists?.IsDeepEqual(expectedPsychologists) == true));
        }

        public static IEnumerable<TestCaseData> GetPsychologistsValidInputs
        {
            get
            {
                yield return new TestCaseData(Clients.Client1.Id, Clients.Client1.Psychologists);
                yield return new TestCaseData(Clients.Client2.Id, Clients.Client2.Psychologists);
                yield return new TestCaseData(Clients.Client3.Id, Clients.Client3.Psychologists);
                yield return new TestCaseData(Clients.Client4.Id, Clients.Client4.Psychologists);
                yield return new TestCaseData(-1, null);
            }
        }
    }
}
