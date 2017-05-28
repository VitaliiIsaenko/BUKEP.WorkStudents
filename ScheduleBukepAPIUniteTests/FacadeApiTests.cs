using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleBukepAPI;

namespace ScheduleBukepAPIUniteTests
{
    [TestClass()]
    public class FacadeApiTests
    {
        [TestMethod()]
        public void GetFacultiesTest()
        {
            var facadeApi = new FacadeApi();
            var faculties = facadeApi.GetFaculties();
            Assert.AreEqual(faculties.Count, 14);
        }
    }
}