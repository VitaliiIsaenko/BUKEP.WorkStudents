using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;

namespace Bukep.Sheduler.logic
{
    /// <summary>
    /// Нужен для представления списка объектов в виде View.
    /// </summary>
    /// <typeparam name="TItem">Список объектов который нужно отобразить как View</typeparam>
    public class ItemAdapter<TItem> : BaseAdapter
    {
        private readonly IList<TItem> _items;

        public delegate string ConvertItemInString(TItem t);

        private readonly Func<TItem, string> _convertItemInString;

        public ItemAdapter(IList<TItem> items, Func<TItem, string> convertItemInString)
        {
            _items = items;
            _convertItemInString = convertItemInString;
        }

        public override int Count => _items.Count;

        public TItem GetObject(int position)
        {
            return _items[position];
        }

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            string ItemName = _convertItemInString(GetObject(position));
            return new TextView(parent.Context) { Text = ItemName };
        }
    }
}