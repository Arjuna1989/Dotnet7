using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Entities;
using API.interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            
        }
        public async Task<bool> CreateUser(User user)
        {
         throw new NotImplementedException();

        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
           return await _dataContext.Users.FindAsync(Id);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _dataContext.Users.Include(x => x.Photos).FirstOrDefaultAsync(user => user.UserName == name);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dataContext.Users.Include(x => x.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
         return await _dataContext.SaveChangesAsync()> 0;
        }

        public void Update(User user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}