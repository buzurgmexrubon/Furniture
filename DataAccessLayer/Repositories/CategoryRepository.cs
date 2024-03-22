namespace DataAccessLayer.Repositories;

public class CategoryRepository(AppDbContext dbContext)
    : Repository<Category>(dbContext),
      ICategoryRepository
{
  public async Task<IEnumerable<Category>> GetAllWithDependenciesAsync()
      => await _dbContext.Categories
                         .Where(c => c.IsActive)
                         .AsNoTracking()
                         .Include(c => c.Furnitures)
                         .ThenInclude(f => f.Images)
                         .Include(c => c.Furnitures)
                         .ThenInclude(f => f.Colors)
                         .Include(c => c.Furnitures)
                         .ThenInclude(f => f.Feedbacks)
                         .ToListAsync();
}