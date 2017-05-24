using System;
using System.Collections.Generic;
using Android.Widget;
using Android.Views;
using Android.Content;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.Controllers
{
    internal class IdentifySchedule : IController
    {
        private readonly IdentifyScheduleActivity _view;

        private Faculty _selectedFaculty;
        private Specialty _selectedSpecialty;
        private Courses _selectedCourse;
        private Group _selectedGroup;

        public IdentifySchedule(IdentifyScheduleActivity view)
        {
            _view = view;
        }

        public void Update()
        {
            var faculties = FacadeApi.GetFaculties();           
            _view.ShowFaculty(faculties);
            _view.SetButtoneShowClickable(false);
        }

        public void SelectFaculty(Faculty faculty)
        {
            _selectedFaculty = faculty;
            var specialtys = FacadeApi.GetSpecialtys(faculty.IdFaculty);
            _view.ShowSpecialtys(specialtys);
        }

        public void SelectSpecialt(Specialty specialty)
        {
            _selectedSpecialty = specialty;
            var courses = FacadeApi.GetCourses(
                _selectedFaculty.IdFaculty,
                FacadeApi.ConvertIdsToString(specialty.IdsSpecialty)
                );
            _view.ShowCourses(courses);
        }

        public void SelectCourses(Courses cours)
        {
            _selectedCourse = cours;
            var groups = FacadeApi.GetGroups(
                _selectedFaculty.IdFaculty,
                _selectedCourse.IdCourse,
                FacadeApi.ConvertIdsToString(_selectedSpecialty.IdsSpecialty)
                );
            _view.ShowGroup(groups);
        }

        public void SelectGroup(Group group)
        {
            _selectedGroup = group;
            _view.SetButtoneShowClickable(true);
        }

        internal void ClickeButtoneShow()
        {
            var intent = new Intent(_view, typeof(ScheduleActivity));
            var jsonGroup = JsonConvert.ConvertToJson(_selectedGroup);
            intent.PutExtra(ScheduleActivity.DataKeyGroupsJson, jsonGroup);
            _view.StartActivity(intent);
        }
    }

    /// <summary>
    /// Нужен для представления DTO как String в Spinner.
    /// Приэтом иметь возможность получить DTO.
    /// </summary>
    /// <typeparam name="T">DTO для DTOAdapter</typeparam>
    internal abstract class DtoAdapter<T> : BaseAdapter
    {
        private readonly IList<T> _objects;
        private readonly Context _context;

        protected DtoAdapter(IList<T> objects, Context context)
        {
            _context = context;
            _objects = objects;
        }

        public override int Count => _objects.Count;

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
            var text = position == 0 ? "select" : ConvertDtoInString(_objects[position]);
            var textView = new TextView(_context)
            {
                Text = text
            };
            return textView;
        }

        public T GetObject(int position)
        {
            return _objects[position];
        }

        public abstract string ConvertDtoInString(T t);

    }

    internal class FacultyAdapter : DtoAdapter<Faculty>
    {
        /// <summary>
        /// Добавляет пустой DTO в objects в позицию 0.
        /// Так как заместо позиции 0 стоит элемент по умолчанию.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="context"></param>
        public FacultyAdapter(IList<Faculty> objects, Context context) : base(objects, context)
        {
            objects.Insert(0, new Faculty());
        }

        public override string ConvertDtoInString(Faculty t)
        {
            return t.Name;
        }
    }

    internal class SpecialtyAdapter : DtoAdapter<Specialty>
    {
        public SpecialtyAdapter(IList<Specialty> objects, Context context) : base(objects, context)
        {
            objects.Insert(0, new Specialty());
        }

        public override string ConvertDtoInString(Specialty t)
        {
            return t.NameSpeciality;
        }
    }

    internal class CoursesAdapter : DtoAdapter<Courses>
    {
        public CoursesAdapter(IList<Courses> objects, Context context) : base(objects, context)
        {
            objects.Insert(0, new Courses());
        }

        public override string ConvertDtoInString(Courses t)
        {
            return t.NameCourse;
        }
    }

    internal class GroupAdapter : DtoAdapter<Group>
    {
        public GroupAdapter(IList<Group> objects, Context context) : base(objects, context)
        {
            objects.Insert(0, new Group());
        }

        public override string ConvertDtoInString(Group t)
        {
            return t.NameGroup;
        }
    }

}