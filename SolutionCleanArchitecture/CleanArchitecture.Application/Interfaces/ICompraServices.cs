using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces
{
    public interface ICompraServices
    {
        Task<List<Compra>> ListaBusquedaCompra(string? numeroDocumento, string? razonSocial);
        Task<int> Insert(Compra compra);
        void UpdateFieldsSave(Compra compra);

        Task Delete(int id);
    }
}
