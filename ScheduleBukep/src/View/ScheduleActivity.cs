using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using Bukep.Sheduler.controllers;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View.factory;

namespace Bukep.Sheduler.View
{
    [Activity()]
    public class ScheduleActivity : NavigationActivity
    {
        private Schedule _controller;
        private bool _isClickImageFavorites;
        private TextView _toolbarGroop;
        private TextView _toolbarDate;
        private TextView _toolbarPeriod;
        private const string Tag = "ScheduleActivity";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ScheduleLayout);

            InitView();
            InitController();
        }

        private void InitView()
        {
            InitNavigationView();
            InitImageFavorites();
            InitToolbarPeriod();

            _toolbarDate = FindViewById<TextView>(Resource.Id.toolbarDate);
            _toolbarGroop = FindViewById<TextView>(Resource.Id.toolbarGroop);
        }

        private void InitToolbarPeriod()
        {
            _toolbarPeriod = FindViewById<TextView>(Resource.Id.toolbarSchedulesPeriod);
            _toolbarPeriod.Click += ClickSchedulesPeriod;
        }

        private void InitImageFavorites()
        {
            var imageFavorites = FindViewById<ImageView>(Resource.Id.toolbarImageFavorites);
            imageFavorites.Click += ClickImageFavorites;
        }

        private void InitController()
        {
            int schedulesTypeInt = Intent.GetIntExtra(IdentifySchedule.IntentKeyDateSchedulesType, 1);
            SchedulesType schedulesType = (SchedulesType)schedulesTypeInt;
            switch (schedulesType)
            {
                case SchedulesType.ForStudent:
                    _controller = new ScheduleForStudent(this);
                    break;
                case SchedulesType.ForTeacher:
                    _controller = new ScheduleForTeacher(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        "Не удалось выбрать тип расписания. SchedulesType = " + schedulesTypeInt);
            }
            _controller.Update();
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
                    _controller.SchedulesManager.ChoosePeriodOneDay();
                    break;
                case 1:
                    _controller.SchedulesManager.ChoosePeriodThreeDay();
                    break;
                case 2:
                    _controller.SchedulesManager.ChoosePeriodWeek();
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