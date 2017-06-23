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
        private const string LessonOnDayNameFormat = "dddd";
        private const string Tag = "ScheduleActivity";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ScheduleLayout);

            InitNavigationView();
            var imageFavorites = FindViewById<ImageView>(Resource.Id.toolbarImageFavorites);
            imageFavorites.Click += ClickImageFavorites;

            var period = FindViewById<TextView>(Resource.Id.toolbarSchedulesPeriod);
            period.Click += ClickSchedulesPeriod;

            _schedule = new Schedule(this);
            _schedule.Update();
        }

        private void ClickSchedulesPeriod(object sender, EventArgs e)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetTitle(GetString(Resource.String.select_period))
                .SetItems(
                Resources.GetStringArray(Resource.Array.schedules_period),
                ClickListPeriod
                )
                .Create()
                .Show();
        }

        private void ClickListPeriod(object sender, DialogClickEventArgs e)
        {
            Log.Debug(Tag, $"ClickListPeriod() Which = {e.Which}");
            switch(e.Which)
            {
                case 0:
                    _schedule.ChoosePeriodOneDay(); break;
                case 1:
                    _schedule.ChoosePeriodThreeDay(); break;
                case 2:
                    _schedule.ChoosePeriodWeek(); break; 
            }
        }

        private void ClickImageFavorites(object sender, EventArgs e)
        {
            var imageFavorites = (ImageView)sender;
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
            var linearLessonOnDays = (LinearLayout)LayoutInflater.Inflate(Resource.Layout.LessonOnDayView, null, false);
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
        private Android.Views.View CreateCardLesson(Lesson lesson)
        {
            var card = (CardView) LayoutInflater.Inflate(Resource.Layout.CardViewLesson, null, false);
            var nameLesson = card.FindViewById<TextView>(Resource.Id.nameLesson);
            nameLesson.Text = lesson.NameDiscipline;

            var timeStartLesson = card.FindViewById<TextView>(Resource.Id.timeStartLesson);
            timeStartLesson.Text = lesson.TimeStartLesson;

            var timeEndLesson = card.FindViewById<TextView>(Resource.Id.timeEndLesson);
            timeEndLesson.Text = lesson.TimeEndLesson;

            var number = card.FindViewById<TextView>(Resource.Id.number);
            number.Text = lesson.NameLesson;

            var typeLesson = card.FindViewById<TextView>(Resource.Id.typeLesson);
            typeLesson.Text = lesson.TypeLesson;

            var nameTeacher = card.FindViewById<TextView>(Resource.Id.nameTeacher);
            nameTeacher.Text = lesson.FioTeacher;

            var nameAudience = card.FindViewById<TextView>(Resource.Id.nameAudience);
            nameAudience.Text = lesson.NameAuditory;

            return card;
        }
    }
}