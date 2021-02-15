using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRespository : IAuthRepository
    {
        #region  Private Memeber

        private readonly DataContext _context;

        #endregion

        #region  Constructor
        public AuthRespository(DataContext context)
        {
            _context = context;
        }
        #endregion
      
        #region Public Function
        public async Task<bool> CheckUserName(string Username)
        {
            if(await _context.Users.AnyAsync(x=>x.UserName==Username))
            return true;

            return false;
        }
        #endregion
        
        #region Log Function
        public async Task<User> Login(string Username, string password)
        {
            var user =await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x=>x.UserName==Username);
            if(user==null)
            return null;

            if(!VerfiyPasswordHash(password,user.PasswordSalt,user.PasswordHash))
            return null;

            return user;
        }

        private bool VerfiyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
              using(var hmac= new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {               
               var ComputeHash =hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i<ComputeHash.Length;i++)
                {
                    if(ComputeHash[i]!=passwordHash[i])return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using(var hmac= new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt=hmac.Key;
                passwordhash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
       
        #endregion
        
        #region Register
        public async Task<User> Register(User user, string password)
        {
           byte[] passwordhash,passwordsalt;
           CreatePasswordHash(password,out passwordhash,out passwordsalt);
           user.PasswordHash=passwordhash;
           user.PasswordSalt=passwordsalt;
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
           return user;
        }

        #endregion
    }
}