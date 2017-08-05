using System;
using System.Collections.Generic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;
using ScheduleBukepAPITest;

namespace Bukep.Sheduler.logic
{
    /// <summary>
    /// ѕредоставл€ет данные дл€ приложени€.
    /// </summary>
    public class DataProvider
    {
        private const string Tag = "DataProvider";

        //TODO: добавить ApiFactory
        private readonly Api _api = new Api(
            new FilteringFacultiesService(new FakeHttpRequstHelper()),
            new SchedulesService(new FakeHttpRequstHelper())
        );

        private readonly InternetChecker _internetChecker;

        public DataProvider(BaseActivity activity)
        {
            _internetChecker = new InternetChecker(activity);
        }

        public IList<Faculty> GetFaculties()
        {
                return _internetChecker.ExecuteOperation(
                    () => _api.GetFaculties(),
                    new List<Faculty>()
                    );
        }

        public IList<Specialty> GetSpecialtys(int idFaculty)
        {
            return _internetChecker.ExecuteOperation(
                () => _api.GetSpecialtys(idFaculty),
                new List<Specialty>()
            );
        }

        public IList<Group> GetGroups(int idFaculty, int idCourse, IList<int> idsSpecialty)
        {
            return _internetChecker.ExecuteOperation(
                () => _api.GetGroups(idFaculty, idCourse, idsSpecialty),
                new List<Group>()
            );
        }

        public IList<Course> GetCourses(int idFaculty, IList<int> idsSpecialty)
        {
            return _internetChecker.ExecuteOperation(
                () => _api.GetCourses(idFaculty, idsSpecialty),
                new List<Course>()
            );
        }

        public IList<Pulpit> GetPulpits()
        {
            return _internetChecker.ExecuteOperation(
                () => _api.GetPulpits(),
                new List<Pulpit>()
            );
        }

        public IList<Teacher> GetTeacher(int idPulpit)
        {
            return _internetChecker.ExecuteOperation(
                () => _api.GetTeacher(idPulpit),
                new List<Teacher>()
            );
        }

        public IList<Lesson> GetGroupLessons(IList<int> idsSheduleGroup,
            DateTime dateFrom, DateTime dateTo)
        {
            return _internetChecker.ExecuteOperation(
                () => _api.GetGroupLessons(idsSheduleGroup, dateFrom, dateTo),
                new List<Lesson>()
            );
        }

        public IList<Lesson> GetTeacherLessons(IList<int> idsTeacher, DateTime dateFrom, DateTime dateTo)
        {
            return _internetChecker.ExecuteOperation(
                () => _api.GetTeacherLessons(idsTeacher, dateFrom, dateTo),
                new List<Lesson>()
            );
        }
    }
}