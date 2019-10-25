using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transavia.Core.Entities;

namespace Transavia.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
    }
}
