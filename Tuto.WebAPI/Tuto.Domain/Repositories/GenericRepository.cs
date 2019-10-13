using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tuto.Domain.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly DbSet<TEntity> CurrentDbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            CurrentDbSet = dbContext.Set<TEntity>();
        }

        public virtual void Create(TEntity entity) => CurrentDbSet.Add(entity);

        public virtual IQueryable<TEntity> Read() => CurrentDbSet;

        public virtual void Delete(TEntity entity) => CurrentDbSet.Remove(entity);

        public virtual void Update(TEntity newEntity) => CurrentDbSet.Update(newEntity);

        public Task Commit() => DbContext.SaveChangesAsync();
    }
}
