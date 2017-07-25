using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Bukep.Sheduler.Controllers;

namespace Bukep.Sheduler.View
{
    /// <summary>
    ///  Данное Activity используется как форма выбора элементов из выпадающих списков.
    /// </summary>
    [Activity(Icon = "@drawable/icon")]
    public partial class IdentifyScheduleActivity : NavigationActivity
    {
        private const string Tag = "IdentifyScheduleActivity";

        public Button ShowSchedulesButtone { get; set; }

        private readonly List<Spinner> _spinners = new List<Spinner>();

        private IdentifySchedule _controller;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            ShowSchedulesButtone = FindViewById<Button>(Resource.Id.buttoneShow);

            InitController();
            InitNavigationView();
        }

        //TODO: add doc
        public void ShowItems<TItem>(ChoiceItem<TItem> choiceItem)
        {
            Spinner spinner = choiceItem.Spinner;
            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linear_layout_choose_item);
            layout.AddView(choiceItem.View);
            spinner.ItemSelected += (sender, args) => UpdateEnabledSpinners((Spinner) sender);
            _spinners.Add(spinner);
        }

        /// <summary>
        /// Включает следующий выпадающий список от выбранного, и выключает все последующие
        /// </summary>
        /// <param name="selectedSpinner">Выбранный выпадающий список</param>
        private void UpdateEnabledSpinners(Spinner selectedSpinner)
        {
            var index = _spinners.FindIndex(spinner => spinner.Equals(selectedSpinner));
            _spinners[index++].Enabled = true;
            for (var i = index + 2; i < _spinners.Count; i++)
            {
                _spinners[i].Enabled = false;
            }
        }

        private void InitController()
        {
            _controller = new IdentifySchedule(this);
            _controller.Update();
        }

        public void SetButtoneShowClickable(bool clickable)
        {
            ShowSchedulesButtone.Clickable = clickable;
        }
    }
}