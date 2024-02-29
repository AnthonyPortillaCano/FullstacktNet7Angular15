using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces
{
    public  interface IUserServices
    {
        Task<Users> ValidarUser(string email, string password);
    }
}
