using System;
using Android.App;
using Android.Widget;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.View.factory;

namespace Bukep.Sheduler.logic
{
    /// <summary>
    /// Агрегирует список элементов для их дальнейшего выбора пользователем.
    /// Предоставляет View для отображения списка элементов.
    /// Позволяет добавить в конструктор метод для обработки выбранного элемента.
    /// </summary>
    /// <typeparam name="TItem">Тип элемента</typeparam>
    public class ItemChoice<TItem>
    {
        private readonly Action<TItem> _selectItem;
        private readonly ItemAdapter<TItem> _adapter;
        public string ChoiceItemsName { get; set; } = "Список элементов";
        public Android.Views.View View { get; set; }
        public Spinner Spinner { get; set; }

        //TODO: заменить Action<TItem> selectItem на событие(Event)
        public ItemChoice(ItemAdapter<TItem> adapter, Action<TItem> selectItem, Activity activity)
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

        private void SelectedItemInSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            //TODO: возможно я могу взять item из _selectItem, а не использовать _adapter
            TItem item = _adapter.GetObject(posotion);
            _selectItem.Invoke(item);
        }
    }
}