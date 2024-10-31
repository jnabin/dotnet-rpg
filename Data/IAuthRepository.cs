using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public interface IAuthRepository
    {
        Task<ServiceRespose<int>> Register(User user, string password);
        Task<ServiceRespose<string>> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}