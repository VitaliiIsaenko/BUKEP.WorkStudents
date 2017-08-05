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
        private Teacher _selectedTeacher;
        private ItemAdapter<Pulpit> _itemAdapterPulpit;
        private ItemAdapter<Teacher> _itemAdapterTeacher;

        public IdentifyScheduleTeacher(IdentifyScheduleActivity view) : base(view)
        {
        }

        public override void Update()
        {
            base.Update();

            InitChoicePulpit();
            _itemAdapterPulpit.Items = DataProvider.GetPulpits();

            InitChoiceTeacher();
        }

        private void InitChoicePulpit()
        {
            _itemAdapterPulpit = new ItemAdapter<Pulpit>(_view,
                pulpit => pulpit.NamePulpit
            );

            ItemChoice<Pulpit> itemChoice = new ItemChoice<Pulpit>(
                _itemAdapterPulpit, SelectPulpit, _view);
            _view.ShowItems(itemChoice);
        }

        private void InitChoiceTeacher()
        {
            _itemAdapterTeacher = new ItemAdapter<Teacher>(_view,
                teacher => teacher.Fio
            );

            ItemChoice<Teacher> itemChoice = new ItemChoice<Teacher>(
                _itemAdapterTeacher, SelectTeacher, _view);
            _view.ShowItems(itemChoice);
        }

        private void SelectPulpit(Pulpit pulpit)
        {
            _itemAdapterTeacher.Items = DataProvider.GetTeacher(pulpit.IdPulpit);
        }

        private void SelectTeacher(Teacher teacher)
        {
            _selectedTeacher = teacher;
            _view.SetButtoneShowClickable(true);
        }

        protected override void ClickeButtoneShow(object sender, EventArgs e)
        {
            var intent = new Intent(_view, typeof(ScheduleActivity));
            var jsonTeacher = JsonConvert.ConvertToJson(_selectedTeacher);
            intent.PutExtra(ScheduleForTeacher.IntentKeyTeacherJson, jsonTeacher);

            var today = DateTime.Today.ToString(Api.DateTimeFormat);
            intent.PutExtra(Schedule.IntentKeyDateLessonStart, today);
            intent.PutExtra(Schedule.IntentKeyDateLessonEnd, today);
            intent.PutExtra(IntentKeyDateSchedulesType, (int)SchedulesType.ForTeacher);

            _view.StartActivity(intent);
        }
    }
}