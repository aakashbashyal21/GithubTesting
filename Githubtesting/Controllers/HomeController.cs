using Githubtesting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Tesseract;

namespace Githubtesting.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Chapter content = new Chapter();
            var chapterOutputPath = @"./page_1.png";
            using (var engine = new TesseractEngine("./tessdata", "hin", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(chapterOutputPath))
                {
                    using (var page = engine.Process(img))
                    {
                        content.Text = page.GetText();
                    }
                }

            }


            return View(content);
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