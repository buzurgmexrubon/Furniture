namespace DataAccessLayer.Interfaces;

public interface IFurnitureRepository
    : IRepository<Furniture>
{
  Task<IEnumerable<Furniture>> GetAllAsyncWithDependencies();
  Task<Furniture?> GetByIdAsyncWithDependencies(int id);
}