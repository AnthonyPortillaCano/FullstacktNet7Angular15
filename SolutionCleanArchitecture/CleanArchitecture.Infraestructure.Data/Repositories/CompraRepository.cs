using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Data.Repositories
{
    public class CompraRepository : RepositoryEF<Compra>, ICompraRepository
    {
        public CompraRepository(DbContext context) : base(context)
        {
            repositoryEF= new RepositoryEF<Compra>(context);
        }
        public IRepositoryEF<Compra> repositoryEF { get;  set; }

        public async Task<List<Compra>> listaBusquedaCompra(string? numeroDocumento,string? razonSocial)
        {
            var listaBusqueda = new List<Compra>();
            listaBusqueda = await repositoryEF.ObtenerList<Compra>(a => (a.NumeroDocumento == numeroDocumento || string.IsNullOrEmpty(numeroDocumento)) && (a.RazonSocial == razonSocial || string.IsNullOrEmpty(razonSocial)));
            return listaBusqueda;
        }
        public async Task<int> Insert(Compra compra)
        {
            return await repositoryEF.Insert(compra);
        }
        public void UpdateFieldsSave(Compra compra)
        {
            repositoryEF.UpdateFieldsSave(compra, b => b.NumeroDocumento, c => c.RazonSocial, d => d.Total);   
        }
        public async Task Delete(int id)
        {
            await repositoryEF.Delete(d => d.Id == id);
        }
    }
}
