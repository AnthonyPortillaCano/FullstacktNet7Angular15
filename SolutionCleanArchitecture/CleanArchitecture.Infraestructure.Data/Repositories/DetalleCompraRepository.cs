using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Data.Repositories
{
    public class DetalleCompraRepository : RepositoryEF<DetalleCompra>, IDetalleCompraRepository
    {
        protected readonly string _connectionString;
        public DetalleCompraRepository(DbContext context,string connectionString) : base(context)
        {
            repositoryEF = new RepositoryEF<DetalleCompra>(context);
            _connectionString = connectionString;
        }
        public IRepositoryEF<DetalleCompra> repositoryEF { get; set; }

        public async Task<int> Insert(DetalleCompra  detalleCompra)
        {
            return await repositoryEF.Insert(detalleCompra);
        }
        public void Update(DetalleCompra detalleCompra)
        {
            repositoryEF.Update(detalleCompra);
        }
        public async Task Delete(int id)
        {
            await repositoryEF.Delete(d=>d.Id== id);
        }
        public async Task DeleteListDetalleCompra(int idCompra)
        {
            List<DetalleCompra> lista = new();
             lista= await  repositoryEF.ObtenerList<DetalleCompra>(a=>a.IdCompra==idCompra);
            repositoryEF.DeleteList(lista);
        }
        public async Task<List<DetalleComprasDto>> ObtenerDetalleCompra(int idCompra)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var parameters = new DynamicParameters();
            parameters.Add("IdCompra", idCompra);
            var resultado = await connection.QueryAsync<DetalleComprasDto>("sp_DetalleCompra", parameters, commandType: CommandType.StoredProcedure);
            var listDetalleCompra=resultado.Cast<DetalleComprasDto>().ToList();
            await connection.CloseAsync();
            return listDetalleCompra;
        }
       
    }
}
