using System.Collections.Generic;
using LancheLunch.Context;
using LancheLunch.Models;
using LancheLunch.Repositories.Interface;

namespace LancheLunch.Repositories.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _contexto;

        public CategoriaRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        IEnumerable<Categoria> ICategoriaRepository.ListaCategorias => _contexto.Categorias;

    }
}
