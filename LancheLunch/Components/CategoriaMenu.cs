using System.Linq;
using LancheLunch.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LancheLunch.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaMenu(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.ListaCategorias.OrderBy(p => p.CategoriaNome);
            return View(categorias);
        }        
    }
}
