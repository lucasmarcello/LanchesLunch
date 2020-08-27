using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LancheLunch.Models;
using LancheLunch.Repositories.Interface;
using LancheLunch.ViewModels;

namespace LancheLunch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        public HomeController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancheRepository.ListaLanchesPreferidos().Result
            };
            return View(homeViewModel);
        }

        public ViewResult AccessDenied()
        {
            return View();
        }
    }
}
