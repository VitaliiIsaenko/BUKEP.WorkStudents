﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.View.factory;

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

        public void SetPeriodForToolbar(string name)
        {
            _toolbarPeriod.Text = name;
        }

        public void SetTodayForToolbar(string today)
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

            var mainFactory = new MainFactory(this);

            foreach (var item in lessonOnDay)
            {
                linearLayout.AddView(mainFactory.CreateLinearLessonOnDays(item));
            }
        }


    }
}