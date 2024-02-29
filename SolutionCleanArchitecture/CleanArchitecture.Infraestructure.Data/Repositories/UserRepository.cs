using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        protected readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Users> ValidarUser(string email,string password)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("Email", email);
                parameters.Add("Password", password);
                var resultado = await connection.QueryFirstOrDefaultAsync<Users>("SP_Validar_User",parameters, commandType: CommandType.StoredProcedure);
                await connection.CloseAsync();
                return resultado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

    }
}
