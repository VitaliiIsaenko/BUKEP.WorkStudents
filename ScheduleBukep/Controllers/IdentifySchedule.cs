using System;
using System.Collections.Generic;
using Bukep.ShedulerApi;
using Bukep.ShedulerApi.apiDTO;
using Android.Widget;
using Android.Views;
using Java.Lang;
using Android.Runtime;
using Android.Content;

namespace Bukep.Sheduler.Controllers
{
    class IdentifySchedule : IController
    {
        private IdentifyScheduleActivity view;

        private Faculty selectedFaculty;
        private Specialty selectedSpecialty;
        private Courses selectedCourse;
        private Group selectedGroup;

        public IdentifySchedule(IdentifyScheduleActivity view)
        {
            this.view = view;
        }

        public void Update()
        {
            List<Faculty> faculties = FacadeAPI.GetFaculties();           
            view.ShowFaculty(faculties);
        }

        internal void SelectFaculty(Faculty faculty)
        {
            selectedFaculty = faculty;
            List<Specialty> specialtys = FacadeAPI.GetSpecialtys(faculty.IdFaculty);
            view.ShowSpecialtys(specialtys);
        }

        internal void SelectSpecialt(Specialty specialty)
        {
            selectedSpecialty = specialty;
            List<Courses> courses = FacadeAPI.GetCourses(
                selectedFaculty.IdFaculty,
                FacadeAPI.ConvertIdsToString(specialty.IdsSpecialty)
                );
            view.ShowCourses(courses);
        }

        internal void SelectCourses(Courses cours)
        {
            selectedCourse = cours;
            List<Group> groups = FacadeAPI.GetGroups(
                selectedFaculty.IdFaculty,
                selectedCourse.IdCourse,
                FacadeAPI.ConvertIdsToString(selectedSpecialty.IdsSpecialty)
                );
            view.ShowGroup(groups);
        }

        internal void SelectGroup(Group group)
        {
            //throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Нужен для представления DTO как String в Spinner.
    /// Приэтом иметь возможность получить DTO.
    /// </summary>
    /// <typeparam name="T">DTO для DTOAdapter</typeparam>
    abstract class DTOAdapter<T> : BaseAdapter
    {
        private IList<T> objects;
        private Context context;

        public DTOAdapter(IList<T> objects, Context context)
        {
            this.context = context;
            this.objects = objects;
        }

        public override int Count
        {
            get { return objects.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView textView = new TextView(context)
            {
                Text = ConvertDTOInString(objects[position])
            };
            return textView;
        }

        public T GetObject(int position)
        {
            return objects[position];
        }

        public abstract string ConvertDTOInString(T t);

    }

    internal class FacultyAdapter<T> : DTOAdapter<Faculty>
    {
        public FacultyAdapter(IList<Faculty> objects, Context context) : base(objects, context)
        {
        }

        public override string ConvertDTOInString(Faculty t)
        {
            return t.Name;
        }
    }

    internal class SpecialtyAdapter<T> : DTOAdapter<Specialty>
    {
        public SpecialtyAdapter(IList<Specialty> objects, Context context) : base(objects, context)
        {
        }

        public override string ConvertDTOInString(Specialty t)
        {
            return t.NameSpeciality;
        }
    }

    internal class CoursesAdapter<T> : DTOAdapter<Courses>
    {
        public CoursesAdapter(IList<Courses> objects, Context context) : base(objects, context)
        {
        }

        public override string ConvertDTOInString(Courses t)
        {
            return t.NameCourse;
        }
    }

    internal class GroupAdapter<T> : DTOAdapter<Group>
    {
        public GroupAdapter(IList<Group> objects, Context context) : base(objects, context)
        {
        }

        public override string ConvertDTOInString(Group t)
        {
            return t.NameGroup;
        }
    }

}