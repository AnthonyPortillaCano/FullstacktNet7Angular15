using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class DetalleCompraServices:IDetalleCompraServices
    {
        protected IDetalleCompraRepository _detalleCompraRepository;
        public DetalleCompraServices(DbContext context,string connectionString)
        {
            _detalleCompraRepository=new DetalleCompraRepository(context,connectionString);
        }
        public  async Task<int> Insert(DetalleCompra detalleCompra)
        {
            return await _detalleCompraRepository.Insert(detalleCompra);
        }
        public void Update(DetalleCompra detalleCompra)
        {
            _detalleCompraRepository.Update(detalleCompra);
        }
        public async Task Delete(int id)
        {
            await _detalleCompraRepository.Delete(id);
        }
        public async  Task<List<DetalleComprasDto>>  ObtenerDetalleCompra(int idCompra)
        {
            return await _detalleCompraRepository.ObtenerDetalleCompra(idCompra);
        }
        public  async Task DeleteList(int idCompra)
        {
            await _detalleCompraRepository.DeleteListDetalleCompra(idCompra);
        }
    }
}
