namespace ScheduleBukepAPI.domain
{
    /// <summary>
    ///  DTO from API method GetFaculties.
    /// </summary>
    public class Faculty
    {
        //TODO: Конфликт имени класса и метода
        public KeyValue faculty { get; set; }
        public bool IsActiveSchedule { get; set; }
    }
}
