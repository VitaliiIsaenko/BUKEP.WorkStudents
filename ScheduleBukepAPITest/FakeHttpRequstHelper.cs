using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service.paremeters;

namespace ScheduleBukepAPITest
{
    /// <summary>
    /// Используется как замена стандартному HttpRequstHelper.
    /// FakeHttpRequstHelper не использует Api для получения данных,
    /// а берёт их из файлов.
    /// </summary>
    public class FakeHttpRequstHelper : HttpRequstHelper
    {
        private readonly List<MethodApi> _methodsApi = Enum.GetValues(typeof(MethodApi)).Cast<MethodApi>().ToList();

        public override string ExecuteGet(string url)
        {
            return GetDataForMethodApi(url);
        }


        public override string ExecutePost(string url, IList<int> bodyForPost)
        {
            return GetDataForMethodApi(url);
        }

        /// <summary>
        /// Определяет какой MethodApi содержится в url 
        /// и возвращает соответствующий ему Json
        /// </summary>
        /// <param name="url"></param>
        /// <returns>В зависимости от MethodApi возвращает соответствующий ему Json</returns>
        private string GetDataForMethodApi(string url)
        {
            MethodApi methodApi = _methodsApi.FirstOrDefault(method => url.Contains(method.ToString()));
            byte[] bytesRes;
            switch (methodApi)
            {
                case MethodApi.GetFaculties:
                    bytesRes = Properties.Resources.GetFaculties;
                    break;
                case MethodApi.GetSpecialtys:
                    bytesRes = Properties.Resources.GetSpecialtys;
                    break;
                case MethodApi.GetCourses:
                    bytesRes = Properties.Resources.GetCourses;
                    break;
                case MethodApi.GetGroups:
                    bytesRes = Properties.Resources.GetGroups;
                    break;
                case MethodApi.GetPulpit:
                    bytesRes = Properties.Resources.GetPulpit;
                    break;
                case MethodApi.GetTeacher:
                    bytesRes = Properties.Resources.GetTeacher;
                    break;
                case MethodApi.GetGroupLessons:
                    bytesRes = Properties.Resources.GetGroupLessons;
                    break;
                case MethodApi.GetTeacherLessons:
                    bytesRes = Properties.Resources.GetTeacherLessons;
                    break;
                case MethodApi.GetLessonTime:
                    bytesRes = Properties.Resources.GetLessonTime;
                    break;
                default:
                    throw new ArgumentException(
                        "Неудалось определить метод запроса! url = " + url);
            }

            return System.Text.Encoding.UTF8.GetString(bytesRes);
        }
    }
}