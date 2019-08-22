using System.Threading.Tasks;
using DatingApp.API.Models;
using System.Collections.Generic;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> RegisterAsync(User user,string password);

         User Register(User user,string password);

         Task<User> Login(string username, string password);

         Task<bool> UserExists(string username);

         Task<IEnumerable<User>> GetUsers();
         
    }
}