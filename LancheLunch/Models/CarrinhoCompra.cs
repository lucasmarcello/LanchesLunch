using System;
using System.Collections.Generic;
using System.Linq;
using LancheLunch.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LancheLunch.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _contexto;

        public CarrinhoCompra(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> ListCarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            session.SetString("CarrinhoId", carrinhoId);

            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarItemCarrinho(Lanche lanche, int quantidade)
        {
            var carrinhoCompraItem =
                _contexto.CarrinhoCompraItens.SingleOrDefault
                    (x => x.Lanche.LancheId == lanche.LancheId && x.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _contexto.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }

            _contexto.SaveChanges();

        }

        public int RemoverItemCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                _contexto.CarrinhoCompraItens.SingleOrDefault
                    (x => x.Lanche.LancheId == lanche.LancheId && x.CarrinhoCompraId == CarrinhoCompraId);

            var qtdLocal = 0;

            if (carrinhoCompraItem == null)
            {
                if (carrinhoCompraItem != null)
                {
                    carrinhoCompraItem.Quantidade--;
                    qtdLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _contexto.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }

            _contexto.SaveChanges();

            return qtdLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return ListCarrinhoCompraItens ??
                    (ListCarrinhoCompraItens =
                        _contexto.CarrinhoCompraItens.Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
                            .Include(x => x.Lanche)
                            .ToList()
                    );
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _contexto.CarrinhoCompraItens
                                    .Where(x => x.CarrinhoCompraId == CarrinhoCompraId);

            _contexto.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _contexto.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _contexto.CarrinhoCompraItens.Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
                .Select(x => x.Lanche.Preco * x.Quantidade).Sum();
            return total;
        }
    }
}
