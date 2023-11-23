using Amazon.Runtime.Internal;
using CansolveANK.AnkurLibservises;
using CansolveANK.CansolveModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CansolveANK.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class CanSolveController : ControllerBase
    {
        private readonly ICan _Servises;
        public CanSolveController(ICan servises)
        {
            _Servises = servises;
        }
        /// <summary>
        /// @AnkurMall_exadformer
        /// </summary>
        /// <param name="EventTimestart"></param>
        /// <param name="EventTimeEnd"></param>
        /// <returns></returns>
        /// 
        //[Authorize]

        [HttpPost("raw")]
        // [Route("raw")]
        public async Task<object> raw(DateTime start_date,
          DateTime end_date, [FromBody] TagNameRequestModel request)
        {
            var result = new List<FilterModelcs>();
            var res0 = await _Servises.GetByEvenTimeAsync(start_date, end_date, request.GetArrayFromList());

            foreach (var item in res0)
            {
                var res = new FilterModelcs
                {
                    //id = item.Id,
                    TagName = item.TagName,
                    Value = item.DoubleValue,
                    EventTime = item.EventTime,

                };

                result.Add(res);



            }

            var schema = new
            {
                fields = new[]
                   {
                new { name = "EventTime", type = "datetime" },
                new { name = "TagName", type = "string" },
                new { name = "Value", type = "number" }
            },
                pandas_version = "1.4.0"
            };

            var result2 = new
            {
                schema = schema,
                data = result
            };
            return result2;



        }
        /// <summary>
        /// @ankur_exadformr
        /// </summary>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="time_interval_rate"></param>
        /// <param name="time_interval_unit"></param>
        /// <param name="sample_rate"></param>
        /// <param name="sample_unit"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("interpolate")]
        public async Task<Object> Interpolate(
     DateTime start_date,
     DateTime end_date,
     int time_interval_rate,
     string time_interval_unit,
     int sample_rate,
     string sample_unit,
     [FromBody] TagNameRequestModel request)
        {
            if (!Enum.TryParse<TimeUnit>(time_interval_unit, out TimeUnit unit))
            {
                Console.WriteLine("Invalid time interval time .");
            }

            var result = new CalculateTimeIntrval().CalculateTimeIntervalInMs(unit, time_interval_rate);
            var res = await _Servises.GetAvgValue(start_date, end_date, request.GetArrayFromList(), result);

           
            var schema = new
            {
                fields = new[]
                    {
                new { name = "EventTime", type = "datetime" },
                new { name = "TagName", type = "string" },
                new { name = "Value", type = "number" }
            },
                pandas_version = "1.4.0"
            };

            var result2 = new
            {
                schema = schema,
                data = res
            };
            return result2;
        }





    }

}
