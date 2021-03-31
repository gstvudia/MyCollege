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
        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
