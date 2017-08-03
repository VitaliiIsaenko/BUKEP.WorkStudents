using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bukep.Sheduler.logic;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.View.factory
{
    /// <summary>
    /// Фабрика для создания View при при помощи Inflate
    /// </summary>
    public class MainFactory
    {
        private readonly Activity _activity;
        private const string LessonStartAndEndFormat = "HH:mm";
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


        private Android.Views.View CreateCardLesson(Lesson lesson)
        {
            var card = Inflate<CardView>(Resource.Layout.CardViewLesson);
            var nameLesson = card.FindViewById<TextView>(Resource.Id.nameLesson);
            nameLesson.Text = lesson.Discipline.Value;

            var timeStartLesson = card.FindViewById<TextView>(Resource.Id.timeStartLesson);
            var timeLessonStartLesson = lesson.TimeLesson.StartLesson;
            timeStartLesson.Text = timeLessonStartLesson.ToString(LessonStartAndEndFormat);

            var timeEndLesson = card.FindViewById<TextView>(Resource.Id.timeEndLesson);
            var timeLessonEndLesson = lesson.TimeLesson.EndLesson;
            timeEndLesson.Text = timeLessonEndLesson.ToString(LessonStartAndEndFormat);

            var number = card.FindViewById<TextView>(Resource.Id.number);
            number.Text = lesson.LessonInfo.Value;

            var typeLesson = card.FindViewById<TextView>(Resource.Id.typeLesson);
            typeLesson.Text = lesson.TypeLesson.Value;

            var nameTeacher = card.FindViewById<TextView>(Resource.Id.nameTeacher);
            nameTeacher.Text = "";
            foreach (var teacher in lesson.Teachers)
            {
                nameTeacher.Text += teacher.Value + " ";
            }

            var nameAudience = card.FindViewById<TextView>(Resource.Id.nameAudience);
            nameAudience.Text = "";
            foreach (var auditory in lesson.Auditory)
            {
                nameAudience.Text += auditory.Value + " ";
            }

            return card;
        }

        private T Inflate<T>(int res) where T : Android.Views.View
        {
            return (T) _activity.LayoutInflater.Inflate(res, null, false);
        }

        public Tuple<LinearLayout, Spinner> CreateSpinner(string choiceItemsName)
        {
            LinearLayout choiceItem = Inflate<LinearLayout>(Resource.Layout.ChoiceItem);
            TextView nameView = choiceItem.FindViewById<TextView>(Resource.Id.choiceItemsName);
            nameView.Text = choiceItemsName;
            Spinner spinner = choiceItem.FindViewById<Spinner>(Resource.Id.choiceItemsSpinner);
            return new Tuple<LinearLayout, Spinner>(choiceItem, spinner);
        }
    }
}