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
        [HttpGet]
        public IActionResult Index()
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
