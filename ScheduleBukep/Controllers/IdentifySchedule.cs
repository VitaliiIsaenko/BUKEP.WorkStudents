using System.Collections.Generic;
using Bukep.ShedulerApi;
using Bukep.ShedulerApi.apiDTO;

namespace Bukep.Sheduler.Controllers
{
    class IdentifySchedule : IController
    {
        private IdentifyScheduleActivity view;

        public IdentifySchedule(IdentifyScheduleActivity view)
        {
            this.view = view;
        }

        public void Update()
        {
            List<Faculty> faculties = FacadeAPI.GetFaculties();           
            view.ShowFaculty(WrappingFaculty(faculties));
        }

        private List<Faculty> WrappingFaculty(List<Faculty> faculties)
        {
            List<Faculty> facultyWrappers = new List<Faculty>();
            foreach (var faculty in faculties)
            {
                facultyWrappers.Add(new FacultyWrapper(faculty));
            }
            return facultyWrappers;
        }
    }

    /// <summary>
    /// Используется для получения из ToString() имени Faculty.
    /// Нужно для ArrayAdapter 
    /// так как ArrayAdapter использует ToString() для отображения Faculty на View.
    /// </summary>
    class FacultyWrapper : Faculty
    {
        private Faculty faculty;

        public FacultyWrapper(Faculty faculty)
        {
            this.faculty = faculty;
        } 

        public override string ToString()
        {
            return faculty.Name;
        }
    }
}