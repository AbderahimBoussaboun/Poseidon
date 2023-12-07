using Poseidon.Entities.ResourceMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Interfaces
{
    public interface IGenericRepository
    {
        public Task<List<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity;
        public Task<TEntity> GetById<TEntity>(Guid id, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity;
        public Task<Guid> InsertEntity<TEntity>(TEntity t) where TEntity : BaseEntity;
        public Task<bool> UpdateEntity<TEntity>(TEntity t) where TEntity : BaseEntity;
        public Task<bool> DeleteEntity<TEntity>(Guid id) where TEntity : BaseEntity;



    }
}
