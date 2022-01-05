using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using static AngelPerezIntegra.DTO.DTOCoin;

namespace AngelPerezIntegra.Controllers
{
    public class CoinController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {

            Bitcoin reservationList = new Bitcoin();
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync("https://api.coindesk.com/v1/bpi/currentprice.json");
                string apiResponse = await response.Content.ReadAsStringAsync();
                reservationList = JsonConvert.DeserializeObject<Bitcoin>(apiResponse);
            }
            return View(reservationList);
        }
    }
}
