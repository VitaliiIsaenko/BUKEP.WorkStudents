using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;

namespace ScheduleBukepAPIUniteTests
{
    [TestClass()]
    public class FacadeApiTests
    {
        private static readonly Mock<IFacultiesService> MockFaculties = new Mock<IFacultiesService>();
        private static readonly Mock<ISchedulesService> MockSchedules = new Mock<ISchedulesService>();
        private static readonly FacadeApi FacadeApi = new FacadeApi(MockFaculties.Object, MockSchedules.Object);

        [TestMethod()]
        public void GetFacultiesTest()
        {        
            MockFaculties.Setup(
                m => m.GetFaculties("2016", "1000"))
                .Returns(new List<Faculty>(){new Faculty()});

            var faculties = FacadeApi.GetFaculties();
            MockFaculties.Verify(m => m.GetFaculties("2016", "1000"), Times.Once());
            Assert.AreEqual(faculties.Count, 1);
        }
    }
}