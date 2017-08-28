using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.logic
{
    /// <summary>
    /// Нужен для добавления и получения избранного из кэша.
    /// </summary>
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
            UseGroups(groups => groups.Add(group));
        }

        public static void AddTeacher(Teacher teacher)
        {
            UseTeachers(teachers => teachers.Add(teacher));
        }

        public static void DeleteGroup(Group group)
        {
            UseGroups(groups =>
            {
                groups.RemoveAll(
                    group1 => group.Ids.SequenceEqual(group.Ids));
            });
        }

        public static void DeleteTeacher(Teacher teacher)
        {
            UseTeachers(teachers =>
            {
                teachers.RemoveAll(
                    teacherComparable => teacherComparable.IdsTeacher.SequenceEqual(teacher.IdsTeacher)
                );
            });
        }

        public static List<Teacher> GetTeachers()
        {
            var teachers = CacheHelper.GetOrPutUserData(
                KeyTeachers,
                () => new List<Teacher>()
            );
            return teachers;
        }

        public static List<Group> GetGroups()
        {
            var group = CacheHelper.GetOrPutUserData(
                KeyGroups,
                () => new List<Group>()
            );
            return group;
        }

        public static bool ExistTeacher(Teacher teacher)
        {
            var isExist = false;
            UseTeachers(list =>
            {
                isExist = list.Any(teacherComparable =>
                    teacherComparable.IdsTeacher.SequenceEqual(teacher.IdsTeacher));
            });

            return isExist;
        }

        public static bool ExistGroup(Group group)
        {
            var isExist = false;
            UseGroups(list =>
            {
                isExist = list.Any(groupComparable =>
                    groupComparable.Ids.SequenceEqual(group.Ids));
            });

            return isExist;
        }

        public static void UseTeachers(Action<List<Teacher>> action)
        {
            List<Teacher> teachers = GetTeachers();
            action.Invoke(teachers);
            CacheHelper.PutUserData(KeyTeachers, teachers);
        }

        public static void UseGroups(Action<List<Group>> action)
        {
            List<Group> groups = GetGroups();
            action.Invoke(groups);
            CacheHelper.PutUserData(KeyTeachers, groups);
        }
    }
}