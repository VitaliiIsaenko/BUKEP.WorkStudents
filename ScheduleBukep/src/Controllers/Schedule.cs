using System;
using System.Collections.Generic;
using Android.Content;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.Controllers
{
    public abstract class Schedule : Controller<ScheduleActivity>
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
        protected readonly Intent intent;

        /// <summary>
        /// Используется для настройки периода отображения расписания.
        /// </summary>
        public Periods Periods { get; }

        protected Schedule(ScheduleActivity view) : base(view)
        {
            intent = view.Intent;
            Periods = new Periods(View, this);

            view.ImageFavorites.Click += ClickImageFavorites;
        }

        public override void Update()
        {
            var lessonOnDays = LessonOnDay.Parse(GetLessons());
            View.ShowLessonOnDay(lessonOnDays);
            View.SetTodayForToolbar(DateTime.Today.ToString(ToolbarDateFormat));
        }

        protected abstract IList<Lesson> GetLessons();

        /// <summary>
        /// Сохраняет расписание или удаляет его из избранного.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickImageFavorites(object sender, EventArgs e)
        {
            _isClickImageFavorites = !_isClickImageFavorites;
            ChangeImageForFavorites(_isClickImageFavorites);

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
        /// <param name="clickImageFavorites"></param>
        protected void ChangeImageForFavorites(bool clickImageFavorites)
        {
            View.ImageFavorites.SetImageResource(
                clickImageFavorites
                    ? Resource.Drawable.favorites
                    : Resource.Drawable.favorites_empty);
        }
    }
}