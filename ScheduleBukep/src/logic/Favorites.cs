using System.Collections.Generic;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.logic
{
    public static class Favorites
    {
        private static readonly string KeyTeachers = UserDataKey.FavoritesTeachers.ToString();
        private static readonly string KeyGroups = UserDataKey.FavoritesGroups.ToString();

        private enum UserDataKey
        {
            FavoritesGroups,
            FavoritesTeachers
        }

        public static void AddGroup(Group group)
        {
            var groups = GetGroups();
            groups.Add(group);
            CacheHelper.PutUserData(KeyGroups, groups);
        }

        public static void AddTeacher(Teacher teacher)
        {
            var teachers = GetTeachers();
            teachers.Add(teacher);
            CacheHelper.PutUserData(KeyTeachers, teacher);
        }

        public static void DeleteGroup(Group group)
        {
            var groups = GetGroups();
            groups.Remove(group);
            CacheHelper.PutUserData(KeyGroups, groups);
        }

        public static void DeleteTeacher(Teacher teacher)
        {
            var teachers = GetTeachers();
            teachers.Remove(teacher);
            CacheHelper.PutUserData(KeyTeachers, teacher);
        }

        public static List<Teacher> GetTeachers()
        {
            List<Teacher> teachers = CacheHelper.GetOrPutUserData(
                KeyTeachers,
                () => new List<Teacher>());
            return teachers;
        }

        public static List<Group> GetGroups()
        {
            List<Group> groups = CacheHelper.GetOrPutUserData(
                KeyGroups,
                () => new List<Group>());
            return groups;
        }
    }
}