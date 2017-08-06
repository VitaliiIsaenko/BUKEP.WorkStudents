using System;
using System.Collections.Generic;
using Android.Widget;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.Controllers
{
    internal abstract class IdentifySchedule : Controller
    {
        protected readonly IdentifyScheduleActivity _view;
        public const string IntentKeyDateSchedulesType = "SchedulesType";

        protected IdentifySchedule(IdentifyScheduleActivity activity) : base(activity)
        {
            _view = activity;
        }

        protected void InitChoice<TItems>(IEnumerable<TItems> items, Action<TItems> selectItem,
            Func<TItems, string> convertInString)
        {
            InitChoice(new List<TItems>(items), selectItem, convertInString);
        }

        protected void InitChoice<TItems>(List<TItems> items, Action<TItems> selectItem, Func<TItems, string> convertInString)
        {
            string[] namePulpits = items.ConvertAll(convertInString.Invoke).ToArray();

            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(
                _view, Resource.Layout.ChooseItem, namePulpits);

            _view.ShowChoiceItem(arrayAdapter, selectIndex => selectItem(items[selectIndex]));
        }
    }
}