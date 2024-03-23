namespace DataAccessLayer.Repositories;

public class FurnitureRepository(AppDbContext dbContext)
    : Repository<Furniture>(dbContext), IFurnitureRepository
{
  public async Task<IEnumerable<Furniture>> GetAllAsyncWithDependencies()
      => await _dbContext.Furnitures
                          .Where(f => f.IsActive)
                          .AsNoTracking()
                          .Include(f => f.Category)
                          .Include(f => f.Images)
                          .Include(f => f.Feedbacks)
                          .Include(f => f.Colors)
                          .ThenInclude(c => c.Color)
                          .ToListAsync();

  public async Task<Furniture?> GetByIdAsyncWithDependencies(int id)
      => await _dbContext.Furnitures
                          .Where(f => f.IsActive)
                          .AsNoTracking()
                          .Include(f => f.Category)
                          .Include(f => f.Images)
                          .Include(f => f.Feedbacks)
                          .Include(f => f.Colors)
                          .ThenInclude(c => c.Color)
                          .FirstOrDefaultAsync(f => f.Id == id);
}