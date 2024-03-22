namespace DataAccessLayer.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
  private readonly AppDbContext dbContext = dbContext;

  public IImageRepository Images => new ImageRepository(dbContext);
  public IFurnitureRepository Furnitures => new FurnitureRepository(dbContext);
  public IColorRepository Colors => new ColorRepository(dbContext);
  public ICategoryRepository Categories => new CategoryRepository(dbContext);

  public IOtpModelRepository OtpModels => new OtpModelRepository(dbContext);

  public IFurnitureColorsRepository FurnitureColors => new FurnitureColorsRepository(dbContext);

  public IFeedbackRepository Feedbacks => new FeedbackRepository(dbContext);

  public IFeedbackBanRepository FeedbackBans => new FeedbackBanRepository(dbContext);

  public void Dispose()
      => GC.SuppressFinalize(this);

  public async Task SaveAsync()
      => await dbContext.SaveChangesAsync();
}