namespace DataAccessLayer.Interfaces;

public interface IFurnitureColorsRepository
{
  IEnumerable<FurnitureColor> GetAll(int furnitureId);
  void Add(FurnitureColor entity);
  void DeleteRange(int furnitureId);
}