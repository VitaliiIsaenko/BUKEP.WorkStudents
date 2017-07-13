using System;
using System.Collections.Generic;

namespace ScheduleBukepAPI.service.paremeters
{
    public class ParameterBuilder
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        public ParameterBuilder SetParameter(ParameterNameForApi parameterName, int value)
        {
            _dictionary[parameterName.ToString()] = value.ToString();
            return this;
        }

        public ParameterBuilder SetParameter(ParameterNameForApi parameterName, IList<int> values)
        {
            _dictionary[parameterName.ToString()] = FacadeApi.ConvertIdsToString(values);
            return this;
        }

        public ParameterBuilder SetParameter(ParameterNameForApi parameterName, DateTime value)
        {
            _dictionary[parameterName.ToString()] = value.ToString(FacadeApi.DateTimeFormat);
            return this;
        }

        public IDictionary<string, string> Build()
        {
            Dictionary<string, string> result = new Dictionary<string, string>(_dictionary);
            _dictionary.Clear();
            return result;
        }
    }
}