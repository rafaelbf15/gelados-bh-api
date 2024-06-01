using GeladosBH.Core.DataObjects;
using GeladosBH.Domain.Interfaces;
using GeladosBH.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeladosBH.Data.Repository
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly GeladosBHContext _context;
        public CarrinhoRepository(GeladosBHContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void AdicionarCarrinho(Carrinho carrinho)
        {
            _context.Carrinhos.Add(carrinho);
        }

        public void AdicionarCarrinhoItem(CarrinhoItem carrinhoItem)
        {
            _context.CarrinhoItens.Add(carrinhoItem);
        }

        public void AdicionarCarrinhoItens(IEnumerable<CarrinhoItem> carrinhoItens)
        {
            _context.CarrinhoItens.AddRange(carrinhoItens);
        }

        public void AtualizarCarrinho(Carrinho carrinho)
        {
            _context.Carrinhos.Update(carrinho);
        }

        public void AtualizarCarrinhoItens(IEnumerable<CarrinhoItem> carrinhoItens)
        {
            _context.CarrinhoItens.UpdateRange(carrinhoItens);
        }

        public async Task<CarrinhoItem> ObterCarrinhoItenPorId(Guid id)
        {
            return await _context.CarrinhoItens.AsNoTracking().FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<IEnumerable<CarrinhoItem>> ObterCarrinhoItensPorCarrinhoId(Guid carrinhoId)
        {
            return await _context.CarrinhoItens.AsNoTracking()
                .Where(ci => ci.CarrinhoId == carrinhoId).ToListAsync();
        }

        public async Task<Carrinho> ObterCarrinhoPorId(Guid id)
        {
            return await _context.Carrinhos.AsNoTracking()
                .Include(c => c.CarrinhoItens).ThenInclude(ci => ci.Produto)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Carrinho> ObterCarrinhoPorColaboradorId(Guid colaboradorId)
        {
            return await _context.Carrinhos.AsNoTracking()
                .Include(c => c.CarrinhoItens).ThenInclude(ci => ci.Produto)
                .FirstOrDefaultAsync(c => c.ColaboradorId == colaboradorId);
        }

        public async Task<IEnumerable<Carrinho>> ObterCarrinhos()
        {
            return await _context.Carrinhos.AsNoTracking()
                .Include(c => c.CarrinhoItens).ThenInclude(ci => ci.Produto)
                .ToListAsync();
        }

        public void RemoverCarrinho(Carrinho carrinho)
        {
            _context.Carrinhos.Remove(carrinho);
        }

        public void RemoverCarrinhoItem(CarrinhoItem carrinhoIten)
        {
            _context.CarrinhoItens.Remove(carrinhoIten);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
