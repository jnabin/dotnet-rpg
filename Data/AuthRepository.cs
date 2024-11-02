using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private DataContext _context;
        private IConfiguration _configure;
        public AuthRepository(DataContext context, IConfiguration configure)
        {
            _configure = configure;
            _context = context;
        }
        public async Task<ServiceRespose<string>> Login(string userName, string password)
        {
            var response = new ServiceRespose<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
            if(user is null){
                response.Success = false;
                response.Message = "User is not found";
            } 
            else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) {
                response.Success = false;
                response.Message = "Wrong password";
            } else{
                response.Data = CreateToken(user);
            }

            return response;
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

        private bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }

        private string CreateToken(User user){
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var appSettingToken = _configure.GetSection("AppSettings:Token").Value;
            if(appSettingToken is null)
                throw new Exception("Token not exist");

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(appSettingToken)
            );
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}