using System;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.controllers.factory
{
    public class SelectItemFactory
    {
        public static SelectItem CreateSelectItem(SelectItemActivity activity,SelectItemType selectItemType)
        {
            SelectItem selectItem;
            switch (selectItemType)
            {
                case SelectItemType.SelectScheduleStudent:
                    selectItem = new SelectScheduleStudent(activity);
                    break;
                case SelectItemType.SelectScheduleTeacher:
                    selectItem = new SelectScheduleTeacher(activity);
                    break;
                case SelectItemType.SelectFavorites:
                    selectItem = new SelectFavoritesGroup(activity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        "Не удалось выбрать тип расписания. SchedulesType = " + selectItemType);
            }
            return selectItem;
        }
    }
}