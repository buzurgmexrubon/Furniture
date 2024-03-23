namespace MobileBLL.Interfaces;

public interface IFurnitureService
{
  Task<List<FurnitureDto>> GetFurnituresAsync(Language language);
  Task<SingleFurnitureDto> GetSingleFurnitureAsync(int id, Language language);
}