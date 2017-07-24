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
        private readonly IList<TItem> _objects;
        private readonly Activity _activity;

        public delegate string ConvertItemInString(TItem t);

        private readonly ConvertItemInString _convertItemInString;

        public ItemAdapter(IList<TItem> objects, Activity activity, ConvertItemInString convertItemInString)
        {
            _activity = activity;
            this._convertItemInString = convertItemInString;
            _objects = objects;
            //Так как за место 0 ItemAdapter в методе GetView будет возвращать свой элемент.
            if (_objects.Any())
                _objects.Insert(0, _objects[0]);
        }

        public override int Count => _objects.Count;

        public TItem GetObject(int position)
        {
            return _objects[position];
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
                if (_convertItemInString == null)
                    throw new ArgumentException(
                        "Parameter convertDtoInString cannot be null.");
                view.Text = _convertItemInString(_objects[position]);
            }
            return view;
        }
    }
}