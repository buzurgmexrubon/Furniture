namespace DataAccessLayer.Repositories;

public class FurnitureColorsRepository(AppDbContext dbContext)
    : IFurnitureColorsRepository
{
  private readonly AppDbContext _dbContext = dbContext;

  public void Add(FurnitureColor entity)
      => _dbContext.FurnitureColors.Add(entity);

  public void DeleteRange(int furnitureId)
  {
    var entities = _dbContext.FurnitureColors
                             .Where(fc => fc.FurnitureId == furnitureId)
                             .ToList();
    _dbContext.FurnitureColors.RemoveRange(entities);
  }

  public IEnumerable<FurnitureColor> GetAll(int furnitureId)
      => _dbContext.FurnitureColors
                    .Where(fc => fc.FurnitureId == furnitureId)
                    .ToList();
}