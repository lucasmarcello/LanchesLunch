using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LancheLunch.Context;
using LancheLunch.Models;
using LancheLunch.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LancheLunch.Repositories.Repository
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _contexto;

        public LancheRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Lanche>> ListaLanches()
        {
            return await _contexto.Lanches.Include(c => c.Categoria).ToListAsync();
        }

        public async Task<IEnumerable<Lanche>> ListaLanchesPreferidos()
        {
            return await _contexto.Lanches.Where(p=>p.IsLanchePreferido).Include(c => c.Categoria).ToListAsync();
        }
        public async Task<Lanche> BuscaLanchePorId(int lancheId)
        {
            return await _contexto.Lanches.FirstOrDefaultAsync(l => l.LancheId == lancheId);
        }

    }
}
