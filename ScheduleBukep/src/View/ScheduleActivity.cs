using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
using Bukep.Sheduler.Controllers;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.View
{
    [Activity()]
    public class ScheduleActivity : NavigationActivity
    {
        private Schedule _schedule;
        private bool _isClickImageFavorites;
        private TextView _toolbarGroop;
        private TextView _toolbarDate;
        private TextView _toolbarPeriod;
        private const string LessonOnDayNameFormat = "dddd";
        private const string Tag = "ScheduleActivity";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ScheduleLayout);

            InitNavigationView();
            var imageFavorites = FindViewById<ImageView>(Resource.Id.toolbarImageFavorites);
            imageFavorites.Click += ClickImageFavorites;

            _toolbarPeriod = FindViewById<TextView>(Resource.Id.toolbarSchedulesPeriod);
            _toolbarPeriod.Click += ClickSchedulesPeriod;

            _toolbarDate = FindViewById<TextView>(Resource.Id.toolbarDate);
            _toolbarGroop = FindViewById<TextView>(Resource.Id.toolbarGroop);

            _schedule = new Schedule(this);
            _schedule.Update();
        }

        public void SetPeriodName(string name)
        {
            _toolbarPeriod.Text = name;
        }

        public void SetToday(string today)
        {
            _toolbarDate.Text = today;
        }

        public void SetGroopName(string name)
        {
            _toolbarGroop.Text = name;
        }

        private void ClickSchedulesPeriod(object sender, EventArgs e)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetTitle(GetString(Resource.String.select_period))
                .SetItems(
                    Resources.GetStringArray(Resource.Array.schedules_periods),
                    ClickListPeriod
                )
                .Create()
                .Show();
        }

        private void ClickListPeriod(object sender, DialogClickEventArgs e)
        {
            Log.Debug(Tag, $"ClickListPeriod() Which = {e.Which}");
            switch (e.Which)
            {
                case 0:
                    _schedule.ChoosePeriodOneDay();
                    break;
                case 1:
                    _schedule.ChoosePeriodThreeDay();
                    break;
                case 2:
                    _schedule.ChoosePeriodWeek();
                    break;
            }
        }

        private void ClickImageFavorites(object sender, EventArgs e)
        {
            var imageFavorites = (ImageView) sender;
            imageFavorites.SetImageResource(_isClickImageFavorites
                ? Resource.Drawable.favorites_empty
                : Resource.Drawable.favorites);
            _isClickImageFavorites = !_isClickImageFavorites;
        }

        internal void ShowLessonOnDay(List<LessonOnDay> lessonOnDay)
        {
            var linearLayout = FindViewById<LinearLayout>(Resource.Id.liner_layout);
            linearLayout.RemoveAllViews();

            foreach (var item in lessonOnDay)
            {
                linearLayout.AddView(CreateLinearLessonOnDays(item));
            }
        }

        //TODO: Вынести в отдельный класс
        private Android.Views.View CreateLinearLessonOnDays(LessonOnDay lessonOnDay)
        {
            var linearLessonOnDays =
                (LinearLayout) LayoutInflater.Inflate(Resource.Layout.LessonOnDayView, null, false);
            var lessonOnDayName = linearLessonOnDays.FindViewById<TextView>(Resource.Id.LessonOnDayName);
            lessonOnDayName.Text = lessonOnDay.DateLesson.ToString(LessonOnDayNameFormat);

            var lessonOnDaysView = linearLessonOnDays.FindViewById<LinearLayout>(Resource.Id.LessonOnDays);
            lessonOnDaysView.RemoveAllViews();

            foreach (var lesson in lessonOnDay.Lessons)
            {
                lessonOnDaysView.AddView(CreateCardLesson(lesson));
            }
            return linearLessonOnDays;
        }

        //TODO: Вынести в отдельный класс
        private Android.Views.View CreateCardLesson(GroupLesson groupLesson)
        {
            var card = (CardView) LayoutInflater.Inflate(Resource.Layout.CardViewLesson, null, false);
            var nameLesson = card.FindViewById<TextView>(Resource.Id.nameLesson);
            nameLesson.Text = groupLesson.Discipline.Value;

            var timeStartLesson = card.FindViewById<TextView>(Resource.Id.timeStartLesson);
            var timeLessonStartLesson = groupLesson.TimeLesson.StartLesson;
            timeStartLesson.Text = DateTime.Parse(timeLessonStartLesson).ToString("hh:mm");

            var timeEndLesson = card.FindViewById<TextView>(Resource.Id.timeEndLesson);
            var timeLessonEndLesson = groupLesson.TimeLesson.EndLesson;
            timeEndLesson.Text = DateTime.Parse(timeLessonEndLesson).ToString("hh:mm");

            var number = card.FindViewById<TextView>(Resource.Id.number);
            number.Text = groupLesson.Lesson.Value;

            var typeLesson = card.FindViewById<TextView>(Resource.Id.typeLesson);
            typeLesson.Text = groupLesson.TypeLesson.Value;

            var nameTeacher = card.FindViewById<TextView>(Resource.Id.nameTeacher);
            nameTeacher.Text = "";
            foreach (var teacher in groupLesson.Teachers)
            {
                nameTeacher.Text += teacher.Value+" ";
            }

            var nameAudience = card.FindViewById<TextView>(Resource.Id.nameAudience);
            nameAudience.Text = "";
            foreach (var auditory in groupLesson.Auditory)
            {
                nameAudience.Text += auditory.Value+" ";
            }
            
            return card;
        }
    }
}