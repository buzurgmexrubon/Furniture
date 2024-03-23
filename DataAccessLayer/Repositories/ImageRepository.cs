namespace DataAccessLayer.Repositories;

public class ImageRepository(AppDbContext dbContext)
    : Repository<Image>(dbContext), IImageRepository
{
}
