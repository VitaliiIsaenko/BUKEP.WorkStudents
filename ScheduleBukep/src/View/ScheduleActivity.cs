using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
using Bukep.Sheduler.controllers;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.logic.extension;
using Bukep.Sheduler.logic.period;
using Bukep.Sheduler.View.factory;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.View
{
    [Activity]
    public class ScheduleActivity : BaseActivity
    {
        private Schedule _controller;
        private TextView _toolbarGroop;
        private TextView _toolbarDate;
        private TextView _toolbarPeriod;
        private SwitchCompat _switchNextWeek;
        public ImageView ImageFavorites { get; set; }

        public PeriodsEnum SelectedPeriods { get; private set; } = PeriodsEnum.PeriodOneDay;

        private const string Tag = "ScheduleActivity";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //TODO: переделать отображение как в статье https://habrahabr.ru/post/237101/
            SetContentView(Resource.Layout.ScheduleLayout);

            InitView();
            InitController();
        }

        private void InitView()
        {
            InitToolbarPeriod();
            
            _switchNextWeek = FindViewById<SwitchCompat>(Resource.Id.toolbar_switch_schedules);
            _switchNextWeek.Click += delegate
            {
                _controller.UpdatePeriods();
                _controller.Update();
            };

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
            SelectItemType selectItemType = 
                Intent.GetObject<SelectItemType>(SelectItemActivity.IntentKeySelectItemType);
            //TODO: вынести в фабрику
            switch (selectItemType)
            {
                case SelectItemType.SelectScheduleStudent:
                    _controller = new ScheduleForStudent(this);
                    break;
                case SelectItemType.SelectScheduleTeacher:
                    _controller = new ScheduleForTeacher(this);
                    break;
                case SelectItemType.SelectFavorites:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        "Не удалось выбрать тип расписания. SchedulesType = " + selectItemType);
            }
            _controller.Update();
        }

        public void SetPeriodName(string name)
        {
            _toolbarPeriod.Text = name;
        }

        public void SetDateTimePeriod(string today)
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
            var periodsEnum = (PeriodsEnum)e.Which;
            SelectedPeriods = periodsEnum;
            Log.Debug(Tag, $"ClickListPeriod() Which = {periodsEnum}");
            
            _controller.UpdatePeriods();
            _controller.Update();
        }

        /// <summary>
        /// Выбрана ли для отображения следующая неделя.
        /// </summary>
        /// <returns>True если выбрана следующая неделя.</returns>
        public bool IsShowNextWeek()
        {
            return _switchNextWeek.Checked;
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

        internal static void StartScheduleActivity(BaseActivity view, Teacher teacher)
        {
            var intent = new Intent(view, typeof(ScheduleActivity));

            intent.PutObject(SelectItemActivity.IntentKeySelectItemType, SelectItemType.SelectScheduleTeacher);
            intent.PutObject(ScheduleForTeacher.IntentKeyTeacherJson, teacher);
            intent.PutDateTime(Schedule.IntentKey.DateLessonStart.ToString(), DateTime.Today);
            intent.PutDateTime(Schedule.IntentKey.DateLessonEnd.ToString(), DateTime.Today);

            view.StartActivity(intent);
        }

        internal static void StartScheduleActivity(BaseActivity view, Group group)
        {
            var intent = new Intent(view, typeof(ScheduleActivity));

            intent.PutObject(SelectItemActivity.IntentKeySelectItemType, SelectItemType.SelectScheduleStudent);
            intent.PutObject(ScheduleForStudent.IntentKeyGroupsJson, group);
            intent.PutDateTime(Schedule.IntentKey.DateLessonStart.ToString(), DateTime.Today);
            intent.PutDateTime(Schedule.IntentKey.DateLessonEnd.ToString(), DateTime.Today);

            
            
            view.StartActivity(intent);
        }
    }
}