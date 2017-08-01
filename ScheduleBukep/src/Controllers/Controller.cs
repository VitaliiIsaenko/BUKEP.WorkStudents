﻿using System;
using System.Collections.Generic;
using System.Net;
using Android.Content;
using Android.Net;
using Android.Util;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service;
using ScheduleBukepAPITest;

namespace Bukep.Sheduler.Controllers
{
    public abstract class Controller
    {
        private const string Tag = "Controller";

        protected readonly DateShedules _dateShedules;
        
        protected Controller(BaseActivity activity)
        {
            _dateShedules = new DateShedules(activity);
        }

        public abstract void Update();
    }

    public class DateShedules
    {
        private readonly BaseActivity _activity;
        private readonly JsonConvert _jsonConvert = new JsonConvert();
        private const string Tag = "DateShedules";

        //TODO: добавить ApiFactory
        private readonly Api _api = new Api(
            new FilteringFacultiesService(new FakeHttpRequstHelper(), new JsonConvert()),
            new SchedulesService(new FakeHttpRequstHelper(), new JsonConvert())
        );

        private ConnectivityManager _connectivityManager;

        public DateShedules(BaseActivity activity)
        {
            _activity = activity;
        }

        public IList<Faculty> GetFaculties()
        {
            if (!CheckInternetConnect())
            {
                FailedInternetConnect();
            }
            try
            {
                return _api.GetFaculties();
            }
            catch (WebException e)
            {
                Log.Error(Tag, e.ToString());
                FailedInternetConnect();
                return new List<Faculty>();
            }
        }

        public IList<Specialty> GetSpecialtys(int idFaculty)
        {
            if (!CheckInternetConnect())
            {
                FailedInternetConnect();
            }
            try
            {
                return _api.GetSpecialtys(idFaculty);
            }
            catch (WebException e)
            {
                Log.Error(Tag, e.ToString());
                FailedInternetConnect();
                return new List<Specialty>();
            }
        }

        public IList<Group> GetGroups(int idFaculty, int idCourse, IList<int> idsSpecialty)
        {
            if (!CheckInternetConnect())
            {
                FailedInternetConnect();
            }

            try
            {
                return _api.GetGroups(idFaculty, idCourse, idsSpecialty);
            }
            catch (WebException e)
            {
                Log.Error(Tag, e.ToString());
                FailedInternetConnect();
                return new List<Group>();
            }
        }

        public IList<Course> GetCourses(int idFaculty, IList<int> idsSpecialty)
        {
            if (!CheckInternetConnect())
            {
                FailedInternetConnect();
            }

            try
            {
                return _api.GetCourses(idFaculty, idsSpecialty);
            }
            catch (WebException e)
            {
                Log.Error(Tag, e.ToString());
                FailedInternetConnect();
                return new List<Course>();
            }
        }

        public IList<Pulpit> GetPulpit()
        {
            if (!CheckInternetConnect())
            {
                FailedInternetConnect();
            }

            try
            {
                return _api.GetPulpits();
            }
            catch (WebException e)
            {
                Log.Error(Tag, e.ToString());
                FailedInternetConnect();
                return new List<Pulpit>();
            }
        }

        public IList<Teacher> GetTeacher(int idPulpit)
        {
            if (!CheckInternetConnect())
            {
                FailedInternetConnect();
            }

            try
            {
                return _api.GetTeacher(idPulpit);
            }
            catch (WebException e)
            {
                Log.Error(Tag, e.ToString());
                FailedInternetConnect();
                return new List<Teacher>();
            }
        }

        public IList<Lesson> GetGroupLessons(IList<int> idsSheduleGroup,
            DateTime dateFrom, DateTime dateTo)
        {
            if (!CheckInternetConnect())
            {
                FailedInternetConnect();
            }

            try
            {
                return _api.GetGroupLessons(idsSheduleGroup, dateFrom, dateTo);
            }
            catch (WebException e)
            {
                Log.Error(Tag, e.ToString());
                FailedInternetConnect();
                return new List<Lesson>();
            }
        }

        public T ConvertTo<T>(string jsonGroup)
        {
            return _jsonConvert.ConvertTo<T>(jsonGroup);
        }

        public string ConvertToJson(Group selectedGroup)
        {
            return _jsonConvert.ConvertToJson(selectedGroup);
        }

        private bool CheckInternetConnect()
        {
            if (_connectivityManager == null)
            {
                _connectivityManager = (ConnectivityManager)_activity.GetSystemService(Context.ConnectivityService);
            }
            return _connectivityManager.ActiveNetworkInfo.IsConnected;
        }

        public void FailedInternetConnect()
        {
            //TODO: move in res
            _activity.ShowError("Отсутствует подключение к интернету.");
        }
    }
}