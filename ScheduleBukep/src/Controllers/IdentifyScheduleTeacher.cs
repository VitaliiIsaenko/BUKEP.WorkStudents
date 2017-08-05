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
    internal class IdentifyScheduleTeacher : IdentifySchedule
    {

        public IdentifyScheduleTeacher(IdentifyScheduleActivity view) : base(view)
        {
        }

        public override void Update()
        {
            InitChoicePulpit();
        }

        private void InitChoicePulpit()
        {
            ItemAdapter<Pulpit> adapterPulpit = new ItemAdapter<Pulpit>(
                DataProvider.GetPulpits(),
                pulpit => pulpit.NamePulpit
            );

            _view.ChoiceItem(adapterPulpit, InitChoiceTeacher);
        }

        private void InitChoiceTeacher(Pulpit pulpit)
        {
            ItemAdapter<Teacher> itemAdapterTeacher = new ItemAdapter<Teacher>(
                DataProvider.GetTeacher(pulpit.IdPulpit),
                teacher => teacher.Fio
            );

            _view.ChoiceItem(itemAdapterTeacher, StartScheduleActivity);
        }

        protected void StartScheduleActivity(Teacher teacher)
        {
            var intent = new Intent(_view, typeof(ScheduleActivity));
            var jsonTeacher = JsonConvert.ConvertToJson(teacher);
            intent.PutExtra(ScheduleForTeacher.IntentKeyTeacherJson, jsonTeacher);

            var today = DateTime.Today.ToString(Api.DateTimeFormat);
            intent.PutExtra(Schedule.IntentKeyDateLessonStart, today);
            intent.PutExtra(Schedule.IntentKeyDateLessonEnd, today);
            intent.PutExtra(IntentKeyDateSchedulesType, (int)SchedulesType.ForTeacher);

            _view.StartActivity(intent);
        }
    }
}