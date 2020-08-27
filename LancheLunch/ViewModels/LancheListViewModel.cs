using System.Collections.Generic;
using LancheLunch.Models;

namespace LancheLunch.ViewModels
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }
        public string CategoriaAtual { get; set; }

    }
}
