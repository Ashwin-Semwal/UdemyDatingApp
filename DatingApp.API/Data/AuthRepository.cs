using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            this._context = context;

        }



        public async Task<IEnumerable<User>> GetUsers()
        {
            var user = await _context.Users.ToListAsync();
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if (!VerifyPassowrdHash(password, user.PasswordHash, user.PasswordSalt))
                return null;


            return user;
        }

        private bool VerifyPassowrdHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            };

            return true;
        }

        public async Task<User> Register(User user, string password)
        {

            byte[] passwordHash, pashwordSalt;
            CreatePasswordHash(password, out passwordHash, out pashwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = pashwordSalt;

            await _context.Users.AddAsync(user);
            try{
            await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
              string msg = ex.Message;  
            }
            return user;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] pashwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pashwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            };
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}