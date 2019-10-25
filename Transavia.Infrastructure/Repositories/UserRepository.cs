using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transavia.Core.Entities;
using Transavia.Core.Interfaces;
using Transavia.Infrastructure.ContextDB;

namespace Transavia.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        protected readonly TestContext _context;
        public UserRepository(TestContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
