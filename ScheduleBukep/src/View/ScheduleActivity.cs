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
using Bukep.Sheduler.logic.extension;
using Bukep.Sheduler.View.factory;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.View
{
    [Activity]
    public class ScheduleActivity : NavigationActivity
    {
        private Schedule _controller;
        private TextView _toolbarGroop;
        private TextView _toolbarDate;
        private TextView _toolbarPeriod;
        public ImageView ImageFavorites { get; set; }
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
            InitToolbarPeriod();

            ImageFavorites = FindViewById<ImageView>(Resource.Id.toolbarImageFavorites);
            _toolbarDate = FindViewById<TextView>(Resource.Id.toolbarDate);
            _toolbarGroop = FindViewById<TextView>(Resource.Id.toolbarGroop);
        }

        private void InitToolbarPeriod()
        {
            _toolbarPeriod = FindViewById<TextView>(Resource.Id.toolbarSchedulesPeriod);
            _toolbarPeriod.Click += ClickSchedulesPeriod;
        }

        private void InitController()
        {
            int schedulesTypeInt = Intent.GetIntExtra(SelectItemActivity.IntentKeyDateSelectItemType, 1);
            SelectItemType selectItemType = (SelectItemType) schedulesTypeInt;
            //TODO: вынести в фабрику
            switch (selectItemType)
            {
                case SelectItemType.SelectScheduleStudent:
                    _controller = new ScheduleForStudent(this);
                    break;
                case SelectItemType.SelectScheduleTeacher:
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
                    ClickListPeriod)
                .Create()
                .Show();
        }

        private void ClickListPeriod(object sender, DialogClickEventArgs e)
        {
            Log.Debug(Tag, $"ClickListPeriod() Which = {e.Which}");
            //TODO: не очень хороший подход, магические числа в switch
            switch (e.Which)
            {
                case 0:
                    _controller.Periods.SelectPeriodOneDay();
                    break;
                case 1:
                    _controller.Periods.SelectPeriodThreeDay();
                    break;
                case 2:
                    _controller.Periods.SelectPeriodWeek();
                    break;
            }
        }

        /// <summary>
        /// Используется для отображения уроков.
        /// </summary>
        /// <param name="lessonOnDay"></param>
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

        internal static void StartScheduleActivity(SelectItemActivity view, Teacher teacher)
        {
            var intent = new Intent(view, typeof(ScheduleActivity));

            intent.PutObject(SelectItemActivity.IntentKeyDateSelectItemType, SelectItemType.SelectScheduleTeacher);
            intent.PutObject(ScheduleForTeacher.IntentKeyTeacherJson, teacher);
            intent.PutDateTime(Schedule.IntentKey.DateLessonStart.ToString(), DateTime.Today);
            intent.PutDateTime(Schedule.IntentKey.DateLessonEnd.ToString(), DateTime.Today);

            view.StartActivity(intent);
        }

        internal static void StartScheduleActivity(SelectItemActivity view, Group group)
        {
            var intent = new Intent(view, typeof(ScheduleActivity));

            intent.PutObject(SelectItemActivity.IntentKeyDateSelectItemType, SelectItemType.SelectFavoritesGroup);
            intent.PutObject(ScheduleForStudent.IntentKeyGroupsJson, group);
            intent.PutDateTime(Schedule.IntentKey.DateLessonStart.ToString(), DateTime.Today);
            intent.PutDateTime(Schedule.IntentKey.DateLessonEnd.ToString(), DateTime.Today);

            view.StartActivity(intent);
        }
    }
}