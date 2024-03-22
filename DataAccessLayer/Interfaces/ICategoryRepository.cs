namespace DataAccessLayer.Interfaces;

public interface ICategoryRepository
    : IRepository<Category>
{
  Task<IEnumerable<Category>> GetAllWithDependenciesAsync();
}