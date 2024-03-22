namespace DataAccessLayer.Repositories;

public class OtpModelRepository(AppDbContext dbContext)
    : Repository<OtpModel>(dbContext), IOtpModelRepository
{
}