namespace DataAccessLayer.Interfaces;

public interface IUnitOfWork : IDisposable
{
  IImageRepository Images { get; }
  IFurnitureRepository Furnitures { get; }
  IColorRepository Colors { get; }
  ICategoryRepository Categories { get; }
  IOtpModelRepository OtpModels { get; }
  IFurnitureColorsRepository FurnitureColors { get; }
  IFeedbackRepository Feedbacks { get; }
  IFeedbackBanRepository FeedbackBans { get; }
  Task SaveAsync();
}