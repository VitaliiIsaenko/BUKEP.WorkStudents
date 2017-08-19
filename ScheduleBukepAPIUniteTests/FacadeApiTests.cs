using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;
using Assert = NUnit.Framework.Assert;

namespace ScheduleBukepAPIUniteTests
{
    [TestFixture()]
    public class FacadeApiTests
    {
        private static readonly Mock<IFacultiesService> MockFaculties = new Mock<IFacultiesService>();
        private static readonly Mock<ISchedulesService> MockSchedules = new Mock<ISchedulesService>();
        private static readonly Api Api = new Api(MockFaculties.Object, MockSchedules.Object);

        [Test()]
        public void GetFacultiesTest()
        {
            var value = new List<Faculty>()
            {
                new Faculty(),
                new Faculty()
            };

            MockFaculties.Setup(m => m.GetFaculties(2016, 1000)).Returns(value);

            var faculties = Api.GetFaculties();

            MockFaculties.Verify(m => m.GetFaculties(2016, 1000), Times.Once());
            Assert.That(faculties.Count, Is.EqualTo(2));
        }
    }
}