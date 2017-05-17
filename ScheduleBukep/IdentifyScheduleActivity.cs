using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Bukep.ShedulerApi;
using Bukep.ShedulerApi.apiDTO;
using System.Collections.Generic;

namespace ScheduleBukep
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
            SetContentView(Resource.Layout.Main);
            List<string> dataForSpinnerFaculty = GetDataForSpinnerFaculty();

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, dataForSpinnerFaculty);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinnerFaculty = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            spinnerFaculty.Adapter = adapter;
        }

        private static List<string> GetDataForSpinnerFaculty()
        {
            FacadeAPI api = new FacadeAPI();
            List<Faculty> faculties = api.GetFaculties();

            List<string> dataForSpinnerFaculty = new List<string>();

            foreach (var faculty in faculties)
            {
                dataForSpinnerFaculty.Add(faculty.Name);
            }

            return dataForSpinnerFaculty;
        }
    }
}

