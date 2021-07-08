using InstagramPromotionHelper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Abstractions;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace InstagramPromotionHelper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInstagramService _instagramService;

        public HomeController(ILogger<HomeController> logger, IInstagramService instagramService)
        {
            _logger = logger;
            _instagramService = instagramService;
        }

        public IActionResult Index()
        {
            //using (StreamReader file = System.IO.File.OpenText(@"C:\Users\Mousa\Desktop\promotion.json"))
            //using (JsonTextReader reader = new JsonTextReader(file))
            //{
            //    JObject o2 = (JObject)JToken.ReadFrom(reader);
            //    System.Console.WriteLine(o2);
            //}
            //dynamic o1 = JObject.Parse(System.IO.File.ReadAllText(@"C:\Users\Mousa\Desktop\promotion.json"));

            var x = _instagramService.GetPostComments();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("fetchData")]
        public async Task<IActionResult> FetchData(string searchText)
        {
            //string jsonString = (string)await LoadJsonFile(@"C:\Users\Mousa\Desktop\promotion.json");

            JObject o1 = JObject.Parse(System.IO.File.ReadAllText(@"C:\Users\Mousa\Desktop\promotion.json"));

            using (StreamReader file = System.IO.File.OpenText(@"c:\videogames.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                System.Console.WriteLine(o2);
            }

            return View("InstagramPostSearchPage");
        }
    }
}