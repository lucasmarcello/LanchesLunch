using System;
using System.Collections.Generic;
using System.Linq;
using LancheLunch.Models;
using LancheLunch.Repositories.Interface;
using LancheLunch.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LancheLunch.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ILancheRepository iLancheRepository, ICategoriaRepository iCategoriaRepository)
        {
            _lancheRepository = iLancheRepository;
            _categoriaRepository = iCategoriaRepository;
        }

        public IActionResult List(string categoria)
        {
            string _categoria = categoria;
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.ListaLanches().Result.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                if (string.Equals("Normal", _categoria, StringComparison.OrdinalIgnoreCase))
                    lanches = _lancheRepository.ListaLanches().Result.Where(p => p.Categoria.CategoriaNome.Equals("Normal")).OrderBy(p => p.Nome);
                else
                    lanches = _lancheRepository.ListaLanches().Result.Where(p => p.Categoria.CategoriaNome.Equals("Natural")).OrderBy(p => p.Nome);

                categoriaAtual = _categoria;
            }

            var lancheListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lancheListViewModel);
        }

        public ViewResult Details(int lancheId)
        {
            var lanche = _lancheRepository.ListaLanches().Result.FirstOrDefault(d => d.LancheId == lancheId);
            if (lanche == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(lanche);
        }

        public ViewResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Lanche> lanches;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                lanches = _lancheRepository.ListaLanches().Result.OrderBy(p => p.LancheId);
            }
            else
            {
                lanches = _lancheRepository.ListaLanches().Result.Where(p => p.Nome.ToLower().Contains(_searchString.ToLower()));
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel { Lanches = lanches, CategoriaAtual = "Todos os lanches" });
        }

    }
}
