using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPI
{
    public class OverrideGetFaculty : FacultiesService
    {
        public OverrideGetFaculty(HttpRequstHelper httpRequestHelper, JsonConvert jsonConvert) : base(httpRequestHelper, jsonConvert)
        {
        }

        public OverrideGetFaculty()
        {
        }

        public override List<Faculty> GetFaculties(int year, int idFilial)
        {
            var Faculties = base.GetFaculties(year, idFilial);
            var faculty = new List<Faculty>(Faculties);
            for (int i = 0; i < Faculties.Count; i++)
            {
                if(Faculties[i].IsActiveSchedule==false)
                {
                    faculty.Remove(Faculties[i]);
                }
            }
            return faculty;
        }
    }
}
