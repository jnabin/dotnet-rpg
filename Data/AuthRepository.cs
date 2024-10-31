using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public Task<ServiceRespose<string>> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceRespose<int>> Register(User user, string password)
        {
            var isUserExist = await UserExists(user.UserName);
            if(isUserExist is true){
                return new ServiceRespose<int>{Data = 0, Message = $"User already exist with {user.UserName}", Success = false};;
            } else{
                CreatePasswordHash(password, out byte[] passwordSalt, out byte[] passwordHash);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new ServiceRespose<int>{Data = user.Id};
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == userName.ToLower());
        }

        private void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash){
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));                
            }
        }
    }
}