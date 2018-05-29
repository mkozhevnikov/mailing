using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model;

namespace Web.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ConcurrentDictionary<long, T> container = new ConcurrentDictionary<long, T>();
        private long counter = 0;
        private readonly object lockObject = new object();
        
        public IQueryable<T> Query()
        {
            return container.Values.Where(v => null != v).AsQueryable();
        }

        public T Get(long id)
        {
            return container.GetValueOrDefault(id) ?? throw new Exception("Entity not found");
        }

        public void Create(T entity)
        {
            lock (lockObject) {
                entity.Id = counter++;
                container.AddOrUpdate(entity.Id, entity,
                    (id, e) => throw new Exception("Object with the same Id exists already"));
            }
        }

        public async Task CreateAsync(T entity)
        {
            await Task.Run(() => Create(entity)).ConfigureAwait(false);
        }

        public void Update(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(T entity)
        {
            container.TryRemove(entity.Id, out entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Delete(entity)).ConfigureAwait(false);
        }
    }
}