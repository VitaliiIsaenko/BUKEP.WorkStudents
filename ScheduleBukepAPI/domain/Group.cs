﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ScheduleBukepAPI.domain
{
    public class Group
    {
        /// <summary>
        /// Идентификаторы расписания групп
        /// </summary>
        [JsonProperty("idsSchedulGroup")]
        public IList<int> Ids { get; set; }

        /// <summary>
        /// Информация о группе
        /// </summary>
        /// <remarks>
        /// Фантомы бывают 
        ///     обычными - когда для основной и фантомной группы в базе присутствует строчка с 
        /// идентификатором расписания (это стандарт и так бывает в 100% случаев)
        ///     чистые - когда фантомная группа не имеет в базе своего расписания и берет расписание чисто с основной 
        /// (в этом случае она на стенде по состоянию на 18.08.2017 не отображается)
        /// с точки зрения логики и чтобы избежать дублей с чистыми, все группы хранятся в списке
        /// </remarks>
        [JsonProperty("group")]
        public GroupInfo[] Info { get; set; }

        /// <summary>
        /// ССФ
        /// </summary>
        public KeyValuePair<int, string> Ssf { get; set; }

        /// <summary>
        /// Семестр
        /// </summary>
        public int Semestr { get; set; }

        /// <summary>
        /// Тип расписания
        /// </summary>
        public KeyValuePair<int, string> TypeShedule { get; set; }

        /// <summary>
        /// Существование расписания на текущий момент времени
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Дата начала расписания
        /// </summary>
        public DateTime ScheduleDateFrom { get; set; }

        /// <summary>
        /// Дата окончания расписания
        /// </summary>
        public DateTime ScheduleDateTo { get; set; }

        public string GetName()
        {
            return Info[0].Group.Value + TypeShedule.Value;
        }
    }

    /// <summary>
    /// Информация о группе
    /// </summary>
    public class GroupInfo
    {
        
        public GroupInfo()
        {
            Group = new KeyValuePair<int, string>();
            Form = new KeyValuePair<int, string>();
        }

        
        /// <summary>
        /// Група
        /// </summary>
        public KeyValuePair<int, string> Group { get; set; }

        /// <summary>
        /// Фантомная група
        /// </summary>
        public bool IsPhantomGroup { get; set; }

        /// <summary>
        /// Аффикс группы. Только для групп, обучающихся на базе 9 или 11 классов
        /// </summary>
        public string AffixusNameGroup { get; set; }

        /// <summary>
        /// Старое наименование группы
        /// </summary>
        public string NameGroupOld { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Форма
        /// </summary>
        public KeyValuePair<int, string> Form { get; set; }
    }
}