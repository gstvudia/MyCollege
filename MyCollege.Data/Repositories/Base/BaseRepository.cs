using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCollege.Data.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly Context _dbContext;
        public BaseRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        
        public virtual List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public virtual TEntity Add(TEntity entity)
        {
            var created = _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return created;
        }

    }
}
