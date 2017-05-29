using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Bukep.Sheduler.Controllers;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler
{
    //TODO: Вынести логику в Controller
    [Activity(Label = "ScheduleActivity")]
    public class ScheduleActivity : Activity
    {
        private Schedule _schedule;
        private const string Tag = "ScheduleActivity";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ScheduleLayout);

            _schedule = new Schedule(this);
            _schedule.Update();
        }

        internal void ShowGroupLesson(IList<GroupLesson> groupLessons)
        {
            var linearLayout = FindViewById<LinearLayout>(Resource.Id.liner_layout);
            linearLayout.RemoveAllViews();

            foreach (var item in groupLessons)
            {
                linearLayout.AddView(CreateCardLesson(item));
            }
        }

        private View CreateCardLesson(GroupLesson lesson)
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