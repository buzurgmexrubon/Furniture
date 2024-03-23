namespace BLL.Interfaces;

public interface IColorService
{
  Task<List<ColorDto>> GetAllAsync(Language language);

  Task<PagedList<ColorDto>> GetAllAsync(int pageSize, int pageNumber, Language language);

  Task<ColorDto> GetByIdAsync(int id, Language language);

  Task<SingleColorDto> GetByIdAsync(int id);

  Task<ColorDto> CreateAsync(AddColorDto colorDto, Language language);

  Task<ColorDto> UpdateAsync(UpdateColorDto colorDto, Language language);

  Task ActionAsync(int id, ActionType action);
}