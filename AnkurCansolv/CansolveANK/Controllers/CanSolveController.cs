using CansolveANK.AnkurLibservises;
using CansolveANK.CansolveModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CansolveANK.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CanSolveController : ControllerBase
    {
        private readonly ICan _Servises;
        public CanSolveController(ICan servises)
        {
            _Servises = servises;
        }
        /// <summary>
        /// @AnkurMall 
        /// </summary>
        /// <param name="EventTimestart"></param>
        /// <param name="EventTimeEnd"></param>
        /// <returns></returns>
        /// 
        //[Authorize]
        [HttpGet]
        [Route("GetAsync")]
        public async Task<List<FilterModelcs>> GetAsync(DateTime EventTimestart, DateTime EventTimeEnd)
        {
            var result2 = new List<FilterModelcs>();
            var result = await _Servises.GetByEvenTimeAsync(EventTimestart, EventTimeEnd);
            foreach (var item in result)
            {
                var res = new FilterModelcs
                {
                    id = item.Id,
                    TagName = item.TagName,
                    Value = item.DoubleValue,
                    EventTime = item.EventTime,

                };




                result2.Add(res);
            }


            return result2;

        }

        

        [HttpGet]
        [Route("GetV")]
       public async Task<List<FilterModelcs>>GetDosCansolvData()
        {
            var result = new List<FilterModelcs>();
            var res0 = await _Servises.GetAsyncValueDos();

            foreach (var item in res0)
            {
                var res = new FilterModelcs
                {
                    id = item.Id,
                    TagName = item.TagName,
                    Value = item.DoubleValue,
                    EventTime = item.EventTime,

                };

                result.Add(res);



            }


            return result;
        }
        [HttpPost("GetAvgValue")]
        public async Task<List<AggregationModelResult>> GetAvgValue(
    DateTime StartEventTimeAvgCalculations,
    DateTime endTimeForCalculations,
    long frequency,
    [FromBody] string[] TagName)
        {
            return await _Servises.GetAvgValue(StartEventTimeAvgCalculations, endTimeForCalculations, TagName, frequency);
        }




        //[Route("{StartEventTimeAvgCalculations}/{endTimeForCalculations}/{frequency}")]
        [HttpPost("{StartEventTimeAvgCalculations}/{endTimeForCalculations}/{frequency}")]
        public async Task<List<AggregationModelResult>> GetAvgValueAsync(
    DateTime StartEventTimeAvgCalculations,
    DateTime endTimeForCalculations,
    long frequency,
    [FromBody()] string[] TagName)
        {
            return await _Servises.GetAvgValue(StartEventTimeAvgCalculations, endTimeForCalculations, TagName, frequency);
        }

    }
}
