using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleBukepAPI.domain
{
    /// <summary>
    /// Нужен для хранения данных ключ-значение в Model
    /// </summary>
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
