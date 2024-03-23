﻿namespace BLL.Interfaces;

public interface ICategoryService
{
  Task<List<CategoryDto>> GetAllAsync(Language language);

  Task<PagedList<CategoryDto>> GetAllAsync(int pageSize, int pageNumber, Language language);

  Task<CategoryDto> GetByIdAsync(int id, Language language);

  Task<SingleCategoryDto> GetByIdAsync(int id);

  Task<CategoryDto> CreateAsync(AddCategoryDto categoryDto, Language language);

  Task<CategoryDto> UpdateAsync(UpdateCategoryDto categoryDto, Language language);

  Task ActionAsync(int id, ActionType action);
}