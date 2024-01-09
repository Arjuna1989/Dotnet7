using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(User user);
        Task<User> GetUserByIdAsync(int Id);

        Task<User> GetUserByNameAsync(string name);
        Task<IEnumerable<User>> GetUsersAsync();
        void Update(User user);

        Task<bool> DeleteUser(User user);

        Task<bool> SaveAll();
    }
}