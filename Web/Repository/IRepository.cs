using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Query();

        T Get(long id);

        void Create(T entity);
        Task CreateAsync(T entity);

        void Update(T entity);
        Task UpdateAsync(T entity);

        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}