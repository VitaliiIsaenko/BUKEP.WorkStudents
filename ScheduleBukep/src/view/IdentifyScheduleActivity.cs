using System;
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

        private IdentifySchedule _controller;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            InitNavigationView();
            InitController();
        }

        //TODO: add doc
        public void ChoiceItem<TItem>(ItemAdapter<TItem> adapter, Action<TItem> selectItem)
        {
            ListView listView = FindViewById<ListView>(Resource.Id.ListItemChoices);

            listView.Adapter = adapter;
            listView.ItemClick += (sender, args) =>
            {
                var posotion = args.Position;
                TItem item = adapter.GetObject(posotion);
                selectItem.Invoke(item);
            };
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
    }
}