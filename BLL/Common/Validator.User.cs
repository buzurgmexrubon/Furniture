﻿namespace BLL.Common;

public partial class Validator
{
  public static bool IsValid(this RegisterUserDto dto)
      => dto is not null
          && !string.IsNullOrWhiteSpace(dto.PhoneNumber)
          && !string.IsNullOrWhiteSpace(dto.Password)
          && !string.IsNullOrWhiteSpace(dto.FullName);

  public static bool IsValid(this NewAdminDto dto)
      => dto is not null
          && !string.IsNullOrWhiteSpace(dto.PhoneNumber)
          && !string.IsNullOrWhiteSpace(dto.FullName);

  public static bool IsValid(this LoginUserDto dto)
      => dto is not null
          && !string.IsNullOrWhiteSpace(dto.PhoneNumber)
          && !string.IsNullOrWhiteSpace(dto.Password);
}