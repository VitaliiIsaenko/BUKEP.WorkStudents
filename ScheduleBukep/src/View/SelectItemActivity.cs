﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Bukep.Sheduler.controllers.factory;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.logic.extension;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.View
{
    /// <summary>
    ///     Используется для выбора элемента из списка.
    /// </summary>
    [Activity(Icon = "@drawable/icon")]
    public class SelectItemActivity : BaseActivity
    {
        private const string Tag = "IdentifyScheduleActivity";

        public const string IntentKeySelectItemType = "SchedulesType";

        //TODO: переместить _controller в BaseActivity. И добавить туда abstrect метод InitController(). (А лучше пусть controller саи себя init)
        private SelectItem _controller;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            InitController();
        }

        /// <summary>
        ///     Отобразить выбор элемента на Activity.
        /// </summary>
        /// <param name="option">Настройки для списка элементов</param>
        public void ShowSelectItem<TItems>(SelectItem.SelectOption<TItems> option)
        {
            var adapter = option.CreateAdapter(this);
            if (adapter.Count == 0)
            {
                HideContenerForListView();
                ShowTextIfNotElement(option.MessagesIfNotElement);
                return;
            }
            var listView = InitListView();

            listView.ItemClick += null;
            listView.Adapter = adapter;
            listView.ItemClick += (sender, args) =>
            {
                option.SelectIndex(args.Position);
            };

            var contenerForListView = InitContenerForListView();
            contenerForListView.AddView(listView);
        }


        /// <summary>
        ///     Инициализация текст в центре экрана.
        ///     Сообщает о том что нет элементов для выбора.
        /// </summary>
        /// <param name="messagesIfNotElement"></param>
        private void ShowTextIfNotElement(string messagesIfNotElement = null)
        {
            var textElementNotPresent = FindViewById<TextView>(Resource.Id.text_element_not_present);
            if (!string.IsNullOrEmpty(messagesIfNotElement))
            {
                textElementNotPresent.Text = messagesIfNotElement;
            }
            textElementNotPresent.Visibility = ViewStates.Visible;
        }

        /// <summary>
        ///     Инициализация контенера для списка элементов.
        /// </summary>
        /// <returns></returns>
        private LinearLayout InitContenerForListView()
        {
            var contenerListView = FindViewById<LinearLayout>(Resource.Id.contener_for_list_item_choices);
            contenerListView.RemoveAllViews();
            return contenerListView;
        }

        private void HideContenerForListView()
        {
            var contenerForListView = InitContenerForListView();
            contenerForListView.Visibility = ViewStates.Gone;
        }

        /// <summary>
        ///     Инициализация списка элементов.
        /// </summary>
        /// <returns></returns>
        private ListView InitListView()
        {
            var layoutParams = new AbsListView.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent
            );
            var listView = new ListView(this) {LayoutParameters = layoutParams};
            return listView;
        }

        private void InitController()
        {
            var selectItemType = Intent.GetObject<SelectItemType>(
                IntentKeySelectItemType);
            _controller = SelectItemFactory.CreateSelectItem(this, selectItemType);
            _controller.Update();
        }

        public static void StartSelectItemActivity(Activity view, SelectItemType selectItemType)
        {
            var intent = new Intent(view, typeof(SelectItemActivity));
            intent.PutObject(
                IntentKeySelectItemType,
                JsonConvert.ConvertToJson(selectItemType));
            view.StartActivity(intent);
        }
    }
}