namespace DataAccessLayer.Repositories;
public class FeedbackBanRepository(AppDbContext dbContext)
    : Repository<FeedbackBan>(dbContext), IFeedbackBanRepository
{
}