using Microsoft.EntityFrameworkCore;
using Poseidon.Entities.ResourceMaps;
using Poseidon.Repositories.ResourceMaps.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }
        

        public async Task<List<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
        {
            var result = await IncludeMultiple(includes).ToListAsync();
            return result;
        }

        public async Task<TEntity> GetById<TEntity>(Guid id, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
        {
            var result = await IncludeMultiple(includes).SingleOrDefaultAsync(e => e.Id == id);
            return result;
        }

        public async Task<Guid> InsertEntity<TEntity>(TEntity t) where TEntity : BaseEntity
        {
            var dbSet = _context.Set<TEntity>();
            t.DateInsert = DateTime.Now;
            dbSet.Add(t);
            var result = await _context.SaveChangesAsync();
            return result > 0 ? t.Id : Guid.Empty;
        }

        public async Task<bool> UpdateEntity<TEntity>(TEntity t) where TEntity : BaseEntity
        {
            var dbSet = _context.Set<TEntity>();
            
            var entity = await dbSet.FindAsync(t.Id);
            if (entity == null) return false;


            t.DateDisable = entity.DateDisable;
            t.DateInsert = entity.DateInsert;        
            t.DateModify = DateTime.Now;

            if (entity != null && !t.Active && entity.Active) {
                t.DateDisable = DateTime.Now;
            } 

            _context.Entry(entity).CurrentValues.SetValues(t);
            
            var result = await _context.SaveChangesAsync() > 0 ? true : false;
            return result;
        }

        public async Task<bool> DeleteEntity<TEntity>(Guid id) where TEntity : BaseEntity
        {
            var dbSet = _context.Set<TEntity>();
            var entity = await dbSet.SingleOrDefaultAsync(e => e.Id == id);
            if (entity != null) dbSet.Remove(entity);
            var result = await _context.SaveChangesAsync() > 0 ? true : false;
            return result;
        }


        protected IQueryable<TEntity> IncludeMultiple<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
        {
            var query = _context.Set<TEntity>().AsNoTrackingWithIdentityResolution();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }


       
    }
}
