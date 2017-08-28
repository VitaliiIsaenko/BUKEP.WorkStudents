using System.Collections.Generic;
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
            IList<Pulpit> pulpits = DataProvider.GetPulpits();
            InitSelect(
                pulpits,
                InitChoiceTeacher,
                pulpit => pulpit.Info.Value);
        }

        private void InitChoiceTeacher(Pulpit pulpit)
        {
            IList<Teacher> teachers = DataProvider.GetTeacher(pulpit.Info.Key);
            InitSelect(
                teachers,
                StartScheduleActivity,
                teacher => teacher.Fio);
        }

        protected void StartScheduleActivity(Teacher teacher)
        {
            ScheduleActivity.StartScheduleActivity(View, teacher);
        }
    }
}