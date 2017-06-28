using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.Views;
using Android.Content;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.Controllers
{
    internal class IdentifySchedule : Controller
    {
        private readonly IdentifyScheduleActivity _view;
        private readonly FacadeApi _facadeApi = new FacadeApi();
        private readonly JsonConvert _jsonConvert = new JsonConvert();

        private Faculty _selectedFaculty;
        private Specialty _selectedSpecialty;
        private Courses _selectedCourse;
        private Group _selectedGroup;

        public IdentifySchedule(IdentifyScheduleActivity view)
        {
            _view = view;
        }

        public override void Update()
        {        
            _view.SetButtoneShowClickable(false);


            _view.SpecialtysSpinnerEnabled(false);
            _view.CourseSpinnerEnabled(false);
            _view.GroupSpinnerEnabled(false);

            var faculties = _facadeApi.GetFaculties();
            _view.ShowFaculty(faculties);
        }

        public void SelectFaculty(Faculty faculty)
        {
            _selectedFaculty = faculty;
            var specialtys = _facadeApi.GetSpecialtys(faculty.IdFaculty);
            _view.ShowSpecialtys(specialtys);

            _view.SpecialtysSpinnerEnabled(true);
            _view.CourseSpinnerEnabled(false);
            _view.GroupSpinnerEnabled(false);
        }

        public void SelectSpecialt(Specialty specialty)
        {
            _selectedSpecialty = specialty;
            var courses = _facadeApi.GetCourses(
                _selectedFaculty.IdFaculty,
                FacadeApi.ConvertIdsToString(specialty.IdsSpecialty)
                );
            _view.ShowCourses(courses);

            _view.SpecialtysSpinnerEnabled(true);
            _view.CourseSpinnerEnabled(true);
            _view.GroupSpinnerEnabled(false);
        }

        public void SelectCourses(Courses cours)
        {
            _selectedCourse = cours;
            var groups = _facadeApi.GetGroups(
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
            var jsonGroup = _jsonConvert.ConvertToJson(_selectedGroup);
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
    internal abstract class DtoAdapter<T> : BaseAdapter
    {
        private readonly IList<T> _objects;
        private readonly Activity _activity;

        protected DtoAdapter(IList<T> objects, Activity activity)
        {
            _activity = activity;
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

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            
            var view = (TextView) _activity.LayoutInflater.Inflate(Resource.Layout.ItemForSpinner, null, false);
            if (position == 0)
            {
                var textForView = _activity.GetText(Resource.String.selectFromeList);
                view.Text = textForView;              
            }
            else
            {
                view.Text = ConvertDtoInString(_objects[position]);
            }
            return view;
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
        /// <param name="activity"></param>
        public FacultyAdapter(IList<Faculty> objects, Activity activity) : base(objects, activity)
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
        public SpecialtyAdapter(IList<Specialty> objects, Activity activity) : base(objects, activity)
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
        public CoursesAdapter(IList<Courses> objects, Activity activity) : base(objects, activity)
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
        public GroupAdapter(IList<Group> objects, Activity activity) : base(objects, activity)
        {
            objects.Insert(0, new Group());
        }

        public override string ConvertDtoInString(Group t)
        {
            return $"{t.NameGroup} {t.NameTypeShedule}";
        }
    }

}