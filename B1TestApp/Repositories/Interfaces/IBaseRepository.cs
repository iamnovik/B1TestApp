using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace B1TestApp.Repositories.Interfaces;

public interface IBaseRepository<TEntity, TKey>
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task Update(TEntity entity);
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}