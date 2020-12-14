using HubWebApp.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HubWebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly HubAppApi api;

        public HomeController(HubAppApi api)
        {
            this.api = api;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
