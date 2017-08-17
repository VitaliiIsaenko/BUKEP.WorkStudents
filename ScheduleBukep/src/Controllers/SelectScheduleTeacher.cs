using System;
using Android.Content;
using Bukep.Sheduler.controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

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
            var jsonTeacher = JsonConvert.ConvertToJson(teacher);
            intent.PutExtra(ScheduleForTeacher.IntentKeyTeacherJson, jsonTeacher);

            var today = DateTime.Today.ToString(Api.DateTimeFormat);
            intent.PutExtra(Schedule.IntentKeyDateLessonStart, today);
            intent.PutExtra(Schedule.IntentKeyDateLessonEnd, today);
            intent.PutExtra(IntentKeyDateSelectItemType, (int)SelectItemType.SelectScheduleTeacher);

            _view.StartActivity(intent);
        }
    }
}