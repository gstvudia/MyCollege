using System.Collections.Generic;

namespace MyCollege.Data.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        List<TEntity> GetAll();
        TEntity GetById(int id);
        void Remove(TEntity entity);
    }
}