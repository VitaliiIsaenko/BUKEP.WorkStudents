using System;
using System.Collections.Generic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.Controllers
{
    internal class IdentifyScheduleTeacher : IdentifySchedule
    {

        private Pulpit _selectedPulpit;
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
            _itemAdapterPulpit.Items = _dateShedules.GetPulpit();

            InitChoiceTeacher();
        }

        private void InitChoicePulpit()
        {
            _itemAdapterPulpit = new ItemAdapter<Pulpit>(_view,
                pulpit => pulpit.NamePulpit
            );

            ChoiceItem<Pulpit> choiceItem = new ChoiceItem<Pulpit>(
                _itemAdapterPulpit, SelectPulpit, _view);
            _view.ShowItems(choiceItem);
        }

        private void InitChoiceTeacher()
        {
            _itemAdapterTeacher = new ItemAdapter<Teacher>(_view,
                teacher => teacher.Fio
            );

            ChoiceItem<Teacher> choiceItem = new ChoiceItem<Teacher>(
                _itemAdapterTeacher, SelectTeacher, _view);
            _view.ShowItems(choiceItem);
        }

        private void SelectPulpit(Pulpit pulpit)
        {
            _selectedPulpit = pulpit;
            _itemAdapterTeacher.Items = _dateShedules.GetTeacher(pulpit.IdPulpit);
        }

        private void SelectTeacher(Teacher teacher)
        {
            _selectedTeacher = teacher;
            _view.SetButtoneShowClickable(true);
        }

        protected override void ClickeButtoneShow(object sender, EventArgs e)
        {
            
        }
    }
}