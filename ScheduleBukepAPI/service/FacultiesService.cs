using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service.paremeters;

namespace ScheduleBukepAPI.service
{
    public class FacultiesService : BaseService, IFacultiesService
    {
        private readonly ParameterBuilder _parameterBuilder = new ParameterBuilder();

        public List<Faculty> GetFaculties(int year, int idFilial)
        {
            IDictionary<string, string> parameters = _parameterBuilder
                .SetParameter(ParameterNameForApi.Year, year)
                .SetParameter(ParameterNameForApi.IdFilial, idFilial)
                .Build();
            string json = ExecuteGet(MethodApi.GetFaculties, parameters);
            return ConvertToList<Faculty>(json);
        }

        public List<Specialty> GetSpecialtys(int year, int idFilial, int idFaculty)
        {
            IDictionary<string, string> parameters = _parameterBuilder
                .SetParameter(ParameterNameForApi.Year, year)
                .SetParameter(ParameterNameForApi.IdFilial, idFilial)
                .SetParameter(ParameterNameForApi.IdFaculty, idFaculty)
                .Build();
            string json = ExecuteGet(MethodApi.GetSpecialtys, parameters);
            return ConvertToList<Specialty>(json);
        }

        public List<Course> GetCourses(int year, int idFilial, int idFaculty, IList<int> idsSpecialty)
        {
            IDictionary<string, string> parameters = _parameterBuilder
                .SetParameter(ParameterNameForApi.Year, year)
                .SetParameter(ParameterNameForApi.IdFilial, idFilial)
                .SetParameter(ParameterNameForApi.IdFaculty, idFaculty)
                .Build();
            string json = ExecutePost(MethodApi.GetCourses, parameters, idsSpecialty);
            return ConvertToList<Course>(json);
        }

        //TODO: idCourse поменять местами с idsSpecialty
        public List<Group> GetGroups(int year, int idFilial, int idFaculty, int idCourse,
            IList<int> idsSpecialty)
        {
            IDictionary<string, string> parameters = _parameterBuilder
                .SetParameter(ParameterNameForApi.Year, year)
                .SetParameter(ParameterNameForApi.IdFilial, idFilial)
                .SetParameter(ParameterNameForApi.IdFaculty, idFaculty)
                .SetParameter(ParameterNameForApi.IdCourse, idCourse)
                .Build();
            string json = ExecutePost(MethodApi.GetGroups, parameters, idsSpecialty);
            return ConvertToList<Group>(json);
        }

        public List<Pulpit> GetPulpits(int year, int idFilial)
        {
            IDictionary<string, string> parameters = _parameterBuilder
                .SetParameter(ParameterNameForApi.Year, year)
                .SetParameter(ParameterNameForApi.IdFilial, idFilial)
                .Build();
            var json = ExecuteGet(MethodApi.GetPulpit, parameters);
            return ConvertToList<Pulpit>(json);
        }

        public List<Teacher> GetTeacher(int year, int idPulpit)
        {
            IDictionary<string, string> parameters = _parameterBuilder
                .SetParameter(ParameterNameForApi.Year, year)
                .SetParameter(ParameterNameForApi.IdPulpit, idPulpit)
                .Build();
            var json = ExecuteGet(MethodApi.GetTeacher, parameters);
            return ConvertToList<Teacher>(json);
        }
    }
}