﻿namespace DataAccessLayer.Entities;

public class User : IdentityUser
{
  [Required, StringLength(100)]
  public string FullName { get; set; } = string.Empty;

  [Required]
  public Gender Gender { get; set; } = Gender.Male;

  [Required]
  public DateTime BirthDate { get; set; }

  public string AvatarUrl { get; set; } = string.Empty;

  [Required, StringLength(200)]
  public string Address { get; set; } = string.Empty;

  public ICollection<Furniture> LikedItems { get; set; }
      = new List<Furniture>();

  public ICollection<Feedback> Feedbacks { get; set; }
      = new List<Feedback>();
}