using System;
using System.Collections.Generic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service;

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
            new FilteringFacultiesService(new HttpRequstHelper()),
            new SchedulesService(new HttpRequstHelper())
        );

        protected readonly ExecutorInternetOperations ExecutorInternetOperations;

        public DataProvider(BaseActivity activity)
        {
            ExecutorInternetOperations = new ExecutorInternetOperations(activity);
        }

        public virtual IList<Faculty> GetFaculties()
        {
            return ExecutorInternetOperations.ExecuteOperation(
                () => _api.GetFaculties(),
                null
            );
        }

        public virtual IList<Specialty> GetSpecialtys(int idFaculty)
        {
            return ExecutorInternetOperations.ExecuteOperation(
                () => _api.GetSpecialtys(idFaculty),
                null
            );
        }

        public virtual IList<Group> GetGroups(int idFaculty, int idCourse, IList<int> idsSpecialty)
        {
            return ExecutorInternetOperations.ExecuteOperation(
                () => _api.GetGroups(idFaculty, idCourse, idsSpecialty),
                null
            );
        }

        public virtual IList<Course> GetCourses(int idFaculty, IList<int> idsSpecialty)
        {
            return ExecutorInternetOperations.ExecuteOperation(
                () => _api.GetCourses(idFaculty, idsSpecialty),
                null
            );
        }

        public virtual IList<Pulpit> GetPulpits()
        {
            return ExecutorInternetOperations.ExecuteOperation(
                () => _api.GetPulpits(),
                null
            );
        }

        public virtual IList<Teacher> GetTeacher(int idPulpit)
        {
            return ExecutorInternetOperations.ExecuteOperation(
                () => _api.GetTeacher(idPulpit),
                null
            );
        }

        public virtual IList<Lesson> GetGroupLessons(IList<int> idsSheduleGroup,
            DateTime dateFrom, DateTime dateTo)
        {
            return ExecutorInternetOperations.ExecuteOperation(
                () => _api.GetGroupLessons(idsSheduleGroup, dateFrom, dateTo),
                null
            );
        }

        public virtual IList<Lesson> GetTeacherLessons(IList<int> idsTeacher, DateTime dateFrom, DateTime dateTo)
        {
            return ExecutorInternetOperations.ExecuteOperation(
                () => _api.GetTeacherLessons(idsTeacher, dateFrom, dateTo),
                null
            );
        }
    }
}