using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.View.factory
{
    /// <summary>
    /// Фабрика для создания View при при помощи Inflate
    /// </summary>
    public class MainFactory
    {
        private readonly Activity _activity;
        private const string LessonOnDayNameFormat = "dddd";

        public MainFactory(Activity activity)
        {
            this._activity = activity;
        }

        public Android.Views.View CreateLinearLessonOnDays(LessonOnDay lessonOnDay)
        {
            var linearLessonOnDays =
                Inflate<LinearLayout>(Resource.Layout.LessonOnDayView);
            var lessonOnDayName =
                linearLessonOnDays.FindViewById<TextView>(Resource.Id.LessonOnDayName);
            lessonOnDayName.Text =
                lessonOnDay.DateLesson.ToString(LessonOnDayNameFormat);

            var lessonOnDaysView = linearLessonOnDays.FindViewById<LinearLayout>(Resource.Id.LessonOnDays);
            lessonOnDaysView.RemoveAllViews();

            foreach (var lesson in lessonOnDay.Lessons)
            {
                lessonOnDaysView.AddView(CreateCardLesson(lesson));
            }
            return linearLessonOnDays;
        }


        private Android.Views.View CreateCardLesson(GroupLesson groupLesson)
        {
            var card = Inflate<CardView>(Resource.Layout.CardViewLesson);
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
                nameTeacher.Text += teacher.Value + " ";
            }

            var nameAudience = card.FindViewById<TextView>(Resource.Id.nameAudience);
            nameAudience.Text = "";
            foreach (var auditory in groupLesson.Auditory)
            {
                nameAudience.Text += auditory.Value + " ";
            }

            return card;
        }

        private T Inflate<T>(int res) where T : Android.Views.View
        {
            return (T) _activity.LayoutInflater.Inflate(res, null, false);
        }
    }
}