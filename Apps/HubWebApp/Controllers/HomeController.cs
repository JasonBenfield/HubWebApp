using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubWebApp.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XTI_App;
using XTI_App.Api;

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

        public async Task<IActionResult> Index()
        {
            var hasAccess = await api.UserAdmin.AddUser.HasAccess(AccessModifier.Default);
            return View();
        }
    }
}
