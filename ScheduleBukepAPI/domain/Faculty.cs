using System.Collections.Generic;

namespace ScheduleBukepAPI.domain
{
    /// <summary>
    ///  DTO from API method GetFaculties.
    /// </summary>
    public class Faculty
    {
        /// <summary>
        /// Key - id
        /// Value - name 
        /// </summary>
        public KeyValuePair<int, string> FacultyInfo { get; set; }

        public bool IsActiveSchedule { get; set; }
    }
}