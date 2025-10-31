using Farmacorp.PosExpress.Domain.Interfaces;
using Farmacorp.PosExpress.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacorp.PosExpress.Infrastructure.Persistence
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        // Repositorios 
        private IErpProductoRepository? _erpProductoRepository;
        private IExpProductoRepository? _expProductoRepository;
        private ICodigoBarraRepository? _codigoBarraRepository;
        private ITipoProductoRepository? _tipoProductoRepository;
        private IProductoCategoriaRepository? _productoCategoriaRepository;
        private ICategoriaRepository? _categoriaRepository;
        private IVentaRepository? _ventaRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        public IErpProductoRepository ErpProductoRepository
        {
            get
            {
                _erpProductoRepository ??= new ErpProductoRepository(_context);
                return _erpProductoRepository;
            }
        }


        public IExpProductoRepository ExpProductoRepository
        {
            get
            {
                _expProductoRepository ??= new ExpProductoRepository(_context);
                return _expProductoRepository;
            }
        }

        public ICodigoBarraRepository CodigoBarraRepository
        {
            get
            {
                _codigoBarraRepository ??= new CodigoBarraRepository(_context);
                return _codigoBarraRepository;
            }
        }

        public ITipoProductoRepository TipoProductoRepository
        {
            get
            {
                _tipoProductoRepository ??= new TipoProductoRepository(_context);
                return _tipoProductoRepository;
            }
        }

        public IProductoCategoriaRepository ProductoCategoriaRepository
        {
            get
            {
                _productoCategoriaRepository ??= new ProductoCategoriaRepository(_context);
                return _productoCategoriaRepository;
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                _categoriaRepository ??= new CategoriaRepository(_context);
                return _categoriaRepository;
            }
        }

        public IVentaRepository VentaRepository
        {
            get
            {
                _ventaRepository ??= new VentaRepository(_context);
                return _ventaRepository;
            }
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
   
   
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}