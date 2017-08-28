using System.Collections.Generic;
using NUnit.Framework;
using ScheduleBukepAPI.service.paremeters;
using Assert = NUnit.Framework.Assert;

namespace ScheduleBukepAPIUniteTests.service.paremeters
{
    [TestFixture]
    public class ParameterConstructorTests
    {
        [Test]
        public void CheckBuildParameter()
        {
            var builder = new ParameterConstructor();
            builder.SetParameter(NameParameterForApi.Year, 2016);
            builder.SetParameter(NameParameterForApi.IdFaculty, 1000);
            IDictionary<string, string> results = builder.GetResults();

            string yearValue = results[NameParameterForApi.Year.ToString()];
            string idFacultyValue = results[NameParameterForApi.IdFaculty.ToString()];

            Assert.That(yearValue, Is.EqualTo("2016"));
            Assert.That(idFacultyValue, Is.EqualTo("1000"));
            Assert.That(builder.GetResults().Count, Is.Zero);
        }
    }
}