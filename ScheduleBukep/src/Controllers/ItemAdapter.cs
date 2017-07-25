using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;

namespace Bukep.Sheduler.Controllers
{
    /// <summary>
    /// Нужен для представления TItem как String в Spinner.
    /// При этом иметь возможность получить TItem.
    /// </summary>
    /// <typeparam name="TItem">Отображается в Spinner</typeparam>
    public class ItemAdapter<TItem> : BaseAdapter
    {
        private IList<TItem> _items = new List<TItem>();
        public IList<TItem> Items {
            get => _items;
            set
            {
                _items = value ?? throw new ArgumentNullException(nameof(value));
                SetItemInFirstList();
                NotifyDataSetChanged();
            }
        }

        private readonly Activity _activity;

        public delegate string ConvertItemInString(TItem t);

        private readonly Func<TItem, string> _convertItemInString;

        public ItemAdapter(Activity activity, Func<TItem, string> convertItemInString)
        {
            _activity = activity;
            _convertItemInString = convertItemInString;
        }

        /// <summary>
        /// Так как за место 0 элемента, ItemAdapter в методе GetView() будет возвращать свой элемент.
        /// </summary>
        private void SetItemInFirstList()
        {
            if (Items.Any())
                Items.Insert(0, Items[0]);
        }

        public override int Count => Items.Count;

        public TItem GetObject(int position)
        {
            return Items[position];
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
            var view = (TextView) _activity.LayoutInflater.Inflate(
                Resource.Layout.ItemForSpinner,
                null, false
            );
            if (position == 0)
            {
                var textForView = _activity.GetText(Resource.String.selectFromeList);
                view.Text = textForView;
            }
            else
            {
                view.Text = _convertItemInString(Items[position]);
            }
            return view;
        }
    }
}