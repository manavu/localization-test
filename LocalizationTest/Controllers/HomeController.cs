using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LocalizationTest.Models;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace LocalizationTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IStringLocalizer _localizer1;
        private readonly IStringLocalizer _localizer2;
        private readonly IStringLocalizer<SharedResource> _localizer3;


        public HomeController(IStringLocalizerFactory factory, IStringLocalizer<SharedResource> sharedLocalizer, ILogger<HomeController> logger)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer1 = factory.Create(type);
            _localizer2 = factory.Create("SharedResource", assemblyName.Name);
            _localizer3 = sharedLocalizer;

            _logger = logger;
        }

        public IActionResult Index()
        {
            var b1 = _localizer1["Title"];
            var b2 = _localizer2["Title"];
            var b3 = _localizer3["Title"];

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
    }
}
