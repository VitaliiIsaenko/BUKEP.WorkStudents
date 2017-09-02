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
            
            var selectOption = new SelectOption<Pulpit>();
            selectOption.SetItems(pulpits)
                .SetOnClickItem(InitChoiceTeacher)
                .SetConvertInString(pulpit => pulpit.Info.Value);
            View.ShowSelectItem(selectOption);
        }

        private void InitChoiceTeacher(Pulpit pulpit)
        {
            IList<Teacher> teachers = DataProvider.GetTeacher(pulpit.Info.Key);
            
            var selectOption = new SelectOption<Teacher>();
            selectOption.SetItems(teachers)
                .SetOnClickItem(StartScheduleActivity)
                .SetConvertInString(teacher => teacher.Fio);
            View.ShowSelectItem(selectOption);
        }

        protected void StartScheduleActivity(Teacher teacher)
        {
            ScheduleActivity.StartScheduleActivity(View, teacher);
        }
    }
}