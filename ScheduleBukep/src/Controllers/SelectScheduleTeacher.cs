using System;
using Android.Content;
using Bukep.Sheduler.controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.logic.extension;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.Controllers
{
    internal class SelectScheduleTeacher : SelectItem
    {

        public SelectScheduleTeacher(SelectItemActivity view) : base(view)
        {
        }

        public override void Update()
        {
            InitChoicePulpit();
        }

        private void InitChoicePulpit()
        {
            InitChoice(
                DataProvider.GetPulpits(),
                InitChoiceTeacher,
                pulpit => pulpit.NamePulpit);
        }

        private void InitChoiceTeacher(Pulpit pulpit)
        {
            InitChoice(
                DataProvider.GetTeacher(pulpit.IdPulpit),
                StartScheduleActivity,
                teacher => teacher.Fio);
        }

        protected void StartScheduleActivity(Teacher teacher)
        {
            var intent = new Intent(_view, typeof(ScheduleActivity));
            intent.PutObject(ScheduleForTeacher.IntentKeyTeacherJson, teacher);
            intent.PutDateTime(Schedule.IntentKey.DateLessonStart.ToString(), DateTime.Today);
            intent.PutDateTime(Schedule.IntentKey.DateLessonEnd.ToString(), DateTime.Today);
            intent.PutExtra(IntentKeyDateSelectItemType, (int)SelectItemType.SelectScheduleTeacher);

            _view.StartActivity(intent);
        }
    }
}