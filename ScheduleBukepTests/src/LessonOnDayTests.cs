using System.Collections.Generic;
using Bukep.Sheduler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleBukepAPI.domain;


namespace ScheduleBukepTests
{
    [TestClass()]
    public class LessonOnDayTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            var lessons = new List<GroupLesson>
            {
                new GroupLesson {DateLesson = "01.01.2017"},
                new GroupLesson {DateLesson = "01.01.2017"},
                new GroupLesson {DateLesson = "01.01.2017"},

                new GroupLesson {DateLesson = "02.01.2017"},
                new GroupLesson {DateLesson = "02.01.2017"},

                new GroupLesson {DateLesson = "03.02.2017"},
                new GroupLesson {DateLesson = "03.02.2017"},
                new GroupLesson {DateLesson = "03.02.2017"}
            };



            var lessonOnDays = LessonOnDay.Parse(lessons);
            Assert.AreEqual(3, lessonOnDays.Count);
            Assert.AreEqual(3, lessonOnDays[0].GroupLessons.Count);
            Assert.AreEqual(2 ,lessonOnDays[1].GroupLessons.Count);
            Assert.AreEqual(3 ,lessonOnDays[2].GroupLessons.Count);
        }
    }
}