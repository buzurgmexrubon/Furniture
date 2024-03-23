namespace DataAccessLayer.Interfaces;

public interface IFeedbackRepository
    : IRepository<Feedback>
{
  Task<IEnumerable<Feedback>> GetAllAsyncWithDependencies();
  Task<Feedback?> GetAsyncWithDependencies(int id);
}