using System;
using System.Collections.Generic;
using Bukep.Sheduler.logic;
using ScheduleBukepAPI.domain;
using NUnit.Framework;

namespace ScheduleBukepTests
{
    [TestFixture]
    public class LessonOnDayTests
    {
        [Test]
        public void ParseTest()
        {
            var lessons = new List<Lesson>
            {
                new Lesson {DateLesson = DateTime.Today},
                new Lesson {DateLesson = DateTime.Today},
                new Lesson {DateLesson = DateTime.Today},

                new Lesson {DateLesson = DateTime.Today.AddDays(1)},
                new Lesson {DateLesson = DateTime.Today.AddDays(1)},

                new Lesson {DateLesson = DateTime.Today.AddDays(4)},
                new Lesson {DateLesson = DateTime.Today.AddDays(4)},
                new Lesson {DateLesson = DateTime.Today.AddDays(4)}
            };

            var lessonOnDays = LessonOnDay.Parse(lessons);
            Assert.That(lessonOnDays, Has.Count.EqualTo(3));
            Assert.That(lessonOnDays[0].Lessons, Has.Count.EqualTo(3));
            Assert.That(lessonOnDays[1].Lessons, Has.Count.EqualTo(2));
            Assert.That(lessonOnDays[2].Lessons, Has.Count.EqualTo(3));
        }
    }
}