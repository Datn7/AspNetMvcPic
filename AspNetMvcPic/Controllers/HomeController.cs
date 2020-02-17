using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetMvcPic.Models;
using AspNetMvcPic.Data;
using Microsoft.AspNetCore.Hosting;
using AspNetMvcPic.ViewModel;
using System.IO;

namespace AspNetMvcPic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ListItemDbContext ctx;
        private readonly IWebHostEnvironment hostEnv;

        public HomeController(ILogger<HomeController> logger, ListItemDbContext ctx, IWebHostEnvironment hostEnv)
        {
            _logger = logger;
            this.ctx = ctx;
            this.hostEnv = hostEnv;
        }

        public IActionResult Index()
        {
            var item = ctx.Items.ToList();
            return View(item);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(ListItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                ListItem listItem = new ListItem
                {
                    Name = model.FirstName,
                    ProfilePicture = uniqueFileName
                };

                ctx.Add(listItem);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        
        private string UploadedFile(ListItemViewModel model)
        {
            string uniqueFileName = null;

            if(model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(hostEnv.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
