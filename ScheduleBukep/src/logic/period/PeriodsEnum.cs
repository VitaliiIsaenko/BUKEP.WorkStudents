using System;
using Android.Content.Res;

namespace Bukep.Sheduler.logic.period
{
    public enum PeriodsEnum
    {
        PeriodOneDay = 0,
        PeriodThreeDay = 1,
        PeriodWeek = 2
    }

    public static class PeriodsEnumExtension
    {
        private static readonly string[] schedulesPeriods = Resources.System.GetStringArray(Resource.Array.schedules_periods);
        
        public static string GetName(this PeriodsEnum self)
        {
            switch (self)
            {
                case PeriodsEnum.PeriodOneDay:
                    return schedulesPeriods[0];
                case PeriodsEnum.PeriodThreeDay:
                    return schedulesPeriods[1];
                case PeriodsEnum.PeriodWeek:
                    return schedulesPeriods[2];
                default:
                    throw new ArgumentOutOfRangeException(nameof(self), self, "Failed get name.");
            }
        }
    }
    
}