using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IDetalleCompraRepository
    {
        Task<int> Insert(DetalleCompra detalleCompra);
        void Update(DetalleCompra detalleCompra);

        Task Delete(int id);
        Task DeleteListDetalleCompra(int idCompra);
        Task<List<DetalleComprasDto>> ObtenerDetalleCompra(int idCompra);
    }
}
