﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Bukep.Sheduler.controllers.factory;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.logic.extension;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.View
{
    /// <summary>
    ///  Используется для выбора элемента из списка.
    /// </summary>
    [Activity(Icon = "@drawable/icon")]
    public class SelectItemActivity : NavigationActivity
    {
        private const string Tag = "IdentifyScheduleActivity";
        public const string IntentKeyDateSelectItemType = "SchedulesType";
        private SelectItem _controller;

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
        /// <param name="adapter">Адаптер с элементами которые нужно отобразить.</param>
        /// <param name="selectItem">Действие которое вызовется при выборе элемента.</param>
        public void ShowChoiceItem(ArrayAdapter adapter, Action<int> selectItem)
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
                selectItem.Invoke(posotion);
            };
        }

        private void InitController()
        {
            SelectItemType selectItemType = Intent.GetObject<SelectItemType>(
            IntentKeyDateSelectItemType);
            _controller = SelectItemFactory.CreateSelectItem(this, selectItemType);
            _controller.Update();
        }

        public static void StartSelectItemActivity(Activity view, SelectItemType selectItemType)
        {
            var intent = new Intent(view, typeof(SelectItemActivity));
            intent.PutObject(
                IntentKeyDateSelectItemType,
                JsonConvert.ConvertToJson(selectItemType));
            view.StartActivity(intent);
        }
    }
}