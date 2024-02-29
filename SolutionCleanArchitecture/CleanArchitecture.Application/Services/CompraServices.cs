using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Services
{
    public  class CompraServices:ICompraServices
    {
          protected ICompraRepository _compraRepository { get; set; }
           public CompraServices(DbContext context)
        {
            _compraRepository=new  CompraRepository(context);
        }
         public async Task<List<Compra>>  ListaBusquedaCompra(string? numeroDocumento,string? razonSocial)
        {
             return  await _compraRepository.listaBusquedaCompra(numeroDocumento, razonSocial);
        }
        public async Task<int> Insert(Compra compra)
        {
            return await _compraRepository.Insert(compra);
        }
        public void UpdateFieldsSave(Compra compra)
        {
            _compraRepository.UpdateFieldsSave(compra);
        }
        public async Task Delete(int id)
        {
            await  _compraRepository.Delete(id);
        }
    }
}
