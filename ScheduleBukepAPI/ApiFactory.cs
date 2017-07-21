using System;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service;

namespace ScheduleBukepAPI
{
    class ApiFactory
    {
        public Api CreateApiFake()
        {
            return new Api(
                new OverrideGetFaculty(
                    new FakeHttpRequstHelper(), new JsonConvert()),
                new SchedulesService(
                    new FakeHttpRequstHelper(), new JsonConvert())
            );
        }
    }
}