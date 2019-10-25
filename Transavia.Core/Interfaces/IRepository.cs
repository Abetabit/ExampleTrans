using System.Collections.Generic;
using System.Threading.Tasks;
using Transavia.Core.Entities;


namespace Transavia.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();

        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> SaveChangesAsync();
        IEnumerable<T> GetAll(ISpecification<T> spec);
    }
}
