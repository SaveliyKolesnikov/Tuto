using System.Linq;

namespace Tuto.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        IQueryable<TEntity> Read();
        void Delete(TEntity entity);
        void Update(TEntity newEntity);
    }
}
