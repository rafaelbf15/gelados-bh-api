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
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly GeladosBHContext _context;
        public ProdutoRepository(GeladosBHContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Produto> ObterProdutoPorId(Guid id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }
        public void AdicionarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void AdicionarProdutos(IEnumerable<Produto> produtos)
        {
            _context.Produtos.AddRange(produtos);
        }

        public void AtualizarProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public void AtualizarProdutos(IEnumerable<Produto> produtos)
        {
            _context.Produtos.UpdateRange(produtos);
        }


        public void RemoverProduto(Produto produto)
        {
            _context.Produtos.Remove(produto);
        }

        public void RemoverProdutos(IEnumerable<Produto> produtos)
        {
            _context.Produtos.RemoveRange(produtos);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
