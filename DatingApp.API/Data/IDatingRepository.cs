using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
          
          Task<System.Collections.Generic.IEnumerable<User>> GetUsers();

          Task<User> GetUser(int id);
    }
}