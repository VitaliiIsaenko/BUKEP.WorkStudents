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
        private readonly CacheHelper CacheHelper;

        public DataProviderCache(BaseActivity activity) : base(activity)
        {
            BlobCache.ApplicationName = "ScheduleBukep";
            CacheHelper = new CacheHelper();
        }

        public override IList<Faculty> GetFaculties()
        {
            const string key = "faculty";
            return CacheHelper.GetOrPutCached(key, () => base.GetFaculties());
        }

        public override IList<Specialty> GetSpecialtys(int idFaculty)
        {
            string key = "specialtys_"+ idFaculty;
            return CacheHelper.GetOrPutCached(key, () => base.GetSpecialtys(idFaculty));
        }

        public override IList<Group> GetGroups(int idFaculty, int idCourse, IList<int> idsSpecialty)
        {
            string key = $"groups_{idFaculty}_{idCourse}_{Api.ConvertIdsToString(idsSpecialty)}";
            return CacheHelper.GetOrPutCached(key, () => base.GetGroups(idFaculty, idCourse, idsSpecialty));
        }

        public override IList<Course> GetCourses(int idFaculty, IList<int> idsSpecialty)
        {
            string key = $"courses{idFaculty}_{Api.ConvertIdsToString(idsSpecialty)}";
            return CacheHelper.GetOrPutCached(key, () => base.GetCourses(idFaculty, idsSpecialty));
        }

        public override IList<Pulpit> GetPulpits()
        {
            const string key = "pulpits";
            return CacheHelper.GetOrPutCached(key, () => base.GetPulpits());
        }

        public override IList<Teacher> GetTeacher(int idPulpit)
        {
            string key = $"teacher_{idPulpit}";
            return CacheHelper.GetOrPutCached(key, () => base.GetTeacher(idPulpit));
        }

        public override IList<Lesson> GetGroupLessons(IList<int> idsSheduleGroup, DateTime dateFrom, DateTime dateTo)
        {
            string key = $"teacher_{Api.ConvertIdsToString(idsSheduleGroup)}_{dateFrom}_{dateTo}";
            return CacheHelper.GetOrPutCached(key, () => base.GetGroupLessons(idsSheduleGroup, dateFrom, dateTo));
        }

        public override IList<Lesson> GetTeacherLessons(IList<int> idsTeacher, DateTime dateFrom, DateTime dateTo)
        {
            string key = $"teacherLessons{Api.ConvertIdsToString(idsTeacher)}_{dateFrom}_{dateTo}";
            return CacheHelper.GetOrPutCached(key, () => base.GetTeacherLessons(idsTeacher, dateFrom, dateTo));
        }
    }
}