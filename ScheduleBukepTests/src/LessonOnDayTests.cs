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
            var lessons = new List<Lesson>
            {
                new Lesson {DateLesson = "01.01.2017"},
                new Lesson {DateLesson = "01.01.2017"},
                new Lesson {DateLesson = "01.01.2017"},

                new Lesson {DateLesson = "02.01.2017"},
                new Lesson {DateLesson = "02.01.2017"},

                new Lesson {DateLesson = "03.02.2017"},
                new Lesson {DateLesson = "03.02.2017"},
                new Lesson {DateLesson = "03.02.2017"}
            };

            var lessonOnDays = LessonOnDay.Parse(lessons);
            Assert.AreEqual(3, lessonOnDays.Count);
            Assert.AreEqual(3, lessonOnDays[0].Lessons.Count);
            Assert.AreEqual(2, lessonOnDays[1].Lessons.Count);
            Assert.AreEqual(3, lessonOnDays[2].Lessons.Count);
        }

        [TestMethod()]
        public void CombineLessonTest()
        {
            const string nameDisciplineLessoneOne = "Обработка отраслевой информации";
            const string nameDisciplineLessonTwo = "Физическая культура";
            const string nameAuditoryLessonTwo = "Спортивный комплекс";

            var lessonOnDay = new LessonOnDay()
            {
                Lessons =
                {
                    CreateLesson(nameDisciplineLessoneOne, "доц. Иванова И.Н.", "101"),
                    CreateLesson(nameDisciplineLessoneOne, "доц. Петрова K.П.", "201н"),

                    CreateLesson(nameDisciplineLessonTwo,
                    "ст. преп. Колобов А.В.", nameAuditoryLessonTwo),
                    CreateLesson(nameDisciplineLessonTwo,
                        "доц. Ленин С.Е.", nameAuditoryLessonTwo),
                    CreateLesson(nameDisciplineLessonTwo,
                        "асс. Колобов М.В.", nameAuditoryLessonTwo)
                }
            };

            lessonOnDay.CombineLesson();

            var lessons = lessonOnDay.Lessons;
            Assert.AreEqual(2, lessons.Count);

            var lessonOne = lessons[0];
            Assert.AreEqual(nameDisciplineLessoneOne,lessonOne.NameDiscipline);
            Assert.AreEqual("101, 201н",lessonOne.NameAuditory);
            Assert.AreEqual(
                "доц. Иванова И.Н., доц. Петрова K.П.",
                lessonOne.FioTeacher
                );

            var lessonTwo = lessons[1];
            Assert.AreEqual(nameDisciplineLessonTwo, lessonTwo.NameDiscipline);
            Assert.AreEqual(nameAuditoryLessonTwo, lessonTwo.NameAuditory);
            Assert.AreEqual(
                "ст. преп. Колобов А.В., доц. Ленин С.Е., асс. Колобов М.В.",
                lessonTwo.FioTeacher
                );
        }

        private static Lesson CreateLesson(string nameDiscipline, string fioTeacher, string nameAuditory)
        {
            return new Lesson {NameDiscipline = nameDiscipline, FioTeacher = fioTeacher, NameAuditory = nameAuditory};
        }
    }
}