namespace Bukep.ShedulerApi.apiDTO
{
    /// <summary>
    ///  DTO from API method GetFaculties.
    /// </summary>
    public class Faculty
    {
        public string IdFaculty { get; set; }
        public string Name { get; set; }
        public bool IsActiveSchedule { get; set; }
    }
}
