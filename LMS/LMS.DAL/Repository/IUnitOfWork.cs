using System.Threading.Tasks;

namespace LMS.Repository;


public interface IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    int SaveChanges();
    void Dispose();
    Task<int> SaveChangesAsync();
}