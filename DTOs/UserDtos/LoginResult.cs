﻿namespace DTOs.UserDtos;

public class LoginResult
{
  public string FullName { get; set; } = string.Empty;

  public string PhoneNumber { get; set; } = string.Empty;

  public string AvatarUrl { get; set; } = string.Empty;

  public string Address { get; set; } = string.Empty;

  public DateTime BirthDate { get; set; }

  public Gender Gender { get; set; }

  public string UserId { get; set; } = string.Empty;

  public string? Token { get; set; }

  public List<string> Roles { get; set; } = [];
}