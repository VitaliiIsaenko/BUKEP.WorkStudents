using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPIUniteTests.helpers
{
    [TestClass()]
    public class CreatorUrlTests
    {
        [TestMethod()]
        public void CreateUrlTest()
        {
            var dictionary = new Dictionary<string, string>
            {
                {"Key_1", "Value_1"},
                {"Key_2", "Value_2"}
            };

            var url = CreatorUrl.CreateUrl("Method_1", dictionary);
            Assert.AreEqual(
                $"{CreatorUrl.UrlApi}/Method_1?Key_1=Value_1&Key_2=Value_2&",
                url
            );
        }
    }
}