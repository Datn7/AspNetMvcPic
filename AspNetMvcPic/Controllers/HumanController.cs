using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetMvcPic.Data.Repositories;
using AspNetMvcPic.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcPic.Controllers
{
    public class HumanController : Controller
    {
        private readonly IHumanRepository humanRepository;

        public HumanController(IHumanRepository humanRepository)
        {
            this.humanRepository = humanRepository;
        }

        public IActionResult Index()
        {
            HumanViewModel humanViewModel = new HumanViewModel();
            humanViewModel.Humans = humanRepository.AllHumans;

            return View(humanViewModel);
        }
    }
}