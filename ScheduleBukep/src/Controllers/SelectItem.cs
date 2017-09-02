using System;
using System.Collections.Generic;
using Android.Widget;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.Controllers
{
    /// <summary>
    /// Общий контроллер для выбора расписания
    /// </summary>
    public abstract class SelectItem : Controller<SelectItemActivity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view">Activity которое будет отображать рассписание</param>
        protected SelectItem(SelectItemActivity view) : base(view)
        {
        }


        /// <summary>
        /// Настройки для списка выбора элемента.
        /// </summary>
        /// <typeparam name="TItems">Тип объекта, который нужно отобразить.</typeparam>
        public class SelectOption<TItems>
        {
            public SelectOption()
            {
                Items = new List<TItems>();
                OnClickItem = items => throw new ArgumentException("Set OnClickItem!");
                ConvertInString = items => items.ToString();
            }

            /// <summary>
            /// Список элементов.
            /// </summary>
            private List<TItems> Items;

            /// <summary>
            /// Вызывает при выборе элемента.
            /// Нужен для получения выбранного элемента.
            /// </summary>
            private Action<TItems> OnClickItem;

            /// <summary>
            /// Функция преобразующая объект в строку.
            /// Нужен для отображения элемента в списке в виде строки.
            /// </summary>
            private Func<TItems, string> ConvertInString;

            /// <summary>
            /// Сообщение отображается когда нет элементов.
            /// </summary>
            public string MessagesIfNotElement { get; set; }

            public SelectOption<TItems> SetItems(List<TItems> itemses)
            {
                Items = itemses;
                return this;
            }

            public SelectOption<TItems> SetItems(IEnumerable<TItems> itemses)
            {
                if (itemses != null)
                {
                    Items = new List<TItems>(itemses);
                }
                return this;
            }

            public SelectOption<TItems> SetOnClickItem(Action<TItems> onClickItem)
            {
                OnClickItem = onClickItem;
                return this;
            }

            public SelectOption<TItems> SetConvertInString(Func<TItems, string> convertInString)
            {
                ConvertInString = convertInString;
                return this;
            }

            public SelectOption<TItems> SetMessagesIfNotElement(string messagesIfNotElement)
            {
                MessagesIfNotElement = messagesIfNotElement;
                return this;
            }

            /// <summary>
            /// Перевести все элементы в строки.
            /// </summary>
            /// <returns></returns>
            private string[] ConvertItemsToString()
            {
                return Items.ConvertAll(ConvertInString.Invoke).ToArray();
            }

            public ArrayAdapter CreateAdapter(BaseActivity view)
            {
                return new ArrayAdapter<string>(
                    view,
                    Resource.Layout.ChooseItem,
                    ConvertItemsToString());
            }

            public void SelectIndex(int selectIndex)
            {
                OnClickItem(Items[selectIndex]);
            }
        }
    }
}