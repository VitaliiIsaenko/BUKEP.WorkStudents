using System;
using System.Collections.Generic;

namespace ScheduleBukepAPI.service.paremeters
{
    public class ParameterConstructor
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        public ParameterConstructor SetParameter(NameParameterForApi nameParameter, int value)
        {
            _dictionary[nameParameter.ToString()] = value.ToString();
            return this;
        }

        public ParameterConstructor SetParameter(NameParameterForApi nameParameter, DateTime value)
        {
            _dictionary[nameParameter.ToString()] = value.ToString(Api.DateTimeFormat);
            return this;
        }

        public IDictionary<string, string> GetResults()
        {
            Dictionary<string, string> result = new Dictionary<string, string>(_dictionary);
            _dictionary.Clear();
            return result;
        }
    }
}