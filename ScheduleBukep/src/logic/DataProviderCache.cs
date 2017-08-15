using System;
using System.Collections.Generic;
using Akavache;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.logic
{
    public class DataProviderCache : DataProvider
    {
        private const string Tag = "DataProviderCache";
        private readonly CacheHelper _cacheHelper;

        public DataProviderCache(BaseActivity activity) : base(activity)
        {
            BlobCache.ApplicationName = "ScheduleBukep";
            _cacheHelper = new CacheHelper(ExecutorInternetOperations);
        }

        public override IList<Faculty> GetFaculties()
        {
            const string key = "faculty";
            return _cacheHelper.GetAndPutInCached(key, () => base.GetFaculties());
        }

        public override IList<Specialty> GetSpecialtys(int idFaculty)
        {
            string key = "specialtys_"+ idFaculty;
            return _cacheHelper.GetAndPutInCached(key, () => base.GetSpecialtys(idFaculty));
        }

        public override IList<Group> GetGroups(int idFaculty, int idCourse, IList<int> idsSpecialty)
        {
            string key = $"groups_{idFaculty}_{idCourse}_{Api.ConvertIdsToString(idsSpecialty)}";
            return _cacheHelper.GetAndPutInCached(key, () => base.GetGroups(idFaculty, idCourse, idsSpecialty));
        }

        public override IList<Course> GetCourses(int idFaculty, IList<int> idsSpecialty)
        {
            string key = $"courses{idFaculty}_{Api.ConvertIdsToString(idsSpecialty)}";
            return _cacheHelper.GetAndPutInCached(key, () => base.GetCourses(idFaculty, idsSpecialty));
        }

        public override IList<Pulpit> GetPulpits()
        {
            const string key = "pulpits";
            return _cacheHelper.GetAndPutInCached(key, () => base.GetPulpits());
        }

        public override IList<Teacher> GetTeacher(int idPulpit)
        {
            string key = $"teacher_{idPulpit}";
            return _cacheHelper.GetAndPutInCached(key, () => base.GetTeacher(idPulpit));
        }

        public override IList<Lesson> GetGroupLessons(IList<int> idsSheduleGroup, DateTime dateFrom, DateTime dateTo)
        {
            string key = $"teacher_{Api.ConvertIdsToString(idsSheduleGroup)}_{dateFrom}_{dateTo}";
            return _cacheHelper.GetAndPutInCached(key, () => base.GetGroupLessons(idsSheduleGroup, dateFrom, dateTo));
        }

        public override IList<Lesson> GetTeacherLessons(IList<int> idsTeacher, DateTime dateFrom, DateTime dateTo)
        {
            string key = $"teacherLessons{Api.ConvertIdsToString(idsTeacher)}_{dateFrom}_{dateTo}";
            return _cacheHelper.GetAndPutInCached(key, () => base.GetTeacherLessons(idsTeacher, dateFrom, dateTo));
        }
    }
}