using System;
using System.Collections.Generic;
using Android.Widget;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.Controllers
{
    /// <summary>
    /// Общий контроллер для выбора расписания
    /// </summary>
    public abstract class SelectItem : Controller
    {
        protected readonly SelectItemActivity _view;
        public const string IntentKeyDateSelectItemType = "SchedulesType";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activity">Activity которое будет отображать рассписание</param>
        protected SelectItem(SelectItemActivity activity) : base(activity)
        {
            _view = activity;
        }

        protected void InitChoice<TItems>(IEnumerable<TItems> items, Action<TItems> selectItem,
            Func<TItems, string> convertInString)
        {
            InitChoice(new List<TItems>(items ?? new List<TItems>()), selectItem, convertInString);
        }

        /// <summary>
        /// Иницилизировать выбор элемента.
        /// </summary>
        /// <typeparam name="TItems">Тип объекта, который нужно отобразить</typeparam>
        /// <param name="items">Список объектов</param>
        /// <param name="selectItem">Действие обработки выбранного элемента</param>
        /// <param name="convertInString">Функция преобразующая объект в строку</param>
        protected void InitChoice<TItems>(List<TItems> items, Action<TItems> selectItem,
            Func<TItems, string> convertInString)
        {
            string[] namesItem = items.ConvertAll(convertInString.Invoke).ToArray();

            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(
                _view, Resource.Layout.ChooseItem, namesItem);

            _view.ShowChoiceItem(arrayAdapter, selectIndex => selectItem(items[selectIndex]));
        }
    }
}