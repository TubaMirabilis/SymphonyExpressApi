namespace SymphonyExpressApi.Data;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SymphonyExpressApi.Models;
public class GenericRepository<TEntity> where TEntity : Entity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private int MaxResultsCountPerPage = 100;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IQueryResult<TEntity>> GetAll()
    {
        return await Get(e => true, null, null);
    }
    public async Task<TEntity> Get(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<IQueryResult<TEntity>> Get(int pageSize, int pageIndex)
    {
        return await Get(e => true, pageSize, pageIndex);
    }
    public async Task<IQueryResult<TEntity>> GetByExpression(Expression<Func<TEntity, bool>> predicate)
    {
        return await Get(predicate.Compile(), null, null);
    }
    public async Task<IQueryResult<TEntity>> GetByExpression(Expression<Func<TEntity, bool>> predicate, int pageSize, int pageIndex)
    {
        return await Get(predicate.Compile(), pageSize, pageIndex);
    }
    private async Task<IQueryResult<TEntity>> Get(Func<TEntity, bool> predicate, int? pageSize, int? pageIndex)
    {
        var filteredItems = 
            predicate != null ? 
                _dbSet.Where(predicate).ToList() : 
                await _dbSet.ToListAsync();
        var finalPageSize = Math.Min(MaxResultsCountPerPage, filteredItems.Count);
        var finalPageIndex = 0;
        if (pageSize != null)
        {
            if (pageSize <= MaxResultsCountPerPage)
            {
                finalPageSize = pageSize.Value;
                finalPageIndex = pageIndex ?? 0;
            }
            else
            {
                finalPageSize = MaxResultsCountPerPage;
                if (pageIndex != null)
                {
                    var oldPagingDescriptor = filteredItems.Page(pageSize.Value);
                    var oldPageBoundries = oldPagingDescriptor.PagesBoundries[pageIndex.Value];
                    var targetedItemZeroIndex = oldPageBoundries.FirstItemZeroIndex;
                    var newPagingDescriptor = filteredItems.Page(finalPageSize);
                    finalPageIndex =
                        newPagingDescriptor
                            .PagesBoundries
                            .ToList()
                            .FindIndex(i => i.FirstItemZeroIndex <= targetedItemZeroIndex && i.LastItemZeroIndex >= targetedItemZeroIndex);
                }
            }
        }
        var pagingDescriptor = filteredItems.Page(finalPageSize);
        var pageBoundries = pagingDescriptor.PagesBoundries[finalPageIndex];
        var from = pageBoundries.FirstItemZeroIndex;
        var to = pageBoundries.LastItemZeroIndex;
        return new QueryResult<TEntity>(pagingDescriptor, finalPageIndex, filteredItems.Skip(from).Take(to - from + 1));
    }
}