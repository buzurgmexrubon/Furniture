namespace DataAccessLayer.Repositories;

public class ColorRepository(AppDbContext dbContext)
    : Repository<Color>(dbContext), IColorRepository
{
}