using System.Collections.Generic;
using System.Threading.Tasks;
using LancheLunch.Models;

namespace LancheLunch.Repositories.Interface
{
    public interface ILancheRepository
    {
        Task<IEnumerable<Lanche>> ListaLanches();
        Task<IEnumerable<Lanche>> ListaLanchesPreferidos();
        Task<Lanche> BuscaLanchePorId(int lancheId);
    }
}
