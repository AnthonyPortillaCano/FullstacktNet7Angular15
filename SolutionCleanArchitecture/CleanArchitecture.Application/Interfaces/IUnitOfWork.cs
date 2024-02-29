using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICompraServices compraServices { get; }
        IUserServices userServices { get; }
        Task<int> CommitAsync();

        IDbContextTransaction BeginTransaction();

        IDetalleCompraServices detalleCompraServices { get; }
    }
}
