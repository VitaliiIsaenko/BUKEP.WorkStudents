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

        /// <summary>
        /// Отобразить выбор элемента на Activity.
        /// </summary>
        /// <typeparam name="TItem">Тип элементов.</typeparam>
        /// <param name="adapter">Адаптер с элементами которые нужно отобразить.</param>
        /// <param name="selectItem">Действие которое вызовется при выборе элемента.</param>
        public void ShowChoiceItem<TItem>(ItemAdapter<TItem> adapter, Action<TItem> selectItem)
        {
            LinearLayout contenerListView = FindViewById<LinearLayout>(Resource.Id.ContenerForListItemChoices);

            ListView listView = new ListView(this);
            contenerListView.RemoveAllViews();
            contenerListView.AddView(listView);

            listView.ItemClick += null;
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
            SchedulesType schedulesType = (SchedulesType) schedulesTypeInt;
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
                        "Не удалось выбрать тип расписания. SchedulesType = " + schedulesTypeInt);
            }

            _controller.Update();
        }
    }
}