using System;
using Android.App;
using Android.Widget;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.View.factory;

namespace Bukep.Sheduler.View
{
    public class ChoiceItem<TItem>
    {
        private readonly Action<TItem> _selectItem;
        private readonly ItemAdapter<TItem> _adapter;
        public string ChoiceItemsName { get; set; } = "Список элементов";
        public Android.Views.View View { get; set; }
        public Spinner Spinner { get; set; }

        public ChoiceItem(ItemAdapter<TItem> adapter, Action<TItem> selectItem, Activity activity)
        {
            _selectItem = selectItem;
            _adapter = adapter;
            IniteView(activity);
            Spinner.Adapter = _adapter;
        }

        private void IniteView(Activity activity)
        {
            Tuple<LinearLayout, Spinner> tuple = new MainFactory(activity).CreateSpinner(ChoiceItemsName);
            View = tuple.Item1;
            Spinner = tuple.Item2;
            Spinner.ItemSelected += SelectedItemInSpinner;
        }

        public void SelectedItemInSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            TItem item = _adapter.GetObject(posotion);
            _selectItem.Invoke(item);
        }
    }
}