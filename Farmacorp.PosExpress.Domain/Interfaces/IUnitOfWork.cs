
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacorp.PosExpress.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IErpProductoRepository ErpProductoRepository { get; }
        IExpProductoRepository ExpProductoRepository { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
