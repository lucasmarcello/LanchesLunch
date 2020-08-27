using System.Collections.Generic;
using LancheLunch.Models;

namespace LancheLunch.Repositories.Interface
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> ListaCategorias { get; }

    }
}
