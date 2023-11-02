using CansolvUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CansolvUI.Controllers
{
    public class CansolvUIController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7222/api");  
        private readonly HttpClient _Client;
        public CansolvUIController()
        {
                _Client = new HttpClient();
            _Client.BaseAddress = baseAddress;  
        }
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    List<UICansolve> cansolvDataList = new List<UICansolve>();
        //    var eventTimeStart = "2023-11-02T10:44:56.310+00:00";
        //    var eventTimeEnd = "2023-11-02T10:45:37.041+00:00";
        //    var frequency = 300000;

        //    // Define the request data
        //    var requestData = new
        //    {
        //        StartEventTimeAvgCalculations = eventTimeStart,
        //        endTimeForCalculations = eventTimeEnd,
        //        frequency = frequency,
        //        TagName = new string[] { "AM:SASK:CCS_BDPS-UEGGACT103A-CO" }
        //    };

        //    HttpResponseMessage response = _Client.PostAsJsonAsync(_Client.BaseAddress + "/CanSolve/GetAvgValue/GetAvgValue", requestData).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = response.Content.ReadAsStringAsync().Result;
        //        cansolvDataList = JsonConvert.DeserializeObject<List<UICansolve>>(data);
        //    }

        //    return View(cansolvDataList);
        //}
        [HttpGet]
        public IActionResult Index()
        {
            List<UICansolve> cansolvDataList = new List<UICansolve>();
            var eventTimeStart = "2023-11-02T10:44:56.310+00:00";
            var eventTimeEnd = "2023-11-02T10:45:37.041+00:00";
            var frequency = 300000;

            // Define the request data (parameters sent as route values)
            var requestData = new
            {
                StartEventTimeAvgCalculations = eventTimeStart,
                endTimeForCalculations = eventTimeEnd,
                frequency = frequency,
                TagName = new string[] { "AM:SASK:CCS_BDPS-UEGGACT103A-CO" }
            };

            // Construct the URL with route parameters
            var url = $"{_Client.BaseAddress}/CanSolve/GetAvgValue/{eventTimeStart}/{eventTimeEnd}/{frequency}";

            // Send a POST request with the data in the request body
            HttpResponseMessage response = _Client.PostAsJsonAsync(url, requestData).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cansolvDataList = JsonConvert.DeserializeObject<List<UICansolve>>(data);
            }

            return View(cansolvDataList);
        }

        [HttpGet]
        public IActionResult Index2()
        {
            List<UICansolve> cansolvDataList = new List<UICansolve>();
            var eventTimeStart = "2023-10-27T08:27:58.724+00:00";
            var eventTimeEnd = "2023-10-27T08:27:58.724+00:00";
            var queryString = $"?EventTimestart={eventTimeStart}&EventTimeEnd={eventTimeEnd}";

            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress +
                $"/CanSolve/GetDosCansolvData/GetV").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cansolvDataList = JsonConvert.DeserializeObject<List<UICansolve>>(data);

            }

            return View(cansolvDataList);
        }
    }
}

    

