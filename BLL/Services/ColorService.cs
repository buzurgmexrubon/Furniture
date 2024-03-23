﻿namespace BLL.Services;

public class ColorService(IUnitOfWork unitOfWork)
    : IColorService
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  /// <summary>
  /// Create new color
  /// </summary>
  /// <param name="colorDto"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task<ColorDto> CreateAsync(AddColorDto colorDto,
                                             Language language)
  {
    if (colorDto is null)
    {
      throw new FurnitureException("Color was null");
    }

    var model = (Color)colorDto;
    if (!model.IsValidColor())
    {
      throw new FurnitureException("Color is not valid");
    }

    var categories = await _unitOfWork.Colors.GetAllAsync();
    if (model.IsExist(categories))
    {
      throw new FurnitureException("Color already exists");
    }

    model = _unitOfWork.Colors.Add(model);
    await _unitOfWork.SaveAsync();
    return model.ToDto(language);
  }

  /// <summary>
  /// Action with color (archive, unarchive, delete)
  /// </summary>
  /// <param name="id"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task ActionAsync(int id, ActionType action)
  {
    var color = await _unitOfWork.Colors.GetByIdAsync(id);
    if (color is null)
    {
      throw new FurnitureException("Color not found");
    }

    switch (action)
    {
      case ActionType.Archive:
        {
          color.IsActive = false;
          _unitOfWork.Colors.Update(color);
        }
        break;
      case ActionType.UnArchive:
        {
          color.IsActive = true;
          _unitOfWork.Colors.Update(color);
        }
        break;
      case ActionType.Delete:
        {
          _unitOfWork.Colors.Delete(id);
        }
        break;
    }
    await _unitOfWork.SaveAsync();
  }

  /// <summary>
  /// Get all categories with pagination
  /// </summary>
  /// <param name="pageSize"></param>
  /// <param name="pageNumber"></param>
  /// <returns></returns>
  public async Task<PagedList<ColorDto>> GetAllAsync(int pageSize, int pageNumber, Language language)
  {
    var categories = await _unitOfWork.Colors.GetAllAsync();
    var colorDtos = categories.Select(c => c.ToDto(language)).ToList();
    return new PagedList<ColorDto>(colorDtos, colorDtos.Count, pageNumber, pageSize);
  }

  /// <summary>
  /// Get all categories
  /// </summary>
  /// <returns></returns>
  public async Task<List<ColorDto>> GetAllAsync(Language language)
  {
    var categories = await _unitOfWork.Colors.GetAllAsync();
    var colorDtos = categories.Select(c => c.ToDto(language)).ToList();
    return colorDtos;
  }

  /// <summary>
  /// Get color by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task<ColorDto> GetByIdAsync(int id, Language language)
  {
    var color = await _unitOfWork.Colors.GetByIdAsync(id);
    return color is null ?
        throw new FurnitureException("Color not found") : color.ToDto(language);
  }

  /// <summary>
  /// Update color
  /// </summary>
  /// <param name="colorDto"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task<ColorDto> UpdateAsync(UpdateColorDto colorDto,
                                             Language language)
  {
    if (colorDto is null)
    {
      throw new FurnitureException("Color was null");
    }

    var color = await _unitOfWork.Colors.GetByIdAsync(colorDto.Id);
    if (color is null)
    {
      throw new FurnitureException("Color not found");
    }

    var model = (Color)colorDto;
    if (!model.IsValidColor())
    {
      throw new FurnitureException("Color is not valid");
    }

    var categories = await _unitOfWork.Colors.GetAllAsync();
    if (model.IsNotUnique(categories))
    {
      throw new FurnitureException("Color already exists");
    }

    _unitOfWork.Colors.Update(model);
    await _unitOfWork.SaveAsync();
    model = await _unitOfWork.Colors.GetByIdAsync(colorDto.Id);
    if (model is null)
    {
      throw new FurnitureException("Color not found");
    }
    return model.ToDto(language);
  }

  /// <summary>
  /// Get color by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public async Task<SingleColorDto> GetByIdAsync(int id)
  {
    var color = await _unitOfWork.Colors.GetByIdAsync(id);
    if (color is null)
    {
      throw new ArgumentNullException("Color not found");
    }
    return (SingleColorDto)color;
  }
}