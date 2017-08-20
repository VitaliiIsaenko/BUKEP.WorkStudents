using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.Controllers
{
    public abstract class Schedule : Controller
    {
        private const string Tag = "Schedule";
        protected const string ToolbarDateFormat = "ddd, dd MMM";

        /// <summary>
        /// Используется для получения данных из Intent.
        /// </summary>
        public enum IntentKey
        {
            DateLessonStart,
            DateLessonEnd
        }

        /// <summary>
        /// Используется для отслужевания состояния кнопки добавить в избранное.
        /// </summary>
        private bool _isClickImageFavorites;

        protected readonly ScheduleActivity view;
        protected readonly Intent intent;

        /// <summary>
        /// Используется для настройки периода отображения расписания.
        /// </summary>
        public Periods Periods { get; }

        protected Schedule(ScheduleActivity view) : base(view)
        {
            this.view = view;
            intent = view.Intent;
            Periods = new Periods(this.view, this);

            view.AddListenerForImageFavorites(ClickImageFavorites);
        }

        public override void Update()
        {
            var lessonOnDays = LessonOnDay.Parse(GetLessons());
            view.ShowLessonOnDay(lessonOnDays);
            view.SetTodayForToolbar(DateTime.Today.ToString(ToolbarDateFormat));
        }

        protected abstract IList<Lesson> GetLessons();

        /// <summary>
        /// Сохраняет расписание или удаляет его из избранного.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickImageFavorites(object sender, EventArgs e)
        {
            var imageFavorites = (ImageView) sender;
            ChangeImageForFavorites(imageFavorites);
            _isClickImageFavorites = !_isClickImageFavorites;

            if (_isClickImageFavorites)
            {
                SaveScheduleInFavorites();
            }
            else
            {
                DeleteScheduleInFavorites();
            }
            
        }

        protected abstract void DeleteScheduleInFavorites();

        protected abstract void SaveScheduleInFavorites();

        /// <summary>
        /// Используется для смены картинки при нажатии 
        /// на кнопку добавить в избранное(звёздочка).
        /// </summary>
        /// <param name="imageFavorites"></param>
        private void ChangeImageForFavorites(ImageView imageFavorites)
        {
            imageFavorites.SetImageResource(
                _isClickImageFavorites
                    ? Resource.Drawable.favorites_empty
                    : Resource.Drawable.favorites);
        }
    }
}