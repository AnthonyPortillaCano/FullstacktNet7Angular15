using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infraestructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class UserServices: IUserServices
    {
        public UserServices(string connectionString) 
        { 
           userRepository=new UserRepository(connectionString);
        }
        protected IUserRepository userRepository { get; set; }

        public async Task<Users> ValidarUser(string email,string password)
        {
            return await userRepository.ValidarUser(email, password);
        }
    }
}
