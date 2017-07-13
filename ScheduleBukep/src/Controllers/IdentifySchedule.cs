using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Widget;
using Android.Views;
using Android.Content;
using Bukep.Sheduler.View;
using Java.Interop;
using Java.Lang;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.Controllers
{
    internal class IdentifySchedule : Controller
    {
        private readonly IdentifyScheduleActivity _view;

        private Faculty _selectedFaculty;
        private Specialty _selectedSpecialty;
        private Course _selectedCourse;
        private Group _selectedGroup;

        public IdentifySchedule(IdentifyScheduleActivity view) : base(view)
        {
            _view = view;
        }

        public override void Update()
        {
            _view.SetButtoneShowClickable(false);


            _view.SpecialtysSpinnerEnabled(false);
            _view.CourseSpinnerEnabled(false);
            _view.GroupSpinnerEnabled(false);

            var faculties = GetFaculties();
            _view.ShowFaculty(faculties);
        }

        public void SelectFaculty(Faculty faculty)
        {
            _selectedFaculty = faculty;
            var specialtys = GetSpecialtys(faculty.IdFaculty);
            _view.ShowSpecialtys(specialtys);

            _view.SpecialtysSpinnerEnabled(true);
            _view.CourseSpinnerEnabled(false);
            _view.GroupSpinnerEnabled(false);
        }

        public void SelectSpecialt(Specialty specialty)
        {
            _selectedSpecialty = specialty;
            var courses = GetCourses(
                _selectedFaculty.IdFaculty,
                FacadeApi.ConvertIdsToString(specialty.IdsSpecialty)
            );
            _view.ShowCourses(courses);

            _view.SpecialtysSpinnerEnabled(true);
            _view.CourseSpinnerEnabled(true);
            _view.GroupSpinnerEnabled(false);
        }

        public void SelectCourses(Course cours)
        {
            _selectedCourse = cours;
            var groups = GetGroups(
                _selectedFaculty.IdFaculty,
                _selectedCourse.IdCourse,
                FacadeApi.ConvertIdsToString(_selectedSpecialty.IdsSpecialty)
            );
            _view.ShowGroup(groups);

            _view.SpecialtysSpinnerEnabled(true);
            _view.CourseSpinnerEnabled(true);
            _view.GroupSpinnerEnabled(true);
        }

        public void SelectGroup(Group group)
        {
            _selectedGroup = group;
            _view.SetButtoneShowClickable(true);
        }

        internal void ClickeButtoneShow()
        {
            var intent = new Intent(_view, typeof(ScheduleActivity));
            var jsonGroup = ConvertToJson(_selectedGroup);
            intent.PutExtra(Schedule.IntentKeyGroupsJson, jsonGroup);

            var today = DateTime.Today.ToString(FacadeApi.DateTimeFormat);
            intent.PutExtra(Schedule.IntentKeyDateLessonStart, today);
            intent.PutExtra(Schedule.IntentKeyDateLessonEnd, today);

            _view.StartActivity(intent);
        }
    }

    /// <summary>
    /// Нужен для представления DTO как String в Spinner.
    /// Приэтом иметь возможность получить DTO.
    /// </summary>
    /// <typeparam name="T">DTO для DTOAdapter</typeparam>
    internal class DtoAdapter<T> : BaseAdapter
    {
        private readonly IList<T> _objects;
        private readonly Activity _activity;

        internal delegate string ConvertDtoInString(T t);

        private readonly ConvertDtoInString _convertDtoInString;

        public DtoAdapter(IList<T> objects, Activity activity, ConvertDtoInString convertDtoInString)
        {
            _activity = activity;
            this._convertDtoInString = convertDtoInString;
            _objects = objects;
            //Так как за место 0 DtoAdapter в методе GetView будет возвращать свой элемент.
            if (_objects.Any())
                _objects.Insert(0, _objects[0]);
        }

        public override int Count => _objects.Count;

        public T GetObject(int position)
        {
            return _objects[position];
        }

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            var view = (TextView) _activity.LayoutInflater.Inflate(
                Resource.Layout.ItemForSpinner,
                null, false
            );
            if (position == 0)
            {
                var textForView = _activity.GetText(Resource.String.selectFromeList);
                view.Text = textForView;
            }
            else
            {
                if (_convertDtoInString == null)
                    throw new ArgumentException(
                        "Parameter convertDtoInString cannot be null.");
                view.Text = _convertDtoInString(_objects[position]);
            }
            return view;
        }
    }
}