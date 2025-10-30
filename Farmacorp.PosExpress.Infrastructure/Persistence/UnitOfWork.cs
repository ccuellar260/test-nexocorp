using Farmacorp.PosExpress.Domain.Interfaces;
using Farmacorp.PosExpress.Domain.Repositories;
using Farmacorp.PosExpress.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacorp.PosExpress.Infrastructure.Data
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        // Repositorios 
        private IErpProductoRepository? _erpProductoRepository;
        private IExpProductoRepository? _expProductoRepository;

        private ICodigoBarraRepository? _codigoBarraRepository;

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