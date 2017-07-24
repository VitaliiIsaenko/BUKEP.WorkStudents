using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Bukep.Sheduler.Controllers;

namespace Bukep.Sheduler.View
{
    /// <summary>
    ///     Данное Activity используется как форма идентификации для студентов.
    ///     Предоставляет пошаговый доступ к расписанию, состоящий из шагов:
    ///     -   выбор факультета
    ///     -	выбор специальности
    ///     -	выбор курса
    ///     -	выбор группы
    ///     После выполнение всех шагов появляется кнопка «показать».
    ///     Нажатие на эту кнопку открывает расписание по заданным параметрам.
    /// </summary>
    [Activity(Icon = "@drawable/icon")]
    public class IdentifyScheduleActivity : NavigationActivity
    {
        private const string Tag = "IdentifyScheduleActivity";

        private Button _showSchedulesButtone;

        private readonly List<Spinner> _spinners = new List<Spinner>();

        private IdentifySchedule _controller;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            InitShowButtone();
            InitController();
            InitNavigationView();
        }

        //TODO: ItemAdapter<TItem>.ConvertItemInString заменить на Func<string, Titem>
        public void Show<TItem>(IList<TItem> items, ItemAdapter<TItem>.ConvertItemInString convertItemInString,
            Action<TItem> selectedItem)
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            ItemAdapter<TItem> itemAdapter = new ItemAdapter<TItem>(items, this, convertItemInString);
            InitItemSpinner(spinner, itemAdapter, selectedItem);

            _spinners.Add(spinner);
        }

        private void InitItemSpinner<TItem>(Spinner spinner, ItemAdapter<TItem> itemAdapter, Action<TItem> selectedItem)
        {
            spinner.Adapter = itemAdapter;
            ItemSelector<TItem> itemSelector = new ItemSelector<TItem>(itemAdapter, selectedItem, _spinners);
            spinner.ItemSelected += itemSelector.SelectedItemInSpinner;
        }

        private void InitController()
        {
            _controller = new IdentifySchedule(this);
            _controller.Update();
        }

        private void InitShowButtone()
        {
            _showSchedulesButtone = FindViewById<Button>(Resource.Id.buttoneShow);
            _showSchedulesButtone.Click += _controller.ClickeButtoneShow;
        }

        public void SetButtoneShowClickable(bool clickable)
        {
            _showSchedulesButtone.Clickable = clickable;
        }
    }
}