using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace CleanArchitecture.Application.Services
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BdempresaContext _context;
        public UnitOfWork(BdempresaContext context,string connectionString)
        {
            _context = context;
            userServices = new UserServices(connectionString);
            compraServices = new CompraServices(context);
            detalleCompraServices = new DetalleCompraServices(context, connectionString);
        }
        public ICompraServices compraServices { get; private set; }
        public IUserServices userServices { get; private set; }

        public IDetalleCompraServices detalleCompraServices { get; private set; }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
        }
    }
}
