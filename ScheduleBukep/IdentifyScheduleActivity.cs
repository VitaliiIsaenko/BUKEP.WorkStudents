using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Bukep.ShedulerApi;
using Bukep.ShedulerApi.apiDTO;
using System.Collections.Generic;
using Android;
using Bukep.Sheduler.Controllers;

namespace Bukep.Sheduler
{
    /// <summary>
    /// Данное Activity используется как форма идентификации для студентов.
    /// Предоставляет пошаговый доступ к расписанию, состоящий из шагов:
    /// -   выбор факультета
    /// -	выбор специальности
    /// -	выбор курса
    /// -	выбор группы
    /// После выполнение всех шагов появляется кнопка «показать».
    /// Нажатие на эту кнопку открывает расписание по заданным параметрам.
    /// </summary>
    [Activity(Label = "ScheduleBukep", MainLauncher = true, Icon = "@drawable/icon")]
    public class IdentifyScheduleActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            IdentifySchedule identifySchedule = new IdentifySchedule(this);
            identifySchedule.Update();
        }

        internal void ShowFaculty(List<Faculty> faculties)
        {
            ArrayAdapter<Faculty> adapter = new ArrayAdapter<Faculty>(this, Android.Resource.Layout.SimpleSpinnerItem, faculties);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinnerFaculty = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            spinnerFaculty.Adapter = adapter;
        } 
    }
}

