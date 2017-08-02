using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;

namespace Bukep.Sheduler.View
{
    /// <summary>
    ///  Данное Activity используется как форма выбора элементов из выпадающих списков.
    /// </summary>
    [Activity(Icon = "@drawable/icon")]
    public class IdentifyScheduleActivity : NavigationActivity
    {
        private const string Tag = "IdentifyScheduleActivity";

        public Button ShowSchedulesButtone { get; set; }

        private IdentifySchedule _controller;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            ShowSchedulesButtone = FindViewById<Button>(Resource.Id.buttoneShow);
            InitNavigationView();

            InitController();
        }

        //TODO: add doc
        public void ShowItems<TItem>(ItemChoice<TItem> itemChoice)
        {
            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linear_layout_choose_item);
            layout.AddView(itemChoice.View);
        }

        private void InitController()
        {
            int schedulesTypeInt = Intent.GetIntExtra(IdentifySchedule.IntentKeyDateSchedulesType, 1);
            SchedulesType schedulesType = (SchedulesType)schedulesTypeInt;
            switch (schedulesType)
            {
                case SchedulesType.ForStudent:
                    _controller = new IdentifyScheduleStudent(this);
                    break;
                case SchedulesType.ForTeacher:
                    _controller = new IdentifyScheduleTeacher(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        "Не удалось выбрать тип расписания. SchedulesType = "+schedulesTypeInt);
            }

            _controller.Update();
        }

        public void SetButtoneShowClickable(bool clickable)
        {
            ShowSchedulesButtone.Clickable = clickable;
        }
    }
}