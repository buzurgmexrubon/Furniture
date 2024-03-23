namespace DataAccessLayer.Repositories;

public class FeedbackRepository(AppDbContext dbContext)
    : Repository<Feedback>(dbContext), IFeedbackRepository
{
  public async Task<IEnumerable<Feedback>> GetAllAsyncWithDependencies()
      => await _dbContext.Feedbacks
                         .Where(f => f.IsActive)
                         .AsNoTracking()
                         .Include(f => f.User)
                         .Include(f => f.Images)
                         .ToListAsync();
  public async Task<Feedback?> GetAsyncWithDependencies(int id)
      => await _dbContext.Feedbacks
                         .Where(f => f.IsActive)
                         .AsNoTracking()
                         .Include(f => f.User)
                         .Include(f => f.Images)
                         .FirstOrDefaultAsync(f => f.Id == id);
}